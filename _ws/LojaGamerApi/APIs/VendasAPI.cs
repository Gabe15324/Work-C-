using Microsoft.EntityFrameworkCore;

public static class VendasAPI
{
    public static void MapGamesAPI(this WebApplication app)

    {
        var group = app.MapGroup("/Games");

        group.MapGet("/", async (BancoDeDados db) =>
        {
            return await db.Venda.Include(c => c.games).ToListAsync();
        }
        );

        group.MapPost("/", async (Venda venda, BancoDeDados db) =>
        {
            Console.WriteLine($"Vendas: {venda}");

            venda.games = await SalvarGames(venda, db);

            db.Venda.Add(venda);

            await db.SaveChangesAsync();

            return Results.Created($"/vendas/{venda.Id}", venda);
        }
      );

        async Task<List<Games>> SalvarGames(Venda venda, BancoDeDados db)
        {
            List<Games> games = new();
            if (venda is not null && venda.games is not null
            && venda.games.Count > 0)
            {

                foreach (var game in venda.games)
                {
                    Console.WriteLine($"game: {game}");
                    if (game.Id > 0)
                    {
                        var eExistente = await db.Games.FindAsync(venda.Id);
                        if (eExistente is not null)
                        {
                            games.Add(eExistente);
                        }
                    }
                    else
                    {
                        games.Add(game);
                    }
                }
            }
            return games;
        }

        group.MapPut("/{id}", async (int id, Venda vendaAlterado, BancoDeDados db) =>
        {
            //select * from games where id = ?
            var venda = await db.Venda.FindAsync(id);
            if (venda is null)
            {
                return Results.NotFound();
            }
            venda.Titulo = vendaAlterado.Titulo;

            // Tratamento para salvar vendas com e sem Ids.
            venda.games = await SalvarGames(vendaAlterado, db);

            //update....
            await db.SaveChangesAsync();

            return Results.NoContent();
        }
        );


        group.MapDelete("/{id}", async (int id, BancoDeDados db) =>
     {
         if (await db.Venda.FindAsync(id) is Venda venda)
         {
             //Operações de exclusão
             db.Venda.Remove(venda);
             //delete from...
             await db.SaveChangesAsync();
             return Results.NoContent();
         }
         return Results.NotFound();
     });



    }
}