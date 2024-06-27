using EduLingual.Api.Configuration;
using EduLingual.Api.Middlewares;
using EduLingual.Domain.Common;
using Microsoft.Extensions.Configuration;
using Net.payOS;
using OfficeOpenXml;
using System.Text.Json.Serialization;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

    builder.Services.AddHttpContextAccessor();
    // Config builder
    builder.ConfigureAutofacContainer();

    // Add Configuration
    builder.Configuration.SettingsBinding();

    builder.Services.AddSwaggerGenOption();
    builder.Services.AddDbContext();
    builder.Services.RegisterStorageService();
    builder.Services.AddPayOs();
    //builder.Services.AddResponseCompression();
    builder.Services.AddMvc()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(option => option.EnablePersistAuthorization());

        await app.Services.ApplyMigration();
        //await app.Services.DbInitializer();
    }
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    //app.UseRequestDecompression();

    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
}
