using Microsoft.EntityFrameworkCore;
using WebAPI.Persistence.Models; // FitnessDbContext namespace

var builder = WebApplication.CreateBuilder(args);

// Controllers & Swagger (optional but handy)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----- EF Core DbContext registration -----
var cs = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<FitnessDbContext>(opts =>
    opts.UseNpgsql(cs)                // Npgsql provider
);

// -----------------------------------------

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
