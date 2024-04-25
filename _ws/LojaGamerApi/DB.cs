
using Microsoft.EntityFrameworkCore;

public class BancoDeDados : DbContext
{
    private object server;

    Protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        Builder.UserMySQL(“server=localhost;port=3306;database=LojaGamesApi;user=root;password=positivo);
    }

    public DbSet<Cliente> Cilente {get; set;}
    public DbSet<Cliente> Games {get; set;}
    public DbSet<Cliente> Venda {get; set;}
    public DbSet<Cliente> Compra {get; set;}
    public object LojaGamesApi { get; private set; }
}

internal class Protected
{
}