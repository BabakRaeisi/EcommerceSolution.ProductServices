using BusinessLogicLayer;
using DataAccessLayer;
using Ecommerce.ProductServices.API.Middleware;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using ProductServices.API.APIEndpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
// DAL and BAL services

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessAccessLayer();

builder.Services.AddControllers();
//fluent validation

builder.Services.AddFluentValidationAutoValidation();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapProductAPIEndpoints();
app.Run();
