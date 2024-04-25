using FluentAssertions;
using MediatR;
using Moq;
using PIMS.Application.Authentication.Queries.ActiveDirectory;
using PIMS.Application.Common.Interfaces.Authentication;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Domain.Common.Models.ActiveDirectory;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserDataAggregate;
using PIMS.Application.UnitTests.TestUtils.Constants;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Application.Common.Interfaces.Services;
using PIMS.Infrastructure.Authentication;
using PIMS.Application.UnitTests.TestUtils.Users.Extensions;
using PIMS.Application.UnitTests.Authentication.Queries.TestUtils;
using PIMS.Application.UnitTests.TestUtils.Errors;

namespace PIMS.Application.UnitTests.Authentication.Queries.AD
{
	public class LoginQueryHandlerADTests
	{
		private readonly LoginQueryHandler _handler;
		private readonly Mock<IUserRepository> _mockUserRepository;
		private readonly Mock<IUserDataRepository> _mockUserDataRepository;
		private readonly Mock<IJwtTokenGenerator> _jwtTokenGenerator;
		private readonly Mock<IMediator> _mockMediator;
		private readonly Mock<IDateTimeProvider> _idateTimeProvider;
		public LoginQueryHandlerADTests()
		{
			_mockUserRepository = new Mock<IUserRepository>();
			_mockUserDataRepository = new Mock<IUserDataRepository>();
			_jwtTokenGenerator = new Mock<IJwtTokenGenerator>();
			_idateTimeProvider = new Mock<IDateTimeProvider>();
			_mockMediator = new Mock<IMediator>();
			_idateTimeProvider.Setup(m => m.UtcNow).Returns(new DateTime(new DateOnly(2023, 1, 1), new TimeOnly(12, 0), DateTimeKind.Utc));

			_jwtTokenGenerator.Setup(m => m.GenerateToken(It.IsAny<User>(), It.IsAny<UserData>())).Returns(JwtTokenGenerator.BaseGenerateToken(
				LoginQueryUtils.PrepareUserForLoginQuery(_idateTimeProvider.Object.UtcNow, out UserData createdUserData),
				createdUserData, UnitTests.TestUtils.Settings.Constants.JwtSettingsDefault.JwtSettings, _idateTimeProvider.Object));

			_mockUserRepository.Setup(u => u.GetUserByUserName(It.IsAny<string>()))
					.Returns(Task.FromResult<User?>(LoginQueryUtils.PrepareUserForLoginQuery(_idateTimeProvider.Object.UtcNow, out createdUserData)));

			_mockUserDataRepository.Setup(u => u.GetActualUserDataByUserId(It.IsAny<UserId>()))
				.Returns(Task.FromResult<UserData?>(createdUserData));

			_handler = new LoginQueryHandler(_mockMediator.Object, _jwtTokenGenerator.Object, _mockUserRepository.Object, _mockUserDataRepository.Object);
		}
		[Fact]
		public async Task HandleLoginQueryAD_WhenUserIsFoundAndDataExists_ShouldCreateAndReturnAuthenticationResult()
		{
			ActiveDirectoryUser adUser = new ActiveDirectoryUser(Constants.User.UserName, Constants.UserData.FirstName, Constants.UserData.MiddleName, Constants.UserData.LastName, Constants.UserData.Email,Constants.UserData.Phone);
			var result = await _handler.Handle(new LoginQuery(adUser), default);
			result.ErrorShouldBeFalse();
			result.Value.ValidationGetAuthResultAD(adUser);

		}
	}
}
