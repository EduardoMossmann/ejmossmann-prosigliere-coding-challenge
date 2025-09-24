using BloggingPlatform.Application.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using BloggingPlatform.Infrastructure.Data;
using BloggingPlatform.Infrastructure.Data.Extensions;
using BloggingPlatform.Application.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BloggingPlatform.Domain.Roles;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilderContext, serviceProvider) => serviceProvider
    .WriteTo.Console()
    .ReadFrom.Configuration(hostBuilderContext.Configuration));

builder.Services.AddDbContext<BloggingPlatformDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    if (builder.Environment.IsDevelopment())
    {
        opts.EnableSensitiveDataLogging();
    }
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
        };
    });


builder.Services.AddAuthorization(x =>
{
    x.AddPolicy(IdentityUserAccessRoles.USER, policy => policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, IdentityUserAccessRoles.USER));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddRepositories();
builder.Services.AddAutoMapperProfiles();
builder.Services.AddValidators();
builder.Services.AddApplicationServices();
builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BloggingPlatform.Api" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Authorization token using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
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
            Array.Empty<string>()
        }
    });

    var basePath = AppContext.BaseDirectory;
    foreach (var file in Directory.EnumerateFiles(basePath, "*.xml", SearchOption.AllDirectories))
    {
        c.IncludeXmlComments(file);
    }
    c.DocInclusionPredicate((name, api) => true);
});

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BloggingPlatform.Api"));
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
