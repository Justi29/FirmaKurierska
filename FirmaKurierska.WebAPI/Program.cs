using FirmaKurierska.Application.Dto;
using FirmaKurierska.Application.Services;
using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Infrastructure.Repositories;
using FirmaKurierska.Infrastructure;
using FirmaKurierska.WebAPI.Middleware;
using FirmaKurierska.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using NLog;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // rejestracja automappera w kontenerze IoC
    builder.Services.AddAutoMapper(typeof(FirmaKurierskaMappingProfile));

    // rejestracja automatycznej walidacji (FluentValidation waliduje i przekazuje wynik przez ModelState)
    builder.Services.AddFluentValidationAutoValidation();

    // rejestracja kontekstu bazy w kontenerze IoC
    var sqliteConnectionString = "Data Source=FirmaKurierska.WebAPI.db";
    builder.Services.AddDbContext<KioskDbContext>(options =>
        options.UseSqlite(sqliteConnectionString));

    builder.Services.AddScoped<IKioskUnitOfWork, KioskUnitOfWork>();
    builder.Services.AddScoped<IClientRepository, ClientRepository>();
    builder.Services.AddScoped<ICourierRepository, CourierRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<IAddressRepository, AddressRepository>();
    builder.Services.AddScoped<DataSeeder>();
    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<IClientService, ClientService>();
    builder.Services.AddScoped<ICourierService, CourierService>();
    builder.Services.AddScoped<IAddressService, AddressService>();
    builder.Services.AddScoped<ExceptionMiddleware>();

    // rejestruje w kontenerze zależności politykę CORS o nazwie FirmaKurierska,
    // która zapewnia dostęp do API z dowolnego miejsca oraz przy pomocy dowolnej metody
    builder.Services.AddCors(o => o.AddPolicy("FirmaKurierska", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    // Configure the HTTP request pipeline
    app.UseStaticFiles();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseHttpsRedirection();

    //app.UseRouting(); //commented

    app.UseAuthorization();

    //app.MapRazorPages(); //commented

    app.MapControllers();

    // wstawia politykę CORS obsługi do potoku żądania
    app.UseCors("FirmaKurierska"); // pozwala na polączenie klienta z serverem

    // seeding data
    using (var scope = app.Services.CreateScope())
    {
        var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        dataSeeder.Seed();
    }

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}











