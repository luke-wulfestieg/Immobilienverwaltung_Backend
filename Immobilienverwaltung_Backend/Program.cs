using Immobilienverwaltung_Backend.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DEVELOP", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

try
{
    dbContext.Database.OpenConnection(); // Tries to open the connection
    dbContext.Database.CloseConnection();
    Console.WriteLine("✅ Database connection successful.");
}
catch (SqlException ex)
{
    Console.WriteLine("❌ Database connection failed:");
    Console.WriteLine(ex.Message);
    // Optional: throw here if you want to stop app startup
    // throw;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DEVELOP");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
