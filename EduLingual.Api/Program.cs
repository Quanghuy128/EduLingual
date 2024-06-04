using EduLingual.Api.Configuration;
using EduLingual.Api.Middlewares;
using EduLingual.Domain.Common;
using Microsoft.Extensions.Configuration;
using Net.payOS;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration.Get<AppConfig>() ?? new AppConfig();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //PayOs
    PayOS payos = new PayOS(AppConfig.PayOSSetting.PAYOS_CLIENT_ID,
                AppConfig.PayOSSetting.PAYOS_API_KEY,
                AppConfig.PayOSSetting.PAYOS_CHECKSUM_KEY);
    builder.Services.AddSingleton(payos);
    builder.Services.AddSingleton(configuration);

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

    builder.Services.AddHttpContextAccessor();
    // Config builder
    builder.ConfigureAutofacContainer();

    // Add Configuration
    builder.Configuration.SettingsBinding();

    builder.Services.AddSwaggerGenOption();
    builder.Services.AddDbContext();
    //builder.Services.AddResponseCompression();

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
