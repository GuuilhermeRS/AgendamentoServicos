namespace AgendamentoServicos.Core.Model;

public class Professional
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Specialty { get; private set; }
    public string Cellphone { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Professional(string name, string specialty, string? cellphone = null, string? email = null)
    {
        Name = name;
        Specialty = specialty;
        Cellphone = cellphone;
        Email = email;
    }

    public void Update(string name, string specialty, string? cellphone, string? email)
    {
        if (!string.IsNullOrEmpty(name)) Name = name;
        if (!string.IsNullOrEmpty(specialty)) Specialty = specialty;
        if (!string.IsNullOrEmpty(cellphone)) Cellphone = cellphone;
        if (!string.IsNullOrEmpty(email)) Email = email;
    }
}