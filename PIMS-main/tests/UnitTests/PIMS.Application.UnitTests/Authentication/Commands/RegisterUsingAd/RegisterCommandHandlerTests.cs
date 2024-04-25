using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Moq;
using PIMS.Application.Authentication.Commands.RegisterUsingAD;
using PIMS.Application.Common.Interfaces.Authentication;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Application.Common.Interfaces.Services;
using PIMS.Application.UnitTests.Authentication.Commands.TestUtils;
using PIMS.Application.UnitTests.TestUtils.Constants;
using PIMS.Application.UnitTests.TestUtils.Errors;
using PIMS.Application.UnitTests.TestUtils.Users.Extensions;
using PIMS.Domain.Common.Authentication.Configuration;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Domain.UserDataAggregate;
using PIMS.Infrastructure.Authentication;
using PIMS.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.UnitTests.Authentication.Commands.RegisterUsingAD
{
    public class RegisterCommandHandlerTests
    {
        private readonly RegisterCommandHandler _handler;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGenerator;
        private readonly Mock<IDateTimeProvider> _idateTimeProvider;
        private readonly Mock<ILogger<RegisterCommandHandler>> _logger;
        public RegisterCommandHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();            
            _jwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            _idateTimeProvider = new Mock<IDateTimeProvider>();
            _logger =new Mock<ILogger<RegisterCommandHandler>> ();
            _idateTimeProvider.SetupAllProperties();
            _idateTimeProvider.Setup(m=>m.UtcNow).Returns(new DateTime(new DateOnly(2023,1 ,1),new TimeOnly(12,0),DateTimeKind.Utc));
            _handler = new RegisterCommandHandler(_jwtTokenGenerator.Object, _mockUserRepository.Object, _idateTimeProvider.Object);            
        }
        [Theory]
        [MemberData(nameof(ValidRegisterCommandAD))]
        public async Task HandleRegisterCommandAD_WhenUserIsValid_ShouldCreateAndReturnUserAndToken(RegisterCommand command)
        {

            var generatedUser = RegisterCommandHandler.PrepareUserForRegisterCommand(command, _idateTimeProvider.Object, out UserData createdUserData);
            _jwtTokenGenerator.Setup(m => m.GenerateToken(It.IsAny<User>(), It.IsAny<UserData>())).Returns(JwtTokenGenerator.BaseGenerateToken(generatedUser, createdUserData,
                PIMS.Application.UnitTests.TestUtils.Settings.Constants.JwtSettingsDefault.JwtSettings, _idateTimeProvider.Object));            
            var result =await _handler.Handle(command, default);
            result.ErrorShouldBeFalse();
			result.Value.ValidationCreatedAD(command);            
            _mockUserRepository.Verify(m=>m.Add(It.IsAny<User>()),Times.Once());

        }
        public static IEnumerable<object[]> ValidRegisterCommandAD()
        {
            yield return new[] { CommandUtils.CreateCommandAD() };
         
        }
    }
}
