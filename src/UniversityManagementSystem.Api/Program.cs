using UniversityManagementSystem.API.StratupExtension;
using UniversityManagementSystem.DLL;
using UniversityManagementSystem.BLL;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using UniversityManagementSystem.DLL.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
        opt => { opt.Filters.Add(new ProducesAttribute("application/json")); })
    .AddJsonOptions(opt=> opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtensionHelper();
builder.Services.AddDatabaseExtensionHelper(builder.Configuration);
builder.Services.AddBLLDependancies(); //this is for bll dependencies
builder.Services.AddDLLDependancies(); //this is for dll dependencies
builder.Services.AddIdentityCustomExtensionHelper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.RunMigration();
}

//app.MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();

app.MapControllers();

app.UseOwnApplicationAuthentication();

app.Run();
