using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Errors;

public static partial class Errors
{
    /// <summary>
    /// Объявление статистических классов аутентификации.
    /// </summary>
    public static class Authentication 
    {
        /// <summary>
        /// Ошибка, возникающая, когда пользователь с указанным логином не найден. 
        /// </summary>
        public static Error NotFoundUser => Error.Validation($"{nameof(PIMS.Domain.UserAggregate.User)}.{nameof(NotFoundUser)}", "Пользователь с указанным логином не найден");
        /// <summary>
        /// Ошибка, возникающая, когда пароль указан неверно. 
        /// </summary>
        public static Error PasswordIncorrect => Error.Validation($"{nameof(PIMS.Domain.UserAggregate.User)}.{nameof(PasswordIncorrect)}", "Пароль указан неверно");
        /// <summary>
        /// Ошибка, возникающая, когда сведения о пользователе отсутствуют (не найдены сведения о ФИО). 
        /// </summary>
        public static Error UserDataNotExists => Error.Validation($"{nameof(PIMS.Domain.UserDataAggregate.UserData)}.{nameof(UserDataNotExists)}", "Сведения о пользователе отсутствуют (не найдены сведения о ФИО)");
    }
}
