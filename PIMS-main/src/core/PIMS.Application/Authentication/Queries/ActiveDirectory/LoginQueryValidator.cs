using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Authentication.Queries.ActiveDirectory
{
    /// <summary>
    /// Проверка для команды запроса на вход.
    /// </summary>
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        /// <summary>
        /// Проверяем, чтобы объекты User и User.UserName не были нулевыми в команде
        /// </summary>
        public LoginQueryValidator() {
            RuleFor(m => m.User).NotNull();
            RuleFor(m => m.User.UserName).NotEmpty();
        }
    }
}
