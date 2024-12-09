using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using XinWebAPI.Data.XinIdentity;
using XinWebAPI.Models.XinIdentity;

var builder = WebApplication.CreateBuilder(args);

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

//builder.Services.AddIdentityCore<IdentityUser>(options => {
//                    options.SignIn.RequireConfirmedAccount = false;
//                    options.User.RequireUniqueEmail = true;
//                    options.Password.RequireDigit = false;
//                    options.Password.RequiredLength = 6;
//                    options.Password.RequireNonAlphanumeric = false;
//                    options.Password.RequireUppercase = false;
//                    options.Password.RequireLowercase = false;
//                })
//                .AddRoles<IdentityRole>()
//                .AddRoleManager<RoleManager<IdentityRole>>()
//                .AddEntityFrameworkStores<XinIdentityDBContext>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(options =>
//                {
//                    options.TokenValidationParameters = new TokenValidationParameters()
//                    {
//                        ValidateIssuer = true,
//                        ValidateAudience = true,
//                        ValidateLifetime = true,
//                        ValidateIssuerSigningKey = true,
//                        ValidAudience = builder.Configuration["XinIdentity:Jwt:Audience"],
//                        ValidIssuer = builder.Configuration["XinIdentity:Jwt:Issuer"],
//                        IssuerSigningKey = new SymmetricSecurityKey(
//                            Encoding.UTF8.GetBytes(builder.Configuration["XinIdentity:Jwt:Key"])
//                        )
//                    };
//                })
//                .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(
//                    "ApiKey",
//                    options => { }
//                );
//builder.Services.AddScoped<JwtService>();
//builder.Services.AddScoped<ApiKeyService>();

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
