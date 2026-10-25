using Mapster;
using SimplePay.Communication.Requests;
using SimplePay.Domain.Entities;

namespace SimplePay.Application.Mappings;

internal static class MapsterConfiguration
{
    internal static void Configure()
    {
        TypeAdapterConfig<RegisterUserRequest, User>
            .NewConfig()
            .Ignore(dest => dest.Password);

        TypeAdapterConfig<Domain.Enums.UserType, Communication.Enums.UserType>.NewConfig();
        TypeAdapterConfig<Communication.Enums.UserType, Domain.Enums.UserType>.NewConfig();
    }
}