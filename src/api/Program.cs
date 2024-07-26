using log4net.Config;
using log4net;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text;
using api.Utility;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using api.Contexts;
using api.Repositories.Interfaces;
using api.Repositories;
using api.Services.Interfaces;
using api.Services;
using api.Models;
using API.Services;
using api.Exceptions;

namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Log4NetConfig


            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));
            

            
            #endregion

            #region Builder Configuration

            var builder = WebApplication.CreateBuilder(args);

            Env.Load();

            #region Base Configuration

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddLogging(l => l.AddLog4Net());

            builder.Services.AddEndpointsApiExplorer();

            #endregion

            #region Swagger

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "web server api", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
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
                        new string[] {}
                    }
                });
            });
            #endregion

            #region JWT Auth/Authorization

            var userSecret = Environment.GetEnvironmentVariable("JWT_USER_SECRET") ?? "temp";
            
            builder.Services.AddAuthentication()
                .AddJwtBearer("UserScheme", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(userSecret))
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("UserPolicy", policy =>
                {
                    policy.AuthenticationSchemes.Add("UserScheme");
                    policy.RequireAuthenticatedUser();
                });
            });

            #endregion

            #region DBContext
            var connectionString = Environment.GetEnvironmentVariable("SQL_SERVER_CONNECTION_STRING");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new EnvironmentVariableUndefinedException("SQL_SERVER_CONNECTION_STRING");
            }
            builder.Services.AddDbContext<DbSql>(options => options.UseSqlServer(connectionString));
            #endregion

            #region Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserAuthRepository, UserAuthRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
            builder.Services.AddScoped<IAzureMailService, AzureMailService>();
            builder.Services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
            builder.Services.AddScoped<ITokenService<User>, UserTokenService>();
            builder.Services.AddScoped<IUserAuthService, UserAuthService>();

            builder.Services.AddScoped<IUserService, UserService>();
            #endregion

            #region CORS
            builder.Services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(option =>
                {
                    option.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            #endregion

            #endregion

            #region App Configuration

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
