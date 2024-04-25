using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Authentication.Commands.RegisterUsingForms
{
    /// <summary>
    /// Проверка для команды регистрации пользователя на основе форм.
    /// </summary>
    public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        /// <summary>
        /// Проверяем, чтобы экземпляр User не был null, были заполнены поля ФИО
        /// </summary>
        public RegisterCommandValidator()
        {
            RuleFor(m => m.FirstName).NotEmpty();
            RuleFor(m => m.MiddleName).NotEmpty();
            RuleFor(m => m.LastName).NotEmpty();
            RuleFor(m => m.UserName).NotEmpty();
            RuleFor(m => m.Password).NotEmpty();
        }
    }
}
