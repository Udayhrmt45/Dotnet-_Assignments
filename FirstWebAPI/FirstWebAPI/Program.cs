using Microsoft.OpenApi;
using WebAPI.service.Implementation;
using WebAPI.service.Abstraction;
using WebAPI.store.Implementation;
using WebAPI.store.Abstraction;
using WebAPI.data.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IEmployeeStore, EmployeeStore>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<DbConnectionFactory>();

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FirstWebAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FirstWebAPI v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();