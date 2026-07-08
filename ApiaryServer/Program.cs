using ApiaryEngine;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{

    opt.AddPolicy("AllowVue", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowVue");

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

var apiaryEngine = new ApiarySimulationEngine();
_ = Task.Run( () => apiaryEngine.Run());

app.MapGet("api/ApiaryStates", (CancellationToken token) =>
 {
     try
     {
         return Results.ServerSentEvents(
             apiaryEngine._stateReader.ReadAllAsync(token));
     }
     catch (OperationCanceledException)
     {
         return Results.Empty;
     }
 });

app.Run();
