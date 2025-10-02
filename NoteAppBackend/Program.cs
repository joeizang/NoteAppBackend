using Microsoft.EntityFrameworkCore;
using NoteAppBackend.Persistence;
using dotenv.net;

DotEnv.Load();
var env = DotEnv.Read();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NoteAppBackendContext>(opt =>
{
    opt.UseNpgsql(env["POSTGRES_CONN_STRING"], act => act.UseNodaTime());
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();
