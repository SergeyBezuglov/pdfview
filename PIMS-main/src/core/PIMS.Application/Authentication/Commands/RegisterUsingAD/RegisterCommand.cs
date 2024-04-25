using ErrorOr;
using MediatR;
using PIMS.Application.Authentication.Common;
using PIMS.Domain.Common.Models.ActiveDirectory;
using PIMS.Domain.Common.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Authentication.Commands.RegisterUsingAD
{
    /// <summary>
    /// Команда регистрации пользователя на основе данных из Active Directory
    /// </summary>
    public class RegisterCommand: IRequest<ErrorOr<AuthenticationResult>>
    {

        /// <summary>
        /// Используем сведения, ранее полученные из Active Directory  <see cref="RegisterCommand"/>.
        /// </summary>
        /// <param name="user">Сведения из Active Directory</param>
        public RegisterCommand(ActiveDirectoryUser user)
        {
            User = user;
        }
        /// <summary>
        /// Получает или задает пользователя.
        /// </summary>
        /// <value>Пользователь ActiveDirectoryUser.</value>
        public ActiveDirectoryUser User { get; set; }
    }
}
