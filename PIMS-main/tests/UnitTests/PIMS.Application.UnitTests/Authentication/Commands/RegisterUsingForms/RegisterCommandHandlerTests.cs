using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Moq;
using PIMS.Application.Authentication.Commands.RegisterUsingForms;
using PIMS.Application.Common.Interfaces.Authentication;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Application.Common.Interfaces.Services;
using PIMS.Application.UnitTests.Authentication.Commands.TestUtils;
using PIMS.Application.UnitTests.TestUtils.Errors;
using PIMS.Application.UnitTests.TestUtils.Users.Extensions;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserDataAggregate;
using PIMS.Infrastructure.Authentication;

namespace PIMS.Application.UnitTests.Authentication.Commands.RegisterUsingForms
{
    /// <summary>
    /// The register command handler tests.
    /// </summary>
    public class RegisterCommandHandlerTests
    {
        /// <summary>
        /// Обработчик.
        /// </summary>
        private readonly RegisterCommandHandler _handler;
        /// <summary>
        /// Репозиторий ложного пользователя.
        /// </summary>
        private readonly Mock<IUserRepository> _mockUserRepository;
        /// <summary>
        /// Генератор токенов jwt.
        /// </summary>
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGenerator;
        /// <summary>
        /// Поставщик времени idate.
        /// </summary>
        private readonly Mock<IDateTimeProvider> _idateTimeProvider;
        /// <summary>
        /// Регистратор.
        /// </summary>
        private readonly Mock<ILogger<RegisterCommandHandler>> _logger;
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RegisterCommandHandlerTests"/> .
        /// </summary>
        public RegisterCommandHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();            
            _jwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            _idateTimeProvider = new Mock<IDateTimeProvider>();
            _logger =new Mock<ILogger<RegisterCommandHandler>> ();
            _idateTimeProvider.SetupAllProperties();
            _idateTimeProvider.Setup(m=>m.UtcNow).Returns(new DateTime(new DateOnly(2023,1 ,1),new TimeOnly(12,0),DateTimeKind.Utc));
            _handler = new RegisterCommandHandler(_jwtTokenGenerator.Object, _mockUserRepository.Object, _logger.Object, _idateTimeProvider.Object);            
        }
		/// <summary>
		/// Обработка команды регистрации с помощью форм, когда пользователь действителен, должна создавать и возвращать пользователя и токен.
		/// </summary>
		/// <param name="command">Команда.</param>
		/// <returns>Возвращение значения задачи (Task).</returns>
		[Theory]
        [MemberData(nameof(ValidRegisterCommandForms))]
        public async Task HandleRegisterCommandForms_WhenUserIsValid_ShouldCreateAndReturnUserAndToken(RegisterCommand command)
        {

            var generatedUser = RegisterCommandHandler.PrepareUserForRegisterCommand(command, _idateTimeProvider.Object, out UserData createdUserData);                        ;
            _jwtTokenGenerator.Setup(m => m.GenerateToken(It.IsAny<User>(), It.IsAny<UserData>())).Returns(JwtTokenGenerator.BaseGenerateToken(generatedUser, createdUserData,
                PIMS.Application.UnitTests.TestUtils.Settings.Constants.JwtSettingsDefault.JwtSettings, _idateTimeProvider.Object));            
            var result =await _handler.Handle(command, default);
			result.ErrorShouldBeFalse();
			result.Value.ValidationCreatedForm(command);            
            _mockUserRepository.Verify(m=>m.Add(It.IsAny<User>()),Times.Once());

        }
        /// <summary>
        /// Проверяет команду регистрации с помощью форм.
        /// </summary>
        /// <returns>Возвращает значения списка объектов (list of object[]).</returns>
        public static IEnumerable<object[]> ValidRegisterCommandForms()
        {
            yield return new[] { CommandUtils.CreateCommand() };
         
        }
    }
}
