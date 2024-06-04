using Microsoft.EntityFrameworkCore;

public static class GamesAPI
{
    public static void MapVendasAPI(this WebApplication app)

    {
        var group = app.MapGroup("/Vendas");

        group.MapGet("/", async (BancoDeDados db) =>
        {
            return await db.Games.Include(c => c.vendas).ToListAsync();
        }
        );

        group.MapPost("/", async (Games game, BancoDeDados db) =>
        {
            Console.WriteLine($"Games: {game}");

            game.vendas = await SalvarVendas(game, db);

            db.Games.Add(game);

            await db.SaveChangesAsync();

            return Results.Created($"/games/{game.Id}", game);
        }
      );
        async Task<List<Venda>> SalvarVendas(Games game, BancoDeDados db)
        {
            List<Venda> vendas = new();
            if (game is not null && game.vendas is not null
            && game.vendas.Count > 0)
            {

                foreach (var venda in game.vendas)
                {
                    Console.WriteLine($"venda: {venda}");
                    if (venda.Id > 0)
                    {
                        var eExistente = await db.Venda.FindAsync(game.Id);
                        if (eExistente is not null)
                        {
                            vendas.Add(eExistente);
                        }
                    }
                    else
                    {
                        vendas.Add(venda);
                    }
                }
            }
            return vendas;
        }

        group.MapPut("/{id}", async (int id, Games gameAlterado, BancoDeDados db) =>
{
    //select * from games where id = ?
    var game = await db.Games.FindAsync(id);
    if (game is null)
    {
        return Results.NotFound();
    }
    game.Titulo = gameAlterado.Titulo;

    // Tratamento para salvar vendas com e sem Ids.
    game.vendas = await SalvarVendas(gameAlterado, db);

    //update....
    await db.SaveChangesAsync();

    return Results.NoContent();
});

        group.MapDelete("/{id}", async (int id, BancoDeDados db) =>
     {
         if (await db.Games.FindAsync(id) is Games game)
         {
             //Operações de exclusão
             db.Games.Remove(game);
             //delete from...
             await db.SaveChangesAsync();
             return Results.NoContent();
         }
         return Results.NotFound();
     });



    }
}