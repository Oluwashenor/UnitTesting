using Microsoft.EntityFrameworkCore;
using Library.API.Data;
using Library.API.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibraryAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryAPIContext") ?? throw new InvalidOperationException("Connection string 'LibraryAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
