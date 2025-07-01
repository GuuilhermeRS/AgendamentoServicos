namespace AgendamentoServicos.Core.Model;

public class Customer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Cellphone { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Customer(string name, string cellphone, string email)
    {
        Name = name;
        Cellphone = cellphone;
        Email = email;
    }
    
    public void Update(string name, string cellphone, string email)
    {
        if (!string.IsNullOrEmpty(name)) Name = name;
        if (!string.IsNullOrEmpty(cellphone)) Cellphone = cellphone;
        if (!string.IsNullOrEmpty(email)) Email = email;
    }
}