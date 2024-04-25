using PIMS.Application.Authentication.Commands.RegisterUsingAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIMS.Application.IntegrationTests.TestUtils;
using PIMS.Application.IntegrationTests.TestUtils.Constants;
using PIMS.Application.IntegrationTests.Authentication.Commands.TestUtils;
using FluentAssertions;
using FluentValidation;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserDataAggregate;

namespace PIMS.Application.IntegrationTests.Authentication.Commands.RegisterUsingForms
{
    [Collection("DatabaseCollection")]
    public class RegisterUsingFormsTests
	{
        DatabaseFixture _CurrentFixture;
        public RegisterUsingFormsTests(DatabaseFixture CurrentFixture)
        {
            _CurrentFixture = CurrentFixture;
            CurrentFixture.ResetState().GetAwaiter().GetResult();
        }
		[Fact]
		public async Task ShouldRequireNotEmptyUserName()
		{

			var command = CommandUtils.CreateCommandFormsEmptyUserName();

			var res=await _CurrentFixture.SendAsync(command);
            res.ErrorsOrEmptyList.Should().NotBeNullOrEmpty();

		}
		[Fact]
		public async Task ShouldRequireNotEmptyLastName()
		{

			var command = CommandUtils.CreateCommandFormsEmptyLastName();

			var res = await _CurrentFixture.SendAsync(command);
			res.ErrorsOrEmptyList.Should().NotBeNullOrEmpty();

		}
		[Fact]
		public async Task ShouldRegisterUsingForms()
        {
            var command = CommandUtils.CreateCommandForms();

			var res = await _CurrentFixture.SendAsync(command);
			res.Value.User.Should().NotBeNull();
			res.Value.UserData.Should().NotBeNull();
			if (res.Value.User is not null && res.Value.UserData is not null)
			{
				var user = await _CurrentFixture.FindAsync<User>(res.Value.User.Id);
				var userData = await _CurrentFixture.FindAsync<UserData>(res.Value.UserData.Id);
				user.Should().NotBeNull();
				user.UserName.Should().Be(command.UserName);
				user.CreatedDate.ToShortDateString().Should().Be(DateTime.Now.ToShortDateString());
				userData.FirstName.Should().Be(command.FirstName);
				userData.LastName.Should().Be(command.LastName);
				userData.MiddleName.Should().Be(command.MiddleName);
				userData.Email.WorkEmail.Should().Be(command.Email);
			}
		}
	}
}
