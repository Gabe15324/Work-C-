public class Venda
{
    public int Id { get; set; }
    public string? NotaFiscal { get; set; }
    public string? Titulo { get; set; }
    public string? CPF { get; set; }

    public List<Games>? games { get; set; } 
    public override string ToString()
    {
        return $"Id: {Id}, Titulo: {Titulo}, Total de Vendas: {games?.Count ?? 0}"; 
    }
}