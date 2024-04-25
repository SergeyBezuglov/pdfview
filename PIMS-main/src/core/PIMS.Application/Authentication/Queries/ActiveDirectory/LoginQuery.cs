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

namespace PIMS.Application.Authentication.Queries.ActiveDirectory
{
    /// <summary>
    /// Команда запроса на вход.
    /// </summary>
    public class LoginQuery : IRequest<ErrorOr<AuthenticationResult>>
    {
        /// <summary>
        /// Используем сведения, полученные из запроса на вход <see cref="LoginQuery"/> .
        /// </summary>
        /// <param name="user">Пользователь.</param>
        public LoginQuery(ActiveDirectoryUser user) {
            User = user;
        }
        /// <summary>
        /// Получает или задает пользователя.
        /// </summary>
        /// <value>Пользователь ActiveDirectoryUser.</value>
        public ActiveDirectoryUser User { get; set; }
    }
}
