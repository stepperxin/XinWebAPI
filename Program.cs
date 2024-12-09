using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using XinWebAPI.Data.XinIdentity;
using XinWebAPI.Models.XinIdentity;

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

var connectionString = builder.Configuration.GetConnectionString(builder.Configuration["CurrentDB"]);
builder.Services.AddDbContextPool<XinIdentityDBContext>(options => {
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// XinIdentity
builder.Services.AddIdentityApiEndpoints<XinUser>()
    .AddEntityFrameworkStores<XinIdentityDBContext>();

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
