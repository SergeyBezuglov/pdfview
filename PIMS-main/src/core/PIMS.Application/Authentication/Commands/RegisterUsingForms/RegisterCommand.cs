using ErrorOr;
using MediatR;
using PIMS.Application.Authentication.Common;
using PIMS.Domain.Common.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Authentication.Commands.RegisterUsingForms
{
    /// <summary>
    /// Команда регистрации пользователя на основе форм.
    /// </summary>
    public class RegisterCommand: UserCreateDataWithPassword,IRequest<ErrorOr<AuthenticationResult>>
    {
        /// <summary>
        /// Используем сведения, ранее полученные из форм <see cref="RegisterCommand"/> .
        /// </summary>
        /// <param name="firstName">Имя.</param>
        /// <param name="middleName">Отчество.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="email">Почта email.</param>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        public RegisterCommand(string firstName, string middleName, string lastName, string email, string userName, string password) :
           base(firstName, middleName, lastName, email, userName, password)
        {
           
        }
    }
}
