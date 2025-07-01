namespace AgendamentoServicos.Core.Model;

public class Service
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Duration { get; private set; }
    public int Value { get; private set; }
}