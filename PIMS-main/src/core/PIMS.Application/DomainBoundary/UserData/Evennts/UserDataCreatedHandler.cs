using MediatR;
using Microsoft.Extensions.Logging;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Common;
using PIMS.Domain.UserAggregate.Events;
using PIMS.Domain.UserDataAggregate.Events;
using PIMS.Logger.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.DomainBoundary.UserData.Evennts
{
    /// <summary>
    /// Обработчик создания данных пользователя.
    /// </summary>
    public class UserDataCreatedHandler : INotificationHandler<UserDataCreated>
    {
        /// <summary>
        /// Логер.
        /// </summary>
        private readonly ILogger<UserDataCreatedHandler> _logger;
        /// <summary>
        /// Создает средство ведения ILogger<UserDataCreatedHandler>, которое использует категорию с полным именем типа <see cref="UserDataCreatedHandler"/> .
        /// </summary>
        /// <param name="logger">Логер.</param>
        public UserDataCreatedHandler(ILogger<UserDataCreatedHandler> logger)
        {
            _logger = logger;
            
        }
        /// <summary>
        /// Обрабатывает событие "UserCreated", создает учетную запись сотрудника.
        /// </summary>
        /// <param name="notification">Уведомление.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        public Task Handle(UserDataCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogWithUserInfo(LogLevel.Information,
                $"Создана учетная запись Сотрудника {notification.UserData.FirstName } {notification.UserData.MiddleName} {notification.UserData.LastName}", CommonSystemValues.DefaultSystemUserName);
            return Task.CompletedTask;
        }
    }
}
