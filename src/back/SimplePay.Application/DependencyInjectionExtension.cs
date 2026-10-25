using Microsoft.Extensions.DependencyInjection;
using SimplePay.Application.Mappings;

namespace SimplePay.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        MapsterConfiguration.Configure();

        services.AddUseCases();
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<UseCases.User.RegisterUser.IRegisterUser, UseCases.User.RegisterUser.RegisterUser>();
        services.AddScoped<UseCases.Wallet.Deposit.IDeposit, UseCases.Wallet.Deposit.Deposit>();
        services.AddScoped<UseCases.Transfer.CreateTransfer.ICreateTransfer, UseCases.Transfer.CreateTransfer.CreateTransfer>();
    }
}