public class Compra
{
    public int Id { get; set; }
    public string? NumeroPedido { get; set; }
    public string? Titulo { get; set; }
    public string? CPF { get; set; }
    public List<Cliente>? clientes { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, NumeroPedido: {NumeroPedido}, CPF: {CPF}, Titulo: {Titulo}";
    }
}
