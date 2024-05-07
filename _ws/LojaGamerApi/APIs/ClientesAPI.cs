using Microsoft.EntityFrameworkCore;

public static class ClienteAPI
{
    public static void MapClienteAPI(this WebApplication app)
    {
        var group = app.MapGroup("/Clientes");

        group.MapGet("/", async (BancoDeDados db) =>
        {
            return await db.Clientes.Include(c => c.compras)ToListAsync();
        }
        );

        group.MapPost("/", async (Cliente cliente, BancoDeDados db) =>
        {
            Console.WriteLine($"Cliente: {cliente}");

            cliente.compras = await SalvarCompras(cliente, db);

            db.Cliente.Add(cliente);

            await db.SaveChangesAsync();

            return Results.Created($"/clientes/{cliente.Id}", cliente);
        }
      );
        async Task<List<Compra>> SalvarCompras(Cliente cliente, BancoDeDados db)
        {
            List<Compra> compras = new();
            if (cliente is not null && cliente.compras is not null
            && cliente.compras.Count > 0)
            {

                foreach (var compra in cliente.compras)
                {

                }
            }
        }
    }
}