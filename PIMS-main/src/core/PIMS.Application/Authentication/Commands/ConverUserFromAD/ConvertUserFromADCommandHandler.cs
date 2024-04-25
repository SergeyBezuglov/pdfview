using ErrorOr;
using MediatR;
using PIMS.Application.Authentication.Commands.RegisterUsingForms;
using PIMS.Application.Authentication.Common;
using PIMS.Application.Common.Interfaces.Authentication;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Application.Common.Interfaces.Services.ActiveDirectory;
using PIMS.Domain.Common.Models.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Authentication.Commands.ConverUserFromAD
{
    /// <summary>
    /// Преобразование пользователя из обработчика команды на создание учетной записи Пользователя PIMS на основе учетной записи Active Directory
    /// </summary>
    internal class ConvertUserFromADCommandHandler : IRequestHandler<ConvertUserFromADCommand, ErrorOr<ActiveDirectoryUser>>
    {
        /// <summary>
        /// Поставщик пользователей Active Directory
        /// </summary>
        private readonly IActiveDirectoryUserProvider _activeDirectoryUserProvider;
        /// <summary>
        ///  Создаем на основе сведений поставщика Active Directory <see cref="ConvertUserFromADCommandHandler"/> .
        /// </summary>
        /// <param name="activeDirectoryUserProvider">Поставщик пользователей активного каталога.</param>
        public ConvertUserFromADCommandHandler(IActiveDirectoryUserProvider activeDirectoryUserProvider)
        {
            _activeDirectoryUserProvider = activeDirectoryUserProvider;
        }
        /// <summary>
        /// Обработчик команды
        /// </summary>
        /// <param name="request">Данные о Пользователе</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Возвращение значения  <see cref="ActiveDirectoryUser"/> в обертке ErrorOr</returns>
        public async Task<ErrorOr<ActiveDirectoryUser>> Handle(ConvertUserFromADCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            ActiveDirectoryUser user = _activeDirectoryUserProvider.GetADUserFromIndentity(request.Identity!, request.Claims);           
            return user;
        }
    }
}
