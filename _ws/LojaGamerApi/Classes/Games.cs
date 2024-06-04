public class Games
{
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public List<Venda>? vendas { get; set; }
    public override string ToString()
    {
        return $"Id: {Id}, Titulo: {Titulo}, Total de Vendas: {vendas?.Count ?? 0}";
    }
}