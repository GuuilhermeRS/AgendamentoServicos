namespace AgendamentoServicos.Core.Model;

public class Service
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Duration { get; private set; }
    public int Value { get; private set; }

    public Service(string name, string description, int duration, int value)
    {
        Name = name;
        Description = description;
        Duration = duration;
        Value = value;
    }

    public void Update(string name, string? description, int durationInMinutes, int value)
    {
        Name = name;
        Description = description;
        Duration = durationInMinutes;
        Value = value;
    }
}