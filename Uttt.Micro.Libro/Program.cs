using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Uttt.Micro.Libro.Extenciones;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var testConnection = builder.Configuration.GetConnectionString("DefaultsConnections");
Console.WriteLine("CADENA DE CONEXIÓN CARGADA ====> " + testConnection);

builder.Services.AddCustomServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Puerto de tu app React
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(int.Parse(port));
});


var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();

// Configure the HTTP request pipeline.

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["ApiSettings:JwtOptions:Issuer"],
//        ValidAudience = builder.Configuration["ApiSettings:JwtOptions:Audience"],
//        IssuerSigningKey = key
//    };
//});




// Habilitar CORS



app.UseCors(); // Agregar antes de UseAuthorization()

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
