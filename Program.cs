/*
    dotnet dev-certs http --trust
    dotnet add package Microsoft.EntityFrameworkCore.Relational
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.Tools
    dotnet add package Microsoft.EntityFrameworkCore.Tools.DotNet
    dotnet add package Swashbuckle.AspNetCore
*/
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Desenvolvimento de Back-End",
        Description = "Serviço em REST desenvolvido na disciplina!",
        Contact = new OpenApiContact
        {
            Name = "Adilson Reis, Alexys Santiago, Raphaela Vieira",
            Url = new Uri("https://github.com/SuxPorT/projeto-dotnet-sql"),
        },
        TermsOfService = new Uri("https://example.com/terms")
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto final em .NET e SQL");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
