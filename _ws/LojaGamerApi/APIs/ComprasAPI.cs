using Microsoft.EntityFrameworkCore;

public static class ComprasAPI
{
    public static void MapComprasAPI(this WebApplication app)

    {
        var group = app.MapGroup("/compras");

        group.MapGet("/", async (BancoDeDados db) =>
        {
            return await db.Compra.Include(c => c.clientes).ToListAsync();
        }
        );

        group.MapPost("/", async (Compra compra, BancoDeDados db) =>
        {
            Console.WriteLine($"Compra: {compra}");

            compra.clientes = await SalvarClientes(compra, db);

            db.Compra.Add(compra);

            await db.SaveChangesAsync();

            return Results.Created($"/compras/{compra.Id}", compra);
        }
      );
        async Task<List<Cliente>> SalvarClientes(Compra compra, BancoDeDados db)
        {
            List<Cliente> clientes = new();
            if (compra is not null && compra.clientes is not null
            && compra.clientes.Count > 0)
            {

                foreach (var cliente in compra.clientes)
                {
                    Console.WriteLine($"cliente: {cliente}");
                    if (cliente.Id > 0)
                    {
                        var eExistente = await db.Cliente.FindAsync(cliente.Id);
                        if (eExistente is not null)
                        {
                            clientes.Add(eExistente);
                        }
                    }
                    else
                    {
                        clientes.Add(cliente);
                    }
                }
            }
            return clientes;
        }

        group.MapPut("/{id}", async (int id, Compra compraAlterado, BancoDeDados db) =>
            {
                //select * from clientes where id = ?
                var compra = await db.Compra.FindAsync(id);
                if (compra is null)
                {
                    return Results.NotFound();
                }
                compra.NumeroPedido = compraAlterado.NumeroPedido;
                compra.CPF = compraAlterado.CPF;
                compra.Titulo = compraAlterado.Titulo;

                // Tratamento para salvar endereços com e sem Ids.
                compra.clientes = await SalvarClientes(compra, db);

                //update....
                await db.SaveChangesAsync();

                return Results.NoContent();
            }
        );
        group.MapDelete("/{id}", async (int id, BancoDeDados db) =>
     {
         if (await db.Compra.FindAsync(id) is Compra compra)
         {
             //Operações de exclusão
             db.Compra.Remove(compra);
             //delete from...
             await db.SaveChangesAsync();
             return Results.NoContent();
         }
         return Results.NotFound();
     }
   );



    }
}