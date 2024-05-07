using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
    options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

builder.Services.AddDbContext<BancoDeDados>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//https://localhost:5096/swagger/index.html

app.MapGet("/", () => "Loja de Games com EF + Swagger");
app.MapCli


app.MapGet("/", () => "Loja Gamer");

app.MapGet("/cliente", async (BancoDeDados db) =>

await db.Cliente.ToListAsync()
);


app.MapPost("/cliente", async (Cliente cliente, BancoDeDados db) =>
{

    db.Cliente.Add(cliente);

    await db.SaveChangesAsync();

    return Results.Created($"/clinte/{cliente.Id}", cliente);
}
);


app.MapPut("/cliente/{id}", async (int id, Cliente clienteAlterada,
 BancoDeDados db) =>
 {
     var cliente = await db.Cliente.FindAsync(id);
     if (cliente is null)
     {
         return Results.NotFound();
     }

     cliente.Nome = clienteAlterada.Nome;
     cliente.CPF = clienteAlterada.CPF;
     cliente.Telefone = clienteAlterada.Telefone;
     cliente.Email = clienteAlterada.Email;

     await db.SaveChangesAsync();

     return Results.NoContent();
 }
 );



app.MapDelete("/cliente/{id}", async (int id, BancoDeDados db) =>
{
    if (await db.Cliente.FindAsync(id) is Cliente cliente)
    {
        //
        db.Cliente.Remove(cliente);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
}
);



app.Run();
