using FluentAssertions;
using Moq;
using PIMS.Application.Authentication.Commands.SelectAuthProvider;
using PIMS.Application.UnitTests.Authentication.Commands.TestUtils;
using PIMS.Application.UnitTests.TestUtils.Errors;
using PIMS.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PIMS.Domain.Common.Authentication.Configuration.Enums;

namespace PIMS.Application.UnitTests.Authentication.Commands.SelectAuthProvider
{
    public class SelectAuthProviderCommandHandlerTests
    {
        private readonly SelectAuthProviderCommandHandler _handler;
        public SelectAuthProviderCommandHandlerTests()
        {
            _handler = new SelectAuthProviderCommandHandler();  
        }
        [Theory]
        [MemberData(nameof(ValidSelectAuthCommandAD))]
        public async Task HandleSelectAuthProviderCommand_WhenSelectAuthAD(SelectAuthProviderCommand command)
        {

            var result = await _handler.Handle(command, default);
			result.ErrorShouldBeFalse();
			Assert.Contains(result.Value.Providers, p => p.Equals(AuthenticationProviders.ActiveDirectory));
			Assert.Equal(AuthenticationProviders.ActiveDirectory, result.Value.Priority);

		}
		public static IEnumerable<object[]> ValidSelectAuthCommandAD()
        {
            yield return new[] { CommandUtils.CreateSelectAuthCommandAD() };
        }
		[Theory]
		[MemberData(nameof(ValidSelectAuthCommandJWT))]
		public async Task HandleSelectAuthProviderCommand_WhenSelectAuthJWT(SelectAuthProviderCommand command)
		{

			var result = await _handler.Handle(command, default);
			result.ErrorShouldBeFalse();
			Assert.DoesNotContain(result.Value.Providers, p => p.Equals(AuthenticationProviders.ActiveDirectory));
			Assert.Contains(result.Value.Providers, p => p.Equals(AuthenticationProviders.JWT));
			Assert.Equal(AuthenticationProviders.JWT, result.Value.Priority);
		}
		public static IEnumerable<object[]> ValidSelectAuthCommandJWT()
		{
			yield return new[] { CommandUtils.CreateSelectAuthCommandJWT() };
		}
	}
}
