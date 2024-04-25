using ErrorOr;
using MediatR;
using PIMS.Application.Authentication.Common;
using PIMS.Domain.Common.Authentication.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Authentication.Commands.SelectAuthProvider
{
    /// <summary>
    /// Команда выбора провайдера аутентификации.
    /// </summary>
    public class SelectAuthProviderCommand: IRequest<ErrorOr<SelectedAuthProviderResult>>
    {
        /// <summary>
        /// Используем authenticationModules, полученную из опций модулей проверки подлинности, модули из провайдера <see cref="SelectAuthProviderCommand"/> .
        /// </summary>
        /// <param name="authenticationModules">Пользовательские модули проверки подлинности.</param>
        public SelectAuthProviderCommand(List<AuthenticationModuleOption> authenticationModules) {
            AuthenticationModules = authenticationModules;
        }
        /// <summary>
        /// Сведения пользовательских модулей проверки подлинности, которые инициализируются новым пустым списком.
        /// </summary>
        /// <value>Обобщенный интерфейс, представляющий упорядоченный список объектов</value>
        public IList<AuthenticationModuleOption> AuthenticationModules { get; set; } = new List<AuthenticationModuleOption>();
    }
}
