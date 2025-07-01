namespace AgendamentoServicos.Core.Model;

public class Slot
{
    public int Id { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    public string Status { get; private set; } //TODO: migrar para enum
    public int CustomerId { get; private set; }
    public Customer Customer { get; private set; }
    public int ProfessionalId { get; private set; }
    public Professional Professional { get; private set; }
    public int ServiceId { get; private set; }
    public Service Service { get; private set; }
    
    private Slot() { }

    public Slot(DateTime date, Customer customer, Professional professional, Service service, string description)
    {
        Date = date;
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        Professional = professional ?? throw new ArgumentNullException(nameof(professional));
        Service = service ?? throw new ArgumentNullException(nameof(service));
        Description = description ?? string.Empty;
        Status = "Scheduled";
    }
    
    public void Cancel()
    {
        // REGRA DE NEGÓCIO: Não é possível cancelar um agendamento que já foi concluído.
        if (Status == "Completed")
        {
            throw new InvalidOperationException("Não é possível cancelar um agendamento já concluído.");
        }
        
        Status = "Canceled";
    }
    
    public void Complete()
    {
        // REGRA DE NEGÓCIO: Não é possível concluir um agendamento que já foi cancelado.
        if (Status == "Canceled")
        {
            throw new InvalidOperationException("Não é possível concluir um agendamento que foi cancelado.");
        }

        Status = "Completed";
    }
}