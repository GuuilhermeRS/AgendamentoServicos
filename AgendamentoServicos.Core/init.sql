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
    status varchar(50) not null,
    customer_id int not null,
    professional_id int not null,
    service_id int not null,
    created_at datetime not null default current_timestamp,
    foreign key (customer_id) references customer(id),
    foreign key (professional_id) references professional(id),
    foreign key (service_id) references service(id)
);