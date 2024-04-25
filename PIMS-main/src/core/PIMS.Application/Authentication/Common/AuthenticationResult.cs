//using PIMS.Application.Base;
using PIMS.Application.Base;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserDataAggregate;

namespace PIMS.Application.Authentication.Common
{
    /// <summary>
    /// Результат аутентификации.
    /// </summary>
    public class AuthenticationResult : BaseModelResult
    {
        /// <summary>
        /// Свойство пользователь должно быть передано или установлено при создании экземпляра объекта, также принимает нулевое значение.
        /// </summary>
        public required User? User { get; set; }
        /// <summary>
        /// Свойство данные пользователя должно быть передано или установлено при создании экземпляра объекта, также принимает нулевое значение.
        /// </summary>
        /// <value>Пользовательские данные.</value>
        public required UserData? UserData { get; set; }
        /// <summary>
        ///  Открытое свойство токен должно быть передано или установлено при создании экземпляра объекта.
        /// </summary>
        public required string Token { get; set; } = string.Empty;
    }
}
