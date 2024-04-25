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
using static System.Runtime.InteropServices.JavaScript.JSType;
using PIMS.Domain.UserAggregate;
using PIMS.Application.Authentication.Common;
using PIMS.Domain.UserAggregate.Events;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Domain.UserDataAggregate;

namespace PIMS.Application.Authentication.Queries.ActiveDirectory
{
    /// <summary>
    /// Команда обработчика запроса на вход.
    /// </summary>
    public class LoginQueryHandler :
        IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
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
        /// Хранилище пользовательских данных.
        /// </summary>
        private readonly IUserDataRepository _userDataRepository;
        /// <summary>
        /// Посредник.
        /// </summary>
        private readonly IMediator _mediator;
        /// <summary>
        /// Конструктор для обработчика команды запроса на вход <see cref="LoginQueryHandler"/> .
        /// </summary>
        /// <param name="mediator">Посредник.</param>
        /// <param name="JwtTokenGenerator">Генератор токенов jwt.</param>
        /// <param name="userRepository">Пользовательский репозиторий.</param>
        /// <param name="userDataRepository">Хранилище пользовательских данных..</param>
        public LoginQueryHandler(IMediator mediator, IJwtTokenGenerator JwtTokenGenerator, IUserRepository userRepository,
            IUserDataRepository userDataRepository)
        {
            _jwtTokenGenerator = JwtTokenGenerator;
            _userRepository = userRepository;
            _userDataRepository = userDataRepository;
            _mediator = mediator;
        }
        /// <summary>
        /// Обрабатывает запрос на вход пользователя, генерирует токен JWT и возвращает результат аутентификации (AuthenticationResult).
        /// </summary>
        /// <param name="query">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Возвращение значения ErrorOr.</returns>
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {            
            if (await _userRepository.GetUserByUserName(query.User.UserName) is not User user)
            {               
                return Errors.Authentication.NotFoundUser;
            }

            if (await _userDataRepository.GetActualUserDataByUserId((UserId)user.Id) is not UserData userData)
            {
                return Errors.Authentication.UserDataNotExists;
            }           
          
            var token = _jwtTokenGenerator.GenerateToken(user, userData!);
            return new AuthenticationResult()
            {
                User=user,
                UserData= userData,
                Token = token
            };
        }
    }
}
