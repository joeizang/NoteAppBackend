using Microsoft.EntityFrameworkCore;
using NoteAppBackend.Persistence;
using dotenv.net;
using NoteAppBackend.DomainModels;
using NoteAppBackend.Persistence.DataGenerators;
using NoteAppBackend.ApiEndpoints;

DotEnv.Load();
var env = DotEnv.Read();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NoteAppBackendContext>(opt =>
{
    opt.UseNpgsql(env["POSTGRES_CONN_STRING"])
        .UseAsyncSeeding(async (context, _, token) =>
        {
            var test = await context.Set<NoteType>().AnyAsync(token).ConfigureAwait(false);
            if(!test)
            {
                var noteTypeGen = new NoteTypeGenerator();
                var types = noteTypeGen.GetNoteTypes();
                context.Set<NoteType>().AddRange(types);
                await context.SaveChangesAsync(token);
            }
        });
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

await using (var serviceScope = app.Services.CreateAsyncScope())
await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<NoteAppBackendContext>())
{
    await dbContext.Database.EnsureCreatedAsync();
}

app.MapNotesEndpoints();
app.MapNoteTypeEndpoints();

app.Run();
