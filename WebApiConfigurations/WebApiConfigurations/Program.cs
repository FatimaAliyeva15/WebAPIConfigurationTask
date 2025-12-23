

using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using WebApiConfigurations.DAL.EFCore;
using WebApiConfigurations.DTOs.CategoryDTOs;
using WebApiConfigurations.Validators.CategoryValidators;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddFluentValidation(opt =>
{

    opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    opt.ImplicitlyValidateChildProperties = true;
    opt.ImplicitlyValidateRootCollectionElements = true;

});

builder.Services.AddAutoMapper(typeof(Program).Assembly);



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; 
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();