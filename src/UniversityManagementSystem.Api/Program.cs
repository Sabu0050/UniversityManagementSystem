using UniversityManagementSystem.API.StratupExtension;
using UniversityManagementSystem.DLL;
using UniversityManagementSystem.BLL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabaseExtensionHelper(builder.Configuration);
builder.Services.AddBLLDependancies(); //this is for bll dependencies
builder.Services.AddDLLDependancies(); //this is for dll dependencies

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
