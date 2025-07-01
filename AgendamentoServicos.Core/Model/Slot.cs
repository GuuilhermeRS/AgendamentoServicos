namespace AgendamentoServicos.Core.Model;

public class Slot
{
    public int Id { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    public string Status { get; private set; }
    public int CustomerId { get; private set; }
    public Customer Customer { get; private set; }
    public int ProfessionalId { get; private set; }
    public Professional Professional { get; private set; }
    public int ServiceId { get; private set; }
    public Service Service { get; private set; }
}