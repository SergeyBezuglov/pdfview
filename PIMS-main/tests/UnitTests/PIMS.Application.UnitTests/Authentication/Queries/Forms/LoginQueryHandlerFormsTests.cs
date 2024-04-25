using FluentAssertions;
using MediatR;
using Moq;
using PIMS.Application.Authentication.Queries.Forms;
using PIMS.Application.Common.Interfaces.Authentication;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserDataAggregate;
using PIMS.Application.UnitTests.TestUtils.Constants;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Application.Common.Interfaces.Services;
using PIMS.Infrastructure.Authentication;
using PIMS.Application.UnitTests.TestUtils.Users.Extensions;
using PIMS.Application.UnitTests.Authentication.Queries.TestUtils;
using PIMS.Application.UnitTests.TestUtils.Errors;

namespace PIMS.Application.UnitTests.Authentication.Queries.Forms
{
	public class LoginQueryHandlerFormsTests
	{
		private readonly LoginQueryHandler _handler;
		private readonly Mock<IUserRepository> _mockUserRepository;
		private readonly Mock<IUserDataRepository> _mockUserDataRepository;
		private readonly Mock<IJwtTokenGenerator> _jwtTokenGenerator;
		private readonly Mock<IMediator> _mockMediator;
		private readonly Mock<IDateTimeProvider> _idateTimeProvider;
		public LoginQueryHandlerFormsTests()
		{
			_mockUserRepository = new Mock<IUserRepository>();
			_mockUserDataRepository = new Mock<IUserDataRepository>();
			_jwtTokenGenerator = new Mock<IJwtTokenGenerator>();
			_idateTimeProvider = new Mock<IDateTimeProvider>();
			_mockMediator = new Mock<IMediator>();
			_idateTimeProvider.Setup(m => m.UtcNow).Returns(new DateTime(new DateOnly(2023, 1, 1), new TimeOnly(12, 0), DateTimeKind.Utc));

			_jwtTokenGenerator.Setup(m => m.GenerateToken(It.IsAny<User>(), It.IsAny<UserData>())).Returns(JwtTokenGenerator.BaseGenerateToken(
				LoginQueryUtils.PrepareUserForLoginQuery(_idateTimeProvider.Object.UtcNow, out UserData createdUserData),
				createdUserData, PIMS.Application.UnitTests.TestUtils.Settings.Constants.JwtSettingsDefault.JwtSettings, _idateTimeProvider.Object));

			_mockUserRepository.Setup(u => u.GetUserByUserName(It.IsAny<string>()))
					.Returns(Task.FromResult<User?>(LoginQueryUtils.PrepareUserForLoginQuery(_idateTimeProvider.Object.UtcNow, out createdUserData)));

			_mockUserDataRepository.Setup(u => u.GetActualUserDataByUserId(It.IsAny<UserId>()))
				.Returns(Task.FromResult<UserData?>(createdUserData));

			_handler = new LoginQueryHandler(_jwtTokenGenerator.Object, _mockUserRepository.Object, _mockUserDataRepository.Object);
		}
		[Fact]
		public async Task HandleLoginQueryForms_WhenUserIsFoundAndDataExists_ShouldCreateAndReturnAuthenticationResult()
		{
			var result = await _handler.Handle(new LoginQuery(), default);
			result.ErrorShouldBeFalse();
			result.Value.ValidationGetAuthResultForms();

		}

	}
}
