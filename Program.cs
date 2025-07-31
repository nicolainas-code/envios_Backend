using Microsoft.EntityFrameworkCore;
using WebApi.Data.Repository;
using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// agregar politica de cors

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.AllowAnyOrigin() //.WithOrigins("http://127.0.0.1:5503") // este es la URL de cuando compilo el html(EL SITIO WEB)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddDbContext<EnviosDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//inyeccion de dependencias
builder.Services.AddScoped<IEnvioRepository, EnviosRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseRouting();
app.UseCors("PermitirFrontend");
app.UseAuthorization();

app.MapControllers();

app.Run();
