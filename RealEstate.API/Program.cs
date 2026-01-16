using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Business.Configs;
using RealEstate.Data;
using RealEstate.Data.Abstract;
using RealEstate.Data.Concrete;
using RealEstate.Entity.Concrete;
using FluentValidation.AspNetCore;
using RealEstate.Business.Validators;
using AutoMapper;
using RealEstate.Business.Mapping;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'PostgreSqlConnection' bulunamadı!");
}

builder.Services.AddDbContext<RealEstateDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//Add services

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.Configure<AppUrlConfig>(builder.Configuration.GetSection("AppUrlConfig"));
builder.Services.Configure<CorsConfig>(builder.Configuration.GetSection("CorsSettings"));


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<PropertyCreateDtoValidator>();

var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();

//Add identity authentication

var corsConfig = builder.Configuration.GetSection("CorsSettings").Get<CorsConfig>();
//Add CORS


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowedSpesificOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
