using Application.Services.RequestHistoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestHistoryController : ControllerBase
    {
        private readonly IRequestHistoryService _requestHistoryService;
        public RequestHistoryController(IRequestHistoryService requestHistoryService)
        {
            _requestHistoryService = requestHistoryService;
        }

        [HttpGet("GetRequestHistory")]
        public async Task<IActionResult> GetRequestHistory(Guid Id)
        {
            var data = await _requestHistoryService.GetRequestHistory(Id);
            return Ok(data);
        }
    }
}
