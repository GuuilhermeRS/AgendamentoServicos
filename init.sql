create table `customer` (
    id int not null auto_increment primary key,
    name varchar(255) not null,
    email varchar(255) not null unique,
    cellphone varchar(20) not null,
    created_at datetime not null default current_timestamp
);

create table `professional` (
    id int not null auto_increment primary key,
    name varchar(255) not null,
    specialty varchar(255) not null,
    email varchar(255) not null unique,
    cellphone varchar(20) not null,
    created_at datetime not null default current_timestamp
);

create table `service` (
   id int not null auto_increment primary key,
   name varchar(255) not null,
   description text,
   duration int not null,
   value int not null,
   created_at datetime not null default current_timestamp
);

create table `slot` (
    id int not null auto_increment primary key,
    date datetime not null,
    description text,
    status int not null,
    customer_id int null,
    professional_id int not null,
    service_id int not null,
    created_at datetime not null default current_timestamp,
    foreign key (customer_id) references customer(id),
    foreign key (professional_id) references professional(id),
    foreign key (service_id) references service(id)
);

create table `transaction_log` (
    id int auto_increment primary key,
    user varchar(100) not null,
    action_time datetime not null default current_timestamp,
    operation_type varchar(50) not null,
    affected_table varchar(50),
    description text
);

create view `vw_slot_details` as
    select
        s.id as slot_id,
        s.date as slot_date,
        s.status as slot_status,
        c.id as customer_id,
        c.name as customer_name,
        c.email as customer_email,
        p.id as professional_id,
        p.name as professional_name,
        p.specialty as professional_specialty,
        srv.id as service_id,
        srv.name as service_name,
        srv.description as service_description,
        srv.duration as service_duration,
        srv.value as service_value
    from
        slot s
        join customer c on s.customer_id = c.id
        join professional p on s.professional_id = p.id
        join service srv on s.service_id = srv.id
    order by s.date DESC;
    
create view `vw_professional_billing` as
    select
        p.id as professional_id,
        p.name as professional_name,
        srv.value as service_value,
        s.date as slot_date
    from slot s
        join professional p on s.professional_id = p.id
        join service srv on s.service_id = srv.id
    where s.status = 'confirmed';

delimiter $$
create procedure `sp_create_available_slots`(
    in p_professional_id int,
    in p_service_id int,
    in p_begin_date datetime,
    in p_end_date datetime
)
begin
    declare v_service_duration int;
   
    select duration into v_service_duration
    from service
    where id = p_service_id;

    if v_service_duration is null then
       signal sqlstate '45000' set message_text = 'Service not found';
    end if;
    
    insert into slot (date, status, professional_id, service_id, customer_id, description)
           with recursive potential_slots (slot_time) as (
                select p_begin_date where hour(p_begin_date) >= 8 and hour(p_begin_date) < 18
                union all
                select date_add(slot_time, interval v_service_duration minute)
                from potential_slots
                where
                    date_add(slot_time, interval v_service_duration minute) <= p_end_date
                    and hour(date_add(slot_time, interval v_service_duration minute)) >= 8
                    and hour(date_add(slot_time, interval v_service_duration minute)) < 18
           )
           select ps.slot_time, 'available', p_professional_id, p_service_id, null, 'Horario Disponivel'
           from potential_slots ps
                left join slot booked_slots on ps.slot_time = booked_slots.date and booked_slots.professional_id = p_professional_id and booked_slots.status <> 'cancelled'
           where booked_slots.id is null;
           
end $$

delimiter $$;

delimiter $$

create function fn_get_end_slot_time(
    p_service_id int,
    p_begin_time datetime
)
returns datetime
deterministic
begin
    declare v_service_duration_in_minutes int;
    declare v_end_time datetime;
            
    select duration into v_service_duration_in_minutes
    from service
    where id = p_service_id;

    if v_service_duration_in_minutes is null then
        return p_begin_time;
    end if;
    
    set v_end_time = date_add(p_begin_time, interval v_service_duration_in_minutes minute);
        
    return v_end_time;
end $$

delimiter ;

