using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
  
}).UseLightweightSessions();

builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapCarter();

app.Run();
