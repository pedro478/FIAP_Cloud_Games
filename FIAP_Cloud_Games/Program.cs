using FIAP_Cloud_Games.Application.Interfaces;
using FIAP_Cloud_Games.Application.Services;
using FIAP_Cloud_Games.Domain.Interfaces;
using FIAP_Cloud_Games.Infra;
using FIAP_Cloud_Games.Infra.Data;
using FIAP_Cloud_Games.Infra.Middleware;
using FIAP_Cloud_Games.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var secretKey = builder.Configuration["JwtSettings:SecretKey"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddCorrelationIdGenerator();
builder.Services.AddTransient(typeof(BaseLogger<>));




builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(connectionString));

builder.Services.AddTransient<tokenService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IJogoRepository, JogoRepository>();
builder.Services.AddScoped<IJogoService, JogoService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key
        };
    });

// Configuração do Swagger para aceitar Bearer Token
builder.Services.AddSwaggerGen(c =>
{

    c.SchemaFilter<EnumSchemaFilter>();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FIAP Cloud Games API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT no formato: Bearer {seu_token_aqui}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();



var app = builder.Build();

//realiza o primeiro migration do banco
using (var scope =  app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate();

}

//função inicial para criar um usuario default
await SeedInicial.SeedDefaultUserAsync(app.Services);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

   
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
