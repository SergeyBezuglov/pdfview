using ErrorOr;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PIMS.Domain.Common.Authentication.Configuration;
using PIMS.Infrastructure.Authentication;
using PIMS.Web.Common.Http;

namespace PIMS.Web.Controllers.Base
{
    /// <summary>
    /// API-контроллер на основе AD.
    /// </summary>
    [ApiController]
    [Authorize(AuthenticationSchemes = ActiveDirectoryAuthenticationModuleOption.SchemeName)]
    public class ADBasedApiController : BaseApiController
    {
        
    }
}
