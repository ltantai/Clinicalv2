using Clinical.Api.Extensions;
using Clinical.Application.Interfaces.GenericDataService;
using Clinical.Application.Mappers;
using Clinical.Infrastructure.GenericDataService;
using Clinical.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// EF Core setup
builder.Services.AddDbContext<ClinicalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
ServiceRegistration.RegisterServices(builder.Services);
builder.Services.AddScoped<IGenericDataService, GenericDataService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("app-cors", policy =>
    {
        policy.AllowAnyOrigin()   // Allows any origin to access the resources
              .AllowAnyHeader()  // Allows any headers in the request
              .AllowAnyMethod(); // Allows any HTTP method (GET, POST, etc.)
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
// Use CORS middleware
app.UseCors("app-cors");
app.MapControllers();

app.Run();
