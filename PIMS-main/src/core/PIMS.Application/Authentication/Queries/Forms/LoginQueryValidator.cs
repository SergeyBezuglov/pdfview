using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Authentication.Queries.Forms
{
    /// <summary>
    ///  Проверка для команды запроса на вход.
    /// </summary>
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        /// <summary>
        /// Проверяем, чтобы объекты UserName и Password не были нулевыми в команде
        /// </summary>
        public LoginQueryValidator() { 
            RuleFor(m=>m.UserName).NotEmpty();
            RuleFor(m => m.Password).NotEmpty();
        }
    }
}
