using Microsoft.EntityFrameworkCore;
using NoteAppBackend.Persistence;
using dotenv.net;
using NoteAppBackend.DomainModels;
using NoteAppBackend.Persistence.DataGenerators;
using NoteAppBackend.ApiEndpoints;
using Microsoft.AspNetCore.Components.Web;
using NoteAppBackend.Persistence.PersistenceServices;

DotEnv.Load();
var env = DotEnv.Read();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NoteAppBackendContext>(opt =>
{
    // opt.UseModel(NoteAppBackendContextModel.Instance);
    //opt.UseNpgsql(env["POSTGRES_CONN_STRING"])
    opt.UseSqlite(env["SQLITE_CONN_STRING"] ?? "Data Source=noteapp.db")
    .UseSeeding(async (context, _) =>
        {
            var test = context.Set<NoteType>().Any();
            if(!test)
            {
                var noteTypeGen = new NoteTypeGenerator();
                var types = noteTypeGen.GetNoteTypes();
                context.Set<NoteType>().AddRange(types);
                context.SaveChanges();
            }
        })
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
builder.Services.AddScoped<ICommandService, CommandService>();

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
