using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using ZooAPI.Datas;
using ZooAPI.Helpers;
using ZooAPI.Repositories;
using ZooCore.Models;

namespace ZooAPI.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void InjectDependancies(this WebApplicationBuilder builder)
        {

            builder.Services.AddControllers();

            builder.AddSwagger();

            builder.AddDatabase();

            builder.AddRepositories();

            builder.AddCors();

            builder.AddAuthentication();

            // ajouter le service IMapper de AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ZooApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.Http
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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
        }

        private static void AddDatabase(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        private static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRepository<Animal>, AnimalRepository>();
            builder.Services.AddScoped<IRepository<User>, UserRepository>();
        }

        private static void AddCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("allConnections", options =>
                {
                    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
                options.AddPolicy("angularApp", options =>
                {
                    options.WithOrigins("https://angularadress:angularport").AllowAnyMethod().AllowAnyHeader();
                });
            });
        }

        private static void AddAuthentication(this WebApplicationBuilder builder)
        {
            // on récupère la section AppSettings du fichier appsettings.json
            var appSettingsSection = builder.Configuration.GetSection(nameof(AppSettings));
            // on l'enregistre dans nos services (utile pour la partie login/register)
            builder.Services.Configure<AppSettings>(appSettingsSection); // => donne un IOption<AppSettings> dans le conteneur de dépendances
            // on récupère la section pour program.cs
            AppSettings appSettings = appSettingsSection.Get<AppSettings>();

            // on récupère la clé et on l'encode
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey!);

            // Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true, // utilisation d'une clé cryptée pour la sécurité du token
                        IssuerSigningKey = new SymmetricSecurityKey(key), // clé cryptée en elle même
                        ValidateLifetime = true, // vérification du temps d'expiration du Token
                        ValidateAudience = true, // vérification de l'audience du token
                        ValidAudience = appSettings.ValidAudience, // l'audience
                        ValidateIssuer = true, // vérification du donneur du token
                        ValidIssuer = appSettings.ValidIssuer, // le donneur
                        ClockSkew = TimeSpan.Zero // décallage possible de l'expiration du token
                    };
                });

            // Autorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.PolicyAdmin, policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, Constants.RoleAdmin);
                });
                options.AddPolicy(Constants.PolicyUser, policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, Constants.RoleUser);
                    //ClaimTypes nous permet d'avoir des valeur normalisées pour nos claims (prédéfinies par des conventions)
                    //policy.RequireClaim("EstUnDresseurPokemon", "true"); 
                    // on peut ajouter la vérification d'autres claims si nécessaire
                });
            });
        }
    }
}

