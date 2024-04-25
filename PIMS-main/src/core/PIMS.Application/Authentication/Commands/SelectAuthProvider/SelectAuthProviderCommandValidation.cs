using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Authentication.Commands.SelectAuthProvider
{
    /// <summary>
    /// Проверка для команды выбора провайдера аутентификации.
    /// </summary>
    public class SelectAuthProviderCommandValidation : AbstractValidator<SelectAuthProviderCommand>
    {
        /// <summary>
        /// Проверяем, чтобы объект AuthenticationModules не был нулевым в команде
        /// </summary>
        public SelectAuthProviderCommandValidation()
        {
            RuleFor(m => m.AuthenticationModules).NotEmpty();            
        }
    }
}
