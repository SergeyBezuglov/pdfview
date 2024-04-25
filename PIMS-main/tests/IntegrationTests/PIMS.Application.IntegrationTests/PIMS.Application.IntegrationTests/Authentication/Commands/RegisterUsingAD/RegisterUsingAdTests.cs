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

namespace PIMS.Application.IntegrationTests.Authentication.Commands.RegisterUsingAD
{
    [Collection("DatabaseCollection")]
    public class RegisterUsingAdTests
    {
        DatabaseFixture _CurrentFixture;
        public RegisterUsingAdTests(DatabaseFixture CurrentFixture)
        {
            _CurrentFixture = CurrentFixture;
            CurrentFixture.ResetState().GetAwaiter().GetResult();
        }
        [Fact]
        public async Task ShouldRequireNotEmptyADUserName()
        {

            var command = CommandUtils.CreateCommandADEmptyUserName();

            var res = await _CurrentFixture.SendAsync(command);
            res.ErrorsOrEmptyList.Should().NotBeNullOrEmpty();

        }
        [Fact]
        public async Task ShouldRequireNotEmptyADFirstName()
        {

            var command = CommandUtils.CreateCommandADEmptyFirstName();

            var res = await _CurrentFixture.SendAsync(command);
            res.ErrorsOrEmptyList.Should().NotBeNullOrEmpty();

        }
        [Fact]
        public async Task ShouldRegisterUsingAd()
        {
            var command = CommandUtils.CreateCommandAD();

            var res = await _CurrentFixture.SendAsync(command);
            res.Value.User.Should().NotBeNull();
            res.Value.UserData.Should().NotBeNull();
            if (res.Value.User is not null && res.Value.UserData is not null)
            {
                var user = await _CurrentFixture.FindAsync<User>(res.Value.User.Id);
                var userData = await _CurrentFixture.FindAsync<UserData>(res.Value.UserData.Id);
                user.Should().NotBeNull();
                user.UserName.Should().Be(command.User.UserName);
                user.CreatedDate.ToShortDateString().Should().Be(DateTime.Now.ToShortDateString());
                userData.FirstName.Should().Be(command.User.FirstName);
                userData.LastName.Should().Be(command.User.LastName);
                userData.MiddleName.Should().Be(command.User.MiddleName);
                userData.Email.WorkEmail.Should().Be(command.User.Email);
            }
        }
    }
}
