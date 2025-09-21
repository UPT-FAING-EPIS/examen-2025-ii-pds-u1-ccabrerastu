using Microsoft.EntityFrameworkCore;
using Exam.Api.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Configuración de MySQL
// ----------------------------
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connStr, ServerVersion.AutoDetect(connStr))
);

// ----------------------------
// Habilitar CORS para React
// ----------------------------
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000") // tu frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
              //.AllowCredentials(); // habilitar si usas cookies o auth
    });
});

// ----------------------------
// Controllers y Swagger
// ----------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Exam API",
        Version = "v1",
        Description = "API para gestionar exámenes, preguntas y envíos"
    });
});

var app = builder.Build();

// ----------------------------
// Middleware
// ----------------------------

// Activar CORS antes de los controladores
app.UseCors();

// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exam API V1");
    });
}

// En desarrollo, puedes comentar HTTPS si React usa HTTP
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
