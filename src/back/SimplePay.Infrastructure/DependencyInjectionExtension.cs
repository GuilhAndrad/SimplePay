using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimplePay.Domain.ExternalServices;
using SimplePay.Domain.Repositories;
using SimplePay.Domain.Repositories.Transfer;
using SimplePay.Domain.Repositories.User;
using SimplePay.Domain.Repositories.Wallet;
using SimplePay.Domain.Security;
using SimplePay.Infrastructure.DataAccess;
using SimplePay.Infrastructure.DataAccess.Repositories;
using SimplePay.Infrastructure.ExternalServices;
using SimplePay.Infrastructure.Security;

namespace SimplePay.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddFluentMigrator(services, configuration);
        AddRepositories(services);
        AddExternalServices(services);

        services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");

        services.AddDbContext<SimplePayDbContext>(options =>
            options.UseMySQL(connectionString!));
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");

        services.AddFluentMigratorCore()
            .ConfigureRunner(runner => runner
                .AddMySql5()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(DependencyInjectionExtension).Assembly).For.Migrations())
            .AddLogging(logging => logging.AddFluentMigratorConsole());
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserReader, UserRepository>();
        services.AddScoped<IUserWriter, UserRepository>();
        services.AddScoped<IWalletReader, WalletRepository>();
        services.AddScoped<IWalletWriter, WalletRepository>();
        services.AddScoped<ITransferWriter, TransferRepository>();
    }

    private static void AddExternalServices(IServiceCollection services)
    {
        services.AddHttpClient<IAuthorizerService, AuthorizerService>()
            .AddStandardResilienceHandler();

        services.AddHttpClient<INotificationService, NotificationService>()
            .AddStandardResilienceHandler();
    }
}