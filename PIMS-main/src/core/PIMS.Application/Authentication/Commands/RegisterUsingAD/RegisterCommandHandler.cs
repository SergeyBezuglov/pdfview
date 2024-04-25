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
using PIMS.Domain.UserDataAggregate.Enums;
using PIMS.Domain.UserDataAggregate.ValueObjects;
using PIMS.Domain.UserDataAggregate;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Application.Common.Interfaces.Services;

namespace PIMS.Application.Authentication.Commands.RegisterUsingAD
{
    /// <summary>
    /// Обработчик команды регистрации пользователя на основе данных из Active Directory
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
        /// Поставщик даты и времени.
        /// </summary>
        private readonly IDateTimeProvider _dateTimeProvider;
        /// <summary>
        /// Конструктор для обработчика команды регистрации пользователя из Active Directory <see cref="RegisterCommandHandler"/>.
        /// </summary>
        /// <param name="JwtTokenGenerator">Генератор токенов jwt.</param>
        /// <param name="userRepository">Пользовательский репозиторий.</param>
        /// <param name="dateTimeProvider">Поставщик даты и времени.</param>
        public RegisterCommandHandler(IJwtTokenGenerator JwtTokenGenerator, IUserRepository userRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _jwtTokenGenerator = JwtTokenGenerator;
            _userRepository = userRepository;
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
            if (await _userRepository.GetUserByUserName(command.User.UserName) is not null)
            {
                return Errors.User.DuplicateUserName;
            }
            var user = PrepareUserForRegisterCommand(command, _dateTimeProvider, out UserData createdUserData);
            await _userRepository.Add(user); 
            var token = _jwtTokenGenerator.GenerateToken(user!,createdUserData);

            return new AuthenticationResult() { User=user,UserData=createdUserData, Token = token };
        }

        /// <summary>
        /// Генерирование учетной записи пользователя на основе данных команды регистрации
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="dateTimeProvider">Поставщик даты и времени.</param>
        /// <param name="CreatedUserData">Созданные пользовательские данные.</param>
        /// <returns>Возвращение значения  <see cref="Domain.UserAggregate.User"/> в обертке ErrorOr</returns>
        public static Domain.UserAggregate.User PrepareUserForRegisterCommand(RegisterCommand command,
            IDateTimeProvider dateTimeProvider, out UserData CreatedUserData)
        {

            var userId = UserId.CreateUnique();
            var userDataId = UserDataId.CreateUnique();
            return PIMS.Domain.UserAggregate.User.Create(userId, command.User.UserName, "",
                CreatedUserData = UserData.Create(userId, userDataId, dateTimeProvider.UtcNow, command.User.FirstName, command.User.MiddleName, command.User.LastName,
             Email.CreateEmail(command.User.Email ?? "", ""),
            Phone.CreatePhone(command.User.PhoneNumber ?? "", "", ""),
            WorkingHours.WorkingHoursEnum.Normal), dateTimeProvider.UtcNow);
        }
    }
}
