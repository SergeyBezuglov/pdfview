using FluentAssertions;
using PIMS.Application.Authentication.Queries.ActiveDirectory;
using PIMS.Application.IntegrationTests.Authentication.Queries.TestUtils;
using PIMS.Application.IntegrationTests.TestUtils.Constants;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Domain.UserDataAggregate;
using PIMS.Domain.UserDataAggregate.ValueObjects;
using PIMS.Domain.Common.Errors;

namespace PIMS.Application.IntegrationTests.Authentication.Queries.ActiveDirectory
{
	[Collection("DatabaseCollection")]
	public class LoginQueryADTests
	{
		DatabaseFixture _CurrentFixture;
		public LoginQueryADTests(DatabaseFixture CurrentFixture)
		{
			_CurrentFixture = CurrentFixture;
			CurrentFixture.ResetState().GetAwaiter().GetResult();
		}


		[Fact]
		public async Task ShouldReturnLoginUser()
		{
			var userId = UserId.CreateUnique();
			await _CurrentFixture.AddAsync(User.Create(userId, Constants.User.UserName, Constants.User.Password, UserData.Create(userId, 
				UserDataId.CreateUnique(),DateTime.Now, Constants.UserData.FirstName, Constants.UserData.MiddleName, Constants.UserData.LastName, Email.CreateEmail(Constants.UserData.Email), Phone.CreatePhone(Constants.UserData.Phone)),
				DateTime.Now));
			var query = new LoginQuery(LoginQueryUtils.CreateADUser());
			var result = await _CurrentFixture.SendAsync(query);

			result.ErrorsOrEmptyList.Should().BeNullOrEmpty();
			result.Value.User.Should().NotBeNull();
			if (result.Value.User is not null)
			{
				result.Value.User.UserName.Should().Be(Constants.User.UserName);
				result.Value.User.Password.Should().Be(Constants.User.Password);
				result.Value.User.CreatedDate.ToShortDateString().Should().Be(DateTime.Now.ToShortDateString());
			}
			result.Value.UserData.Should().NotBeNull();
			if (result.Value.UserData is not null)
			{
				result.Value.UserData.FirstName.Should().Be(Constants.UserData.FirstName);
				result.Value.UserData.LastName.Should().Be(Constants.UserData.LastName);
				result.Value.UserData.MiddleName.Should().Be(Constants.UserData.MiddleName);
				result.Value.UserData.Email.WorkEmail.Should().Be(Constants.UserData.Email);
			}
		}
		//[Fact]
		//public async Task ShouldReturnNotFoundUser()
		//{
		//	var userId = UserId.CreateUnique();
		//	await _CurrentFixture.AddAsync(User.Create(userId, Constants.User.UserName, Constants.User.Password, UserData.Create(userId,
		//		UserDataId.CreateUnique(), DateTime.Now, Constants.UserData.FirstName, Constants.UserData.MiddleName, Constants.UserData.LastName, Email.CreateEmail(Constants.UserData.Email), Phone.CreatePhone(Constants.UserData.Phone)),
		//		DateTime.Now));
		//	var query = new LoginQuery(LoginQueryUtils.CreateAnotherADUser());
		//	var result = await _CurrentFixture.SendAsync(query);

		//	result.ErrorsOrEmptyList.Should().NotBeNullOrEmpty();
		//	result.ErrorsOrEmptyList.First().Should().Be(Errors.Authentication.NotFoundUser);
		//}
	}
}
