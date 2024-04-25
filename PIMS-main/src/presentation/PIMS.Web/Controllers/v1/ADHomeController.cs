using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PIMS.Web.Controllers.Base;

namespace PIMS.Web.Controllers.v1
{
    /// <summary>
    /// Домашний контроллер AD.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ADHomeController : ADBasedApiController
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
        /// Инициализирует новый экземпляр класса <see cref="ADHomeController"/> .
        /// </summary>
        /// <param name="mediator">Посредник.</param>
        /// <param name="mapper">Картограф.</param>
        public ADHomeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        /// <summary>
        /// Профилирует <see cref="IActionResult"/>.
        /// </summary>
        /// <returns>Возвращение значения результата действия (IActionResult).</returns>
        [HttpGet("Profile")]
        public IActionResult Profile()
        {
            var t = HttpContext.User.Identity;

            return Ok(Array.Empty<string>());
        }
        

    }
}
