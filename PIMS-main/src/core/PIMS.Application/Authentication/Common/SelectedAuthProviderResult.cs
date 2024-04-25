using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PIMS.Domain.Common.Authentication.Configuration.Enums;

namespace PIMS.Application.Authentication.Common
{
    /// <summary>
    /// Результат команды выбора провайдера аутентификации.
    /// </summary>
    public class SelectedAuthProviderResult
    {
        /// <summary>
        /// Используем сведения, полученные из провайдера аутентификации <see cref="SelectedAuthProviderResult"/> .
        /// </summary>
        /// <param name="providers">Поставщики.</param>
        /// <param name="priority">Приоритет.</param>
        public SelectedAuthProviderResult(List<AuthenticationProviders> providers, AuthenticationProviders priority)
        {
            Providers = providers;
            Priority = priority;
        }
        /// <summary>
        /// Свойство провайдера возвращает список объектов только для чтения и не могут быть изменены извне.
        /// </summary>
        public IReadOnlyList<AuthenticationProviders> Providers { get; private set; }
        /// <summary>
        /// Свойство приоритет доступно только для чтения и изменение значения возможно только внутри этого класса.
        /// </summary>
        public AuthenticationProviders Priority { get; private set; }
    }
}
