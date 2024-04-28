using Microsoft.EntityFrameworkCore;

public class BancoDeeDados : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseOracle("server=localhost;port=3306;database=lojagames;user=root;password=positivo");
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Games> Games { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<Compra> Compras { get; set; }
}