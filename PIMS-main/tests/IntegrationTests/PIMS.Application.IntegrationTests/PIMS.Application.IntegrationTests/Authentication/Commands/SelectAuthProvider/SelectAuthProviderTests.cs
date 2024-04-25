using PIMS.Application.Authentication.Commands.SelectAuthProvider;
using PIMS.Application.IntegrationTests.Authentication.Commands.TestUtils;
using PIMS.Application.IntegrationTests.TestUtils.Errors;
using static PIMS.Domain.Common.Authentication.Configuration.Enums;

namespace PIMS.Application.IntegrationTests.Authentication.Commands.SelectAuthProvider
{
	[Collection("DatabaseCollection")]
	public class SelectAuthProviderTests
	{
		DatabaseFixture _CurrentFixture;
		private readonly SelectAuthProviderCommandHandler _handler;
		public SelectAuthProviderTests(DatabaseFixture CurrentFixture)
		{
			_CurrentFixture = CurrentFixture;
			CurrentFixture.ResetState().GetAwaiter().GetResult();
			_handler = new SelectAuthProviderCommandHandler();
		}

		[Fact]
		public async Task ShouldReturnADProvider()
		{
			var command = CommandUtils.CreateSelectAuthCommandAD();
			var result = await _handler.Handle(command, default);
			result.ErrorShouldBeFalse();
			Assert.Contains(result.Value.Providers, p => p.Equals(AuthenticationProviders.ActiveDirectory));
			Assert.Equal(AuthenticationProviders.ActiveDirectory, result.Value.Priority);
		}
		[Fact]
		public async Task ShouldReturnJWTProvider()
		{
			var command = CommandUtils.CreateSelectAuthCommandJWT();
			var result = await _handler.Handle(command, default);
			result.ErrorShouldBeFalse();
			Assert.DoesNotContain(result.Value.Providers, p => p.Equals(AuthenticationProviders.ActiveDirectory));
			Assert.Contains(result.Value.Providers, p => p.Equals(AuthenticationProviders.JWT));
			Assert.Equal(AuthenticationProviders.JWT, result.Value.Priority);
		}
	}
}
