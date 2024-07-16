using System;
using System.Reflection;
using Auth0.AuthenticationApi;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Serilog;
using Swashbuckle.NodaTime.AspNetCore;
using Training.API.Users.Database;
using Training.API.Users.Services.Abstraction;
using Training.API.Users.Strategy;
using Training.Common.Configuration;
using Training.Common.Utils;

namespace Training.API.Users;

public class Startup
{
    private const string CorsPolicyName = nameof(CorsPolicyName);

    public Startup(IConfiguration configuration, IHostEnvironment environment, IWebHostEnvironment webHost)
    {
        this.Configuration = configuration;
        this.Logger = Log.Logger.ForContext<Startup>();
    }

    public Serilog.ILogger Logger { get; }
    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        this.Logger.Information("Configuring services...");

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = { new StringEnumConverter() },
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
        }.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

        services.AddCors(options => options.AddPolicy(CorsPolicyName, this.Configuration.GetSectionOrDefault<CorsPolicy>()));
        services.AddControllers(options =>
        {
            options.Conventions.Add(new ControllerDocumentationConvention());
        })
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            options.SerializerSettings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        });

        services.AddApiVersioning(o => o.ReportApiVersions = true);
        services.AddVersionedApiExplorer(options =>
        {
            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";
            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        });
        services.AddAuthorization(o =>
        {
            o.DefaultPolicy = new AuthorizationPolicyBuilder()
               .RequireAuthenticatedUser()
               .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
               .Build();
        })
            .AddAuthentication(options => this.Configuration.Bind(Constants.AuthenticationSection.DefaultConfig, options))
            .AddJwtBearer(options => this.Configuration.Bind(Constants.AuthenticationSection.JwtBearer, options));
        // .AddJwtBearer(InternalPolicyName.ServiceInternal, options =>
        // {
        //     options.TokenValidationParameters = this.Configuration.GetSectionOrDefault<AuthorizationConfiguration>(Constants.AuthenticationSection.InternalServiceJwtBearer).GetTokenValidationParameters();
        // });

        services.AddHealthChecks();

        this.RegisterSwagger(services);
        this.RegisterDatabases(services);
        this.RegisterWorkers(services);
        this.RegisterExternalApis(services);
        this.RegisterServicesInternal(services);
        this.RegisterStrategys(services);
        this.RegisterMappings(services);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        this.Logger.Information("Configuring app...");

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        this.ConfigureSwagger(app);

        app.UseCors(CorsPolicyName);
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        this.ConfigureDatabase(app);
    }

    private void RegisterSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.ConfigureForNodaTime();
            c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

            c.UseOneOfForPolymorphism();
            c.UseAllOfForInheritance();

            c.SwaggerDoc("all", new OpenApiInfo { Title = "Users API", Version = "all" });
            c.AddSecurityDefinition("BearerJWT", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme.",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "BearerJWT" },
                        },
                        Array.Empty<string>()
                    },
                });
            c.DocInclusionPredicate(SwashbuckleHelper.DocInclusionStrategy.Flexible);
        });

        services.AddSwaggerGenNewtonsoftSupport();
    }

    private void RegisterMappings(IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }

    private void RegisterServicesInternal(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        var auth0Client = this.Configuration.GetSectionOrDefault<OpenApiClientConfiguration>("Authentication:Auth0Client");
        services.AddScoped<IAuthenticationApiClient>(_ => new AuthenticationApiClient(new Uri(auth0Client.BasePath)));
        services.AddScoped<IAuthenticationDetailsProvider, AuthenticationDetailsProvider>();
    }

    private void RegisterStrategys(IServiceCollection services)
    {
        // Validator
        services.AddScoped<UsersValidator>();
        services.AddScoped<RolesValidator>();
        services.AddScoped<GroupsValidator>();

        // Strategy-Users
        services.AddScoped<CreateUserStrategy>();
        services.AddScoped<GetAllUserStrategy>();
        services.AddScoped<GetUserStrategy>();
        services.AddScoped<UpdateUserStrategy>();


        // Strategy-Roles
        services.AddScoped<CreateRoleStrategy>();
        services.AddScoped<GetAllRoleStrategy>();
        services.AddScoped<DeleteRoleStrategy>();
        services.AddScoped<UpdateRoleStrategy>();


        // Strategy-Groups
        services.AddScoped<CreateGroupStrategy>();
        services.AddScoped<GetAllGroupStrategy>();
        services.AddScoped<AddToGroupStrategy>();
        services.AddScoped<RemoveFromGroupStrategy>();
        services.AddScoped<GetGroupStrategy>();
        services.AddScoped<UpdateGroupStrategy>();


        // Strategy-Permissions
        services.AddScoped<CreatePermissionStrategy>();
        services.AddScoped<DeletePermissionStrategy>();
    }

    private void RegisterWorkers(IServiceCollection services)
    {

    }

    private void RegisterDatabases(IServiceCollection services)
    {
        this.Logger.Information("Configuring Training Users DB...");

        var trainingDbConfig = this.Configuration.GetSection<PostgresDbConfiguration>(Constants.DatabaseSection.Training);
        if (trainingDbConfig == default) throw new Exception($"Cannot load Training DB configuration, has you defined the configuration under: '{Constants.DatabaseSection.Training}'");

        this.Logger.Information("Loaded Collections DB configuration {@TrainingDbConfig}", new
        {
            HasConnectionString = !string.IsNullOrWhiteSpace(trainingDbConfig.ConnectionString),
            trainingDbConfig.DefaultDatabase,
            trainingDbConfig.EnableAutomaticMigration,
            trainingDbConfig.PostgresApiVersion,
        });

        services.AddDbContext<TrainingUsersDbContext>(
            builder =>
            {
                builder.UseNpgsql(trainingDbConfig.ConnectionString, config =>
                {
                    config.SetPostgresVersion(trainingDbConfig.PostgresApiVersion);
                    config.MigrationsAssembly(typeof(TrainingUsersDbContext).Namespace);
                    config.MigrationsHistoryTable(TrainingUsersDatabaseConstants.MigrationsHistoryTableName, TrainingUsersDatabaseConstants.MigrationsHistoryTableSchema);
                    config.UseNetTopologySuite();
                    config.UseNodaTime();
                });
            },
            ServiceLifetime.Scoped,
            ServiceLifetime.Singleton
        );
    }

    private void RegisterExternalApis(IServiceCollection services)
    {

    }

    private void ConfigureSwagger(IApplicationBuilder app)
    {
        var config = this.Configuration.GetSectionOrDefault<SwaggerConfiguration>(Constants.SwaggerConfigSection.Root);

        if (config.Enable)
        {
            this.Logger.Information("Enabling swagger documentation");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/all/swagger.json", "Users API");
            });
        }

        if (config.Enable && config.AutoRedirect)
        {
            this.Logger.Information("Enabling swagger documentation auto-redirect");

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);
        }
    }

    private void ConfigureDatabase(IApplicationBuilder app)
    {
        DatabaseHelper.MigrateDatabase<TrainingUsersDbContext>(app.ApplicationServices, this.Configuration.GetSection<PostgresDbConfiguration>(Constants.DatabaseSection.Training));
    }
}