using ApiaryEngine;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var apiaryEngine = new Engine();
_ = Task.Run( () => apiaryEngine.Run());

app.MapGet("api/ApiaryStates", (CancellationToken token) =>
 {
     try
     {
         return Results.ServerSentEvents(
             apiaryEngine._stateReader.ReadAllAsync(token),
             "ActorsStates");
     }
     catch (OperationCanceledException)
     {
         return Results.Empty;
     }
 });

app.Run();
