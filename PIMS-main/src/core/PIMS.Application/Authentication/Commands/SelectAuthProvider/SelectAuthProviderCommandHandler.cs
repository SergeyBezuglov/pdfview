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
    /// Обработчик команды выбора провайдера аутентификации.
    /// </summary>
    public class SelectAuthProviderCommandHandler :
        IRequestHandler<SelectAuthProviderCommand, ErrorOr<SelectedAuthProviderResult>>
    {
        /// <summary>
        /// Обрабатывает запрос на выбор провайдера аутентификации, возвращает созданный экземпляр класса <see cref="SelectedAuthProviderResult"/> в результате выполнения запроса.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Возвращение значения ErrorOr.</returns>
        public async Task<ErrorOr<SelectedAuthProviderResult>> Handle(SelectAuthProviderCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            Enums.AuthenticationProviders? Priority = Enums.AuthenticationProviders.JWT;
            var ADProvider = request.AuthenticationModules.FirstOrDefault(m => m.Type == Enums.AuthenticationProviders.ActiveDirectory);
            if (ADProvider != null)

            {
                Priority = ADProvider.Type;
            }            
            return new SelectedAuthProviderResult(request.AuthenticationModules.Select(m => m.Type).ToList(),Priority.Value);
        }
    }
}
