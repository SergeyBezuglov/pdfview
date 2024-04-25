using MediatR;
using Microsoft.Extensions.Logging;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Common;
using PIMS.Domain.UserAggregate.Events;
using PIMS.Logger.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.DomainBoundary.User.Events
{
    /// <summary>
    /// Обработчик создания пользователя.
    /// </summary>
    public class UserCreatedHandler : INotificationHandler<UserCreated>
    {
        /// <summary>
        /// Хранилище пользовательских данных.
        /// </summary>
        private readonly IUserDataRepository _userDataRepository;
        /// <summary>
        /// Логер.
        /// </summary>
        private readonly ILogger<UserCreatedHandler> _logger;
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UserCreatedHandler"/> .
        /// </summary>
        /// <param name="logger">Логер.</param>
        /// <param name="userDataRepository">Хранилище пользовательских данных.</param>
        public UserCreatedHandler(ILogger<UserCreatedHandler> logger, IUserDataRepository userDataRepository)
        {
            _logger = logger;
            _userDataRepository = userDataRepository;
        }
        /// <summary>
        /// Обрабатывает событие "UserCreated", содержит информацию о созданном пользователе, добавление информации о созданном пользователе в базу данных. 
        /// </summary>
        /// <param name="notification">Уведомление.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
        {           
            await _userDataRepository.Add(notification.UserData);
        }
    }
}
