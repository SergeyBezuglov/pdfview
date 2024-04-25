using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Errors;

 public  static partial class Errors
{
    /// <summary>
    /// Объявление статистических классов пользователя.
    /// </summary>
    public static class User
    {
        /// <summary>
        /// Ошибка, возникающая, когда пользователь с указанным логином уже существует. 
        /// </summary>
        public static Error DuplicateUserName => Error.Conflict($"{nameof(PIMS.Domain.UserAggregate.User)}.{nameof(DuplicateUserName)}", "Пользователь с указанным логином уже существует");
        /// <summary>
        /// Ошибка, возникающая, когда пользователь с указанным почтовым ящиком уже существует. 
        /// </summary>
        public static Error DuplicateEmail => Error.Conflict($"{nameof(PIMS.Domain.UserAggregate.User)}.{nameof(DuplicateEmail)}", "Пользователь с указанным почтовым ящиком уже существует");
      
    }
}
