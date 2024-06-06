using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DigitecContext>(options =>
{
    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SchoolProjectDB");
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/s washbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Call Seed method to populate the database
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var context = services.GetRequiredService<DigitecContext>();
    context.Database.EnsureCreated();
    context.Seed();
}

app.Run();
