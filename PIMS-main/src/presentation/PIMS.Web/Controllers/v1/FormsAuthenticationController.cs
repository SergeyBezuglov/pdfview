 using Microsoft.AspNetCore.Mvc;
 using PIMS.Contracts.Authentication;
using PIMS.Application.Authentication;
using PIMS.Web.Controllers.Base;
using MediatR;
using PIMS.Application.Authentication.Commands.RegisterUsingForms;
using Microsoft.IdentityModel.Tokens;
using PIMS.Application.Authentication.Queries.Forms;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using PIMS.Domain;

namespace PIMS.Web.Controllers.v1;
[ApiVersion("1.0")] 
[Route("api/v{version:apiVersion}/[controller]")]
[AllowAnonymous]
public class FormsAuthenticationController : JwtBasedApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public FormsAuthenticationController(IMediator mediator,IMapper mapper)
    { 
        _mediator = mediator;
        _mapper = mapper;
    }
    [HttpPost("register")]
    public  async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var registerResult = await _mediator.Send(command);
      
        return registerResult.Match(
            registerResult=>Ok(_mapper.Map<AuthenticationResponse>(registerResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var data = HttpContext.User;
        var loginQuery= _mapper.Map<LoginQuery>(request);
        var loginResult = await _mediator.Send(loginQuery);
        if (loginResult.IsError && loginResult.Errors.
            Any(e=>e== Domain.Common.Errors.Errors.Authentication.NotFoundUser ||
            e == Domain.Common.Errors.Errors.Authentication.PasswordIncorrect  
            ))
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: loginResult.FirstError.Description);
        }


        return
            loginResult.Match(
                loginResult=> Ok(_mapper.Map<AuthenticationResponse>(loginResult)),
                errors=>Problem(errors));
    }

    public class SearchController : ControllerBase
    {
        private readonly ISearchEngine _searchEngine;

        public SearchController([FromBody] ISearchEngine searchEngine)
        {
            _searchEngine = searchEngine;
        }

        [HttpPost("Word")]
        public ActionResult<List<PageEntry>> Get(string word)
        {
            var results = _searchEngine.Search(word);
            if (results.Count == 0)
            {
                return NotFound("No entries found for the given word.");
            }
            return Ok(results);
        }
    }

}
