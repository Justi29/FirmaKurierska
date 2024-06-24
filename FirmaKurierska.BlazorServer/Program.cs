using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using FirmaKurierska.BlazorServer.Data;
using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Infrastructure;
using FirmaKurierska.Application;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FirmaKurierska.Application.Mappings;
using FirmaKurierska.Infrastructure.Repositories;
using FirmaKurierska.Application.Services;
using FirmaKurierska.SharedKernel.Dto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();


// rejestracja automappera w kontenerze IoC
builder.Services.AddAutoMapper(typeof(FirmaKurierskaMappingProfile));
// rejestracja automatycznej walidacji (FluentValidation waliduje i przekazuje wynik przez ModelState)
builder.Services.AddFluentValidation();
builder.Services.AddFluentValidationAutoValidation();
// rejestracja kontekstu bazy w kontenerze IoC
var sqliteConnectionString = "Data Source=FirmaKurierska.WebAPI.db";
builder.Services.AddDbContext<KioskDbContext>(options =>
 options.UseSqlite(sqliteConnectionString));
// rejestracja walidatora
//builder.Services.AddScoped<IValidator<UpdateAddressDto>>();
builder.Services.AddScoped<IKioskUnitOfWork, KioskUnitOfWork>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ICourierRepository, CourierRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ICourierService, CourierService>();
builder.Services.AddScoped<IOrderService, OrderService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// seeding data
using (var scope = app.Services.CreateScope())
{
    var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    dataSeeder.Seed();
}

app.Run();

