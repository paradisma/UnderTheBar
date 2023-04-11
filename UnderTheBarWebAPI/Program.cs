using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MySql.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using UnderTheBarWebAPI.Entities;

// https://www.c-sharpcorner.com/article/rest-api-with-asp-net-6-and-mysql/
// https://github.com/esdanielgomez/ASP.NET6_WebAPI/blob/main/ASP.NET_WebAPI6/Controllers/UserController.cs

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddEntityFrameworkMySQL()
    .AddDbContext<UserContext>(options =>
    {
        options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
    })
    .AddDbContext<WorkoutsContext>(options =>
    {
        options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200");
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
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
