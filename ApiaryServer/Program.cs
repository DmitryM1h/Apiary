using ApiaryEngine;
using ApiaryEngine.Domain;

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
        policy.WithOrigins("http://localhost:5175")
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

app.MapGet("api/Flowers", (CancellationToken token) =>
{

    var response = from flower in Apiary.Flowers
                   join flowerPos in Apiary.FlowerPositions on flower.Key equals flowerPos.Key
                   select new
                   {
                       flowerId = flower.Key,
                       position = flowerPos.Value,
                       amountOfNectar = flower.Value.NectarAmount
                   };

    return response;
});

app.Run();
