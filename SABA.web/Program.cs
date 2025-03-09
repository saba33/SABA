using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SABA.Persistance.DataContext;
using SABA.web.ExtensionMethods;
using SABA.web.MIddlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));
builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and your token",
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

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

//#pragma warning disable CS0618
//Log.Logger = new LoggerConfiguration()
//      .MinimumLevel.Verbose()
//      .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//      .Enrich.FromLogContext()
//      .WriteTo.MSSqlServer(
//          connectionString: builder.Configuration.GetConnectionString("connectionString"),
//          tableName: "Logs",
//          autoCreateSqlTable: true,
//          columnOptions: new ColumnOptions(),
//          restrictedToMinimumLevel: LogEventLevel.Verbose)
//      .CreateLogger();


var app = builder.Build();
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
