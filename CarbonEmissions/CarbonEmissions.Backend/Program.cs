using CarbonEmissions.Backend.Data;
using CarbonEmissions.Backend.Repositories.Implementations;
using CarbonEmissions.Backend.Repositories.Interfaces;
using CarbonEmissions.Backend.UnitOfWork.Implementations;
using CarbonEmissions.Backend.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        var _userName = configuration["Loggin:Username"];
        var _password = configuration["Loggin:Password"];
        var _issuer = configuration["JwtBearer:Issuer"];
        var _audience = configuration["JwtBearer:Audience"];
        var _secretKey = configuration["JwtBearer:SecretKey"];

        // Add services to the container.

        // Taking care of cicle references
        builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Record Emissions API",
                    Description = "Api to Perform CRUD Operations and Record Company Emissions in a Mysql Database",
                    Version = "v1"
                });

            c.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Autorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Ingrese su info..."
                });

            c.AddSecurityRequirement(
                new OpenApiSecurityRequirement {
                {
                  new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
                }
            });
        });

        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseMySql(builder.Configuration.GetConnectionString("localConnection"), new MySqlServerVersion(new Version(8, 0, 35)));
        });

        builder.Services.AddTransient<SeedDB>();

        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));

        builder.Services.AddScoped<ICompaniesRepository, CompaniesRepository>();
        builder.Services.AddScoped<ICompaniesUnitOfWork, CompaniesUnitOfWork>();

        builder.Services.AddScoped<IEmissionsRepository, EmissionsRepository>();
        builder.Services.AddScoped<IEmissionsUnitOfWork, EmissionsUnitOfWork>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            // Stablish token parameters
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,                  // configuration["JwtBearer:Issuer"],
                ValidAudience = _audience,               //configuration["JwtBearer:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtBearer:SecretKey"]!)),
                ClockSkew = TimeSpan.Zero
            };

            // JwtBearer event conf
            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    // Evita el envío del header WWW-Authenticate
                    context.HandleResponse();

                    // Personaliza la respuesta
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";

                    // Escribe el contenido de la respuesta JSON personalizada
                    var result = JsonSerializer.Serialize(new { error = "Usted no está autorizado para acceder a este recurso." });
                    return context.Response.WriteAsync(result);
                }
            };
        });

        var app = builder.Build();


        // Populating DB
        SeedData(app);

        void SeedData(WebApplication app)
        {
            var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory!.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<SeedDB>();
                service!.SeedAsync().Wait();
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {

            app.Use(async (context, next) =>
            {

                string authHeader = context.Request.Headers["Authorization"]!;

                if (authHeader != null && authHeader.StartsWith("Basic"))
                {
                    // Decode the Base64 string
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                    var username = decodedUsernamePassword.Split(':', 2)[0];
                    var password = decodedUsernamePassword.Split(':', 2)[1];

                    if (username == _userName && password == _password)
                    {
                        await next();
                        return;
                    }
                }

                if (authHeader != null && authHeader.StartsWith("Bearer"))
                {
                    var token = authHeader.Substring("Bearer ".Length).Trim();

                    // Configura las opciones de validación de tokens
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey!)), //_secretKey es tu clave secreta para firmar el token
                        ValidateIssuer = true,
                        ValidIssuer = _issuer, // El emisor del token
                        ValidateAudience = true,
                        ValidAudience = _audience, // La audiencia del token
                        ValidateLifetime = true, // Valida la expiración
                        ClockSkew = TimeSpan.FromDays(1) // La tolerancia de tiempo para la expiración del token
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();

                    try
                    {
                        // Intenta validar el token
                        var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                        // Si llega aquí, el token es válido
                        await next();
                        return;
                    }
                    catch (SecurityTokenException)
                    {
                        // El token no es válido
                        context.Response.StatusCode = 403; // el servidor ha entendido la solicitud pero se niega a authorizarla Forbidden o puedes usar 401 Unauthorized
                    }
                    catch (Exception)
                    {
                        // Otro tipo de error
                        context.Response.StatusCode = 500; // Internal Server Error
                    }
                }

                // Return authentication type (causes browser to show login dialog)        
                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = 401; //Unauthorized
                return;

            });

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseCors(
            x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials()
        );

        app.Run();
    }
}