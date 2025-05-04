using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Data;
using MyApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRealmDbContextFactory, RealmDbContextFactory>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration["DbFolder"] = "Databases"; // default location

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet(
    "/api/{realm}/users",
    async (string realm, IRealmDbContextFactory factory) =>
    {
        using var db = factory.CreateDbContext(realm);
        var users = await db.Users.ToListAsync();
        return Results.Ok(new { success = true, data = users });
    }
);

app.MapPost(
    "/api/{realm}/users",
    async (string realm, User user, IRealmDbContextFactory factory) =>
    {
        using var db = factory.CreateDbContext(realm);
        db.Users.Add(user);
        await db.SaveChangesAsync();
        return Results.Created($"/api/{realm}/users/{user.Id}", user);
    }
);

app.Run();
