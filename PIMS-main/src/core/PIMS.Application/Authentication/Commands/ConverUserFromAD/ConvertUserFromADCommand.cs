using ErrorOr;
using MediatR;
using PIMS.Application.Authentication.Common;
using PIMS.Domain.Common.Models.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Authentication.Commands.ConverUserFromAD
{
    /// <summary>
    /// Команда на создание учетной записи Пользователя PIMS на основе учетной записи Active Directory
    /// </summary>
    public class ConvertUserFromADCommand: IRequest<ErrorOr<ActiveDirectoryUser>>
    {
        /// <summary>
        ///  Используем IIdentity, полученную из провайдера аутентификации для связи с AD, список данных о пользователе из провайдера <see cref="ConvertUserFromADCommand"/> .
        /// </summary>
        /// <param name="identity">Личности.</param>
        /// <param name="claims">Претензии.</param>
        public ConvertUserFromADCommand(IIdentity? identity, IEnumerable<Claim> claims) {
            Identity = identity;
            Claims = claims;
        }
        /// <summary>
        /// Сведения из провайдера об учетной записи
        /// </summary>
        public IIdentity? Identity { get; private set; } = null;
        /// <summary>
        /// Список данных о пользователе из провайдера
        /// </summary>
        public IEnumerable<Claim> Claims { get; private set; }
    }
}
