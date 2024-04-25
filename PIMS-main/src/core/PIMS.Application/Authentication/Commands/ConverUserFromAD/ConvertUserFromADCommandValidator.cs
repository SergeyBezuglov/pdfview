using FluentValidation;
using PIMS.Application.Authentication.Commands.RegisterUsingForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Authentication.Commands.ConverUserFromAD
{
    /// <summary>
    ///  Проверка для команды на создание учетной записи Пользователя PIMS на основе учетной записи Active Directory
    /// </summary>
    public class ConvertUserFromADCommandValidator : AbstractValidator<ConvertUserFromADCommand>
    {
        /// <summary>
        /// Проверяем, чтобы объект Identity не был нулевым в команде
        /// </summary>
        public ConvertUserFromADCommandValidator()
        {
            RuleFor(m => m.Identity).NotNull().NotEmpty();
            
        }
    }
}
