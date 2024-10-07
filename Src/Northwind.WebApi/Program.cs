using Northwind.DataContext.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddControllers();
builder.Services.AddNorthwindContext();
var app = builder.Build();

// Configure the HTTP request pipeline.

// Enable Cors
app.UseCors("MyPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
