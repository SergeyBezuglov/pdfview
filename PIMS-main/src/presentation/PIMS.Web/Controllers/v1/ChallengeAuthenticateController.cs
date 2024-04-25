using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PIMS.Application.Authentication.Commands.ConverUserFromAD;
using PIMS.Application.Authentication.Commands.SelectAuthProvider;
using PIMS.Contracts.Authentication;
using PIMS.Domain.Common.Authentication.Configuration;
using PIMS.Infrastructure.Authentication;
using PIMS.Web.Controllers.Base;

namespace PIMS.Web.Controllers.v1
{
    /// <summary>
    /// Задача аутентификации контроллера.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class ChallengeAuthenticateController:BaseApiController
    {
        /// <summary>
        /// Настройки провайдера активного каталога.
        /// </summary>
        private readonly ActiveDirectoryAuthenticationModuleOption _activeDirectoryProviderSettings;
        /// <summary>
        /// Настройки провайдера jwt.
        /// </summary>
        private readonly JwtAuthenticationModuleOption _jwtProviderSettings;
        /// <summary>
        /// Посредник.
        /// </summary>
        private readonly IMediator _mediator;
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ChallengeAuthenticateController"/> .
        /// </summary>
        /// <param name="mediator">Посредник.</param>
        /// <param name="activeDirectoryProviderSettings">Настройки провайдера активного каталога.</param>
        /// <param name="jwtProviderSettings">Настройки провайдера jwt.</param>
        public ChallengeAuthenticateController(IMediator mediator, IOptions<ActiveDirectoryAuthenticationModuleOption> activeDirectoryProviderSettings,
            IOptions<JwtAuthenticationModuleOption> jwtProviderSettings)
        {
            _mediator = mediator;
            _activeDirectoryProviderSettings = activeDirectoryProviderSettings.Value;
            _jwtProviderSettings = jwtProviderSettings.Value;
        }

        /// <summary>
        /// Испытание <see cref="IActionResult"/>.
        /// </summary>
        /// <returns>Возвращение значения результата действия (IActionResult).</returns>
        [HttpGet("challenge")]
        public async new Task<IActionResult> Challenge()
        { 
            var selectedProviders = await _mediator.Send(new SelectAuthProviderCommand(new List<AuthenticationModuleOption>()
            {
                _activeDirectoryProviderSettings,
                _jwtProviderSettings
            }));
            if (selectedProviders.IsError)
            {
                return Problem(selectedProviders.Errors);
            }
            if (selectedProviders.Value.Priority == Domain.Common.Authentication.Configuration.Enums.AuthenticationProviders.ActiveDirectory)
            {
                return Ok(new ChallengeResponse(Contracts.Authentication.Enums.ChallengeResponseType.AD));
            }
            if (selectedProviders.Value.Priority == Domain.Common.Authentication.Configuration.Enums.AuthenticationProviders.JWT)
            {
                return Ok(new ChallengeResponse(Contracts.Authentication.Enums.ChallengeResponseType.JWT));
            }

            return Ok(Array.Empty<string>());
        }

    }
}
