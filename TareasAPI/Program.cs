using Microsoft.EntityFrameworkCore;
using TareasAPI.Middlewares;
using TareasBLL.Interfaces;
using TareasBLL.Services;
using TareasDLL.Context;
using TareasDLL.Interfaces;
using TareasDLL.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
});
// ?? Configuración de la cadena de conexión (appsettings.json)
builder.Services.AddDbContext<DB_AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ?? Inyección de dependencias
builder.Services.AddScoped<ITareasRepository, TareasRepository>();
builder.Services.AddScoped<ITareasService, TareasService>();
builder.Services.AddScoped<IReportesRepository, ReportesRepository>();
builder.Services.AddScoped<IReportesService, ReportesService>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ?? Middleware global de excepciones (si lo implementaste)
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
