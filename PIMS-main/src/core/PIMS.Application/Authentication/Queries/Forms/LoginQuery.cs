using ErrorOr;
using MediatR;
using PIMS.Application.Authentication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Authentication.Queries.Forms
{
    /// <summary>
    /// Команда запроса на вход.
    /// </summary>
    public class LoginQuery:IRequest<ErrorOr<AuthenticationResult>>
    {
        /// <summary>
        /// Свойство имя пользователя должно быть передано или установлено при создании экземпляра объекта.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Свойство пароль должно быть передано или установлено при создании экземпляра объекта.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
