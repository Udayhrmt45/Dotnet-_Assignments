using CollegeManagement.API.Middleware;
using CollegeManagement.Common.Utilities;
using CollegeManagement.Data.DbHelpers;
using CollegeManagement.Service.Abstractions;
using CollegeManagement.Service.Implementations;
using CollegeManagement.Store.Abstractions;
using CollegeManagement.Store.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;  

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var response = new
            {
                Success = false,
                Message = "Validation Failed",
                Errors = errors
            };

            return new BadRequestObjectResult(response);
        };
    });

// Swagger Services
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "College Management API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",

        Type = SecuritySchemeType.Http,

        Scheme = "bearer",

        BearerFormat = "JWT",

        In = ParameterLocation.Header,

        Description =
            "Enter JWT Token like this: Bearer your_token"
    });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
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
});

// Dependency Injection
builder.Services.AddScoped<DbHelper>();

builder.Services.AddScoped<IStudentStore, StudentStore>();

builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddScoped<IAuthStore, AuthStore>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<JwtTokenGenerator>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,

            ValidateAudience = true,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,

            ValidIssuer =
                builder.Configuration["JwtSettings:Issuer"],

            ValidAudience =
                builder.Configuration["JwtSettings:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["JwtSettings:Key"]
                    )),

            ClockSkew = TimeSpan.Zero
        };
});

var app = builder.Build();

// Configure Swagger Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();