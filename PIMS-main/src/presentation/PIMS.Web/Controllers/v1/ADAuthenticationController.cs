using Azure.Core;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIMS.Application.Authentication.Commands.ConverUserFromAD;
using PIMS.Application.Authentication.Commands.RegisterUsingAD;
using PIMS.Application.Authentication.Queries.ActiveDirectory;
using PIMS.Contracts.Authentication;
using PIMS.Infrastructure.Authentication;
using PIMS.Web.Controllers.Base;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PIMS.Web.Controllers.v1
{
    /// <summary>
    /// Контроллер аутентификации AD.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
     public class ADAuthenticationController: ADBasedApiController
    {
        /// <summary>
        /// Посредник.
        /// </summary>
        private readonly IMediator _mediator;
        /// <summary>
        /// Картограф.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ADAuthenticationController"/> .
        /// </summary>
        /// <param name="mediator">Посредник..</param>
        /// <param name="mapper">Картограф.</param>
        public ADAuthenticationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        /// <summary>
        /// Вход в систему <see cref="IActionResult"/>.
        /// </summary>
        /// <returns>Возвращение значения результата действия (IActionResult)</returns>
        [HttpGet("login")]        
        public async Task<IActionResult> Login()
        {            
            var convertADUserResult = await _mediator.Send(new ConvertUserFromADCommand(HttpContext.User.Identity, HttpContext.User.Claims));
            if (convertADUserResult.IsError)
            {
                return Problem(convertADUserResult.Errors);
            }
            var loginResult = await _mediator.Send(new LoginQuery(convertADUserResult.Value));
            if (loginResult.IsError && loginResult.Errors.
                Any(e => e == Domain.Common.Errors.Errors.Authentication.NotFoundUser
                ))
            {
                var registerResult = await _mediator.Send(new RegisterCommand(convertADUserResult.Value));
                return registerResult.Match(
                    registerResult => Ok(_mapper.Map<AuthenticationResponse>(registerResult)),
                    errors => Problem(errors));
            }
            else
            {
                return Ok(_mapper.Map<AuthenticationResponse>(loginResult.Value));
            }
        }
    }
}
