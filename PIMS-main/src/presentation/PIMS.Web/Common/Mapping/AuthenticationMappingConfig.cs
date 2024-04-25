using Mapster;
using PIMS.Application.Authentication.Commands.RegisterUsingForms;
using PIMS.Application.Authentication.Common;
using PIMS.Application.Authentication.Queries.Forms;
using PIMS.Contracts.Authentication;
using PIMS.Domain.UserAggregate;

namespace PIMS.Web.Common.Mapping
{
    /// <summary>
    /// Конфигурация сопоставления аутентификации.
    /// </summary>
    public class AuthenticationMappingConfig : IRegister
    {
        /// <summary>
        /// Задача (TODO): Добавить сводку
        /// </summary>
        /// <param name="config">Конфигурация.</param>
        public void Register(TypeAdapterConfig config)
        {

            config.NewConfig<RegisterRequest, RegisterCommand> ();
            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>().
                Map(dest => dest.Token, src => src.Token).
                Map(dest => dest.Id, src => src.User!.GetId).
                Map(dest => dest.Email, src => src.UserData!.Email.WorkEmail ?? "").
                Map(dest => dest.UserName, src => src.User!.UserName).
                Map(dest => dest.FirstName, src => src.UserData!.FirstName).
                Map(dest => dest.MiddleName, src => src.UserData!.MiddleName).
                Map(dest => dest.LastName, src => src.UserData!.LastName);
            


         ;
        }
    }
}
