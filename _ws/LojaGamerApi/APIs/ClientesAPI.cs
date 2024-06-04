using Microsoft.EntityFrameworkCore;

public static class ClienteAPI
{
    public static void MapClienteAPI(this WebApplication app)

    {
        var group = app.MapGroup("/Clientes");

        group.MapGet("/", async (BancoDeDados db) =>
        {
            return await db.Cliente.Include(c => c.compras).ToListAsync();
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
                    Console.WriteLine($"compra: {compra}");
                    if (compra.Id > 0)
                    {
                        var eExistente = await db.Compra.FindAsync(compra.Id);
                        if (eExistente is not null)
                        {
                            compras.Add(eExistente);
                        }
                    }
                    else
                    {
                        compras.Add(compra);
                    }
                }
            }
            return compras;
        }

        group.MapPut("/{id}", async (int id, Cliente clienteAlterado, BancoDeDados db) =>
            {
                //select * from clientes where id = ?
                var cliente = await db.Cliente.FindAsync(id);
                if (cliente is null)
                {
                    return Results.NotFound();
                }
                cliente.Nome = clienteAlterado.Nome;
                cliente.Telefone = clienteAlterado.Telefone;
                cliente.Email = clienteAlterado.Email;
                cliente.CPF = clienteAlterado.CPF;

                // Tratamento para salvar endereços com e sem Ids.
                cliente.compras = await SalvarCompras(cliente, db);

                //update....
                await db.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        group.MapDelete("/{id}", async (int id, BancoDeDados db) =>
     {
         if (await db.Cliente.FindAsync(id) is Cliente cliente)
         {
             //Operações de exclusão
             db.Cliente.Remove(cliente);
             //delete from...
             await db.SaveChangesAsync();
             return Results.NoContent();
         }
         return Results.NotFound();
     }
   );



    }
}