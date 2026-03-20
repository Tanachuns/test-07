using products.Interfaces;
using products.Repositories;
using products.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(
        "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:ddMMyyyy HH:mm:ss} [{Level:u3}]  [Trace:{TraceId}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173") // React / Vite
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});



//Map Interface with Controller
builder.Services.AddRepositories().AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
