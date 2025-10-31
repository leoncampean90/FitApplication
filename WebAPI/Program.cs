using Microsoft.EntityFrameworkCore;
using WebAPI.Persistence.Models; // FitnessDbContext namespace

var builder = WebApplication.CreateBuilder(args);

// 1) CORS — allow your Vue origin
const string FrontendCors = "FrontendCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCors, policy =>
    {
        policy.WithOrigins("http://localhost:8082") // Vue dev server
              .AllowAnyHeader()
              .AllowAnyMethod();
        // If you send cookies or Authorization and need credentials, also add:
        // .AllowCredentials();
    });
});

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core DbContext registration
var cs = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<FitnessDbContext>(opts => opts.UseNpgsql(cs));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2) Enable CORS early in the pipeline (before auth and MapControllers)
app.UseCors(FrontendCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
