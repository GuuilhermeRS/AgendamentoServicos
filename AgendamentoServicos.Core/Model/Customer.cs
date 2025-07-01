namespace AgendamentoServicos.Core.Model;

public class Customer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Cellphone { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedAt { get; private set; }
}