using ErrorOr;
using MediatR;
using PIMS.Application.Common.Interfaces.Authentication;
using PIMS.Application.Common.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIMS.Domain.Common.Errors;
using PIMS.Domain.UserAggregate;
using PIMS.Application.Authentication.Common;
using Microsoft.Extensions.Logging;
using PIMS.Logger.Services;
using PIMS.Common;
using PIMS.Domain.UserDataAggregate.Enums;
using PIMS.Domain.UserDataAggregate.ValueObjects;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Domain.UserDataAggregate;
using static PIMS.Domain.Common.Errors.Errors;
using PIMS.Application.Common.Interfaces.Services;

namespace PIMS.Application.Authentication.Commands.RegisterUsingForms
{
    /// <summary>
    /// Обработчик команды регистрации пользователя на основе форм.
    /// </summary>
    public class RegisterCommandHandler :
        IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        /// <summary>
        /// Генератор токенов jwt.
        /// </summary>
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        /// <summary>
        /// Пользовательский репозиторий.
        /// </summary>
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// Регистратор.
        /// </summary>
        private readonly ILogger<RegisterCommandHandler> _logger;
        /// <summary>
        /// Поставщик даты и времени.
        /// </summary>
        private readonly IDateTimeProvider _dateTimeProvider;
        /// <summary>
        /// Конструктор для обработчика команды регистрации пользователя из форм <see cref="RegisterCommandHandler"/> .
        /// </summary>
        /// <param name="JwtTokenGenerator">Генератор токенов jwt.</param>
        /// <param name="userRepository">Пользовательский репозиторий.</param>
        public RegisterCommandHandler(IJwtTokenGenerator JwtTokenGenerator, IUserRepository userRepository, 
            ILogger<RegisterCommandHandler> logger,
            IDateTimeProvider dateTimeProvider)
        {
            _jwtTokenGenerator = JwtTokenGenerator;
            _userRepository = userRepository;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }
        /// <summary>
        /// Создает учетную запись Пользователя в базе данных, генерирует Jwt Token и возвращает сведения о Пользователе, Jwt Token
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Возвращение значения ErrorOr.</returns>
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {            
            if (await _userRepository.GetUserByUserName(command.UserName) is not null)
            {
                _logger.LogWithUserInfo(LogLevel.Warning, $"В процессе регистрации Пользователя {command.UserName} возникла ошибка {Errors.User.DuplicateUserName.Description}", CommonSystemValues.DefaultSystemUserName);
                return Errors.User.DuplicateUserName;
            }
            var user = PrepareUserForRegisterCommand(command, _dateTimeProvider, out UserData createdUserData);
            await _userRepository.Add(user);            
            var token = _jwtTokenGenerator.GenerateToken(user!, createdUserData);
            return new AuthenticationResult() { User=user,UserData=createdUserData, Token = token };
        }
        /// <summary>
        /// Генерирование учетной записи пользователя на основе данных команды регистрации
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="dateTimeProvider">Поставщик даты и времени.</param>
        /// <param name="CreatedUserData">Созданные пользовательские данные.</param>
        /// <returns>Возвращение значения <see cref="Domain.UserAggregate.User"/> в обертке ErrorOr</returns>
        public static Domain.UserAggregate.User PrepareUserForRegisterCommand(RegisterCommand command,             
            IDateTimeProvider dateTimeProvider, out UserData CreatedUserData)
        {
            
            var userId = UserId.CreateUnique();
            var userDataId =  UserDataId.CreateUnique();
            return PIMS.Domain.UserAggregate.User.Create(userId, command.UserName, command.Password,
                CreatedUserData = UserData.Create(userId, userDataId,dateTimeProvider.UtcNow, command.FirstName, command.MiddleName, command.LastName,
             Email.CreateEmail(command.Email ?? "", ""),
            Phone.CreatePhone("", "", ""),
            WorkingHours.WorkingHoursEnum.Normal), dateTimeProvider.UtcNow);            
        }
    }
}
