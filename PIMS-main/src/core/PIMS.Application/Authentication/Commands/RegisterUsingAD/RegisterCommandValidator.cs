using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Authentication.Commands.RegisterUsingAD
{
    /// <summary>
    /// Проверка для команды регистрации пользователя на основе данных из Active Directory
    /// </summary>
    /// </summary>
    public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        /// <summary>
        ///  Проверяем, чтобы экземпляр User не был null, были заполнены поля ФИО
        /// </summary>
        public RegisterCommandValidator()
        {
            RuleFor(m => m.User).NotNull();
            RuleFor(m => m.User.FirstName).NotEmpty();
            RuleFor(m => m.User.LastName).NotEmpty();
            RuleFor(m => m.User.UserName).NotEmpty();            
        }
    }
}
