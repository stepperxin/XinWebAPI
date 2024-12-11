using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using XinWebAPI.Data.PlayGround;
using XinWebAPI.Data.XinIdentity;
using XinWebAPI.Models.XinIdentity;
using XinWebAPI.Services.PlayGround;

var builder = WebApplication.CreateBuilder(args);

//Add Logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// XinIdentity (https://www.youtube.com/watch?v=tXUtSGvHMeg)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    // XinIdentity -> to show the Authorization button in Swagger
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type= SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
            },
            []
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString(builder.Configuration["CurrentDBContext"]);

if (builder.Configuration["CurrentDBContext"] == "PlayGroundDBContext")
{
    builder.Services.AddDbContextPool<PlayGroundDBContext>(options => {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });

    // XinIdentity
    builder.Services.AddIdentityApiEndpoints<XinUser>()
        .AddEntityFrameworkStores<PlayGroundDBContext>();
}
else
{
    builder.Services.AddDbContextPool<XinIdentityDBContext>(options => {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });

    // XinIdentity
    builder.Services.AddIdentityApiEndpoints<XinUser>()
        .AddEntityFrameworkStores<XinIdentityDBContext>();
}



builder.Services.AddAutoMapper(typeof(Program));

// Map PlayGround Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// XinIdentity
app.MapGroup("api/xinidentity").MapIdentityApi<XinUser>();

app.UseAuthorization();

app.MapControllers();

app.Run();
