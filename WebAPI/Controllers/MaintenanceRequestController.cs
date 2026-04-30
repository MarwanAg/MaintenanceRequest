using Application.Services.MaintenanceRequestService;
using Application.Services.MaintenanceRequestService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceRequestController : ControllerBase
    {
        private readonly IMaintenanceRequestService _requestService;

        public MaintenanceRequestController(IMaintenanceRequestService requestService)
        {
            _requestService = requestService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests()
        {
            var result = await _requestService.GetAllRequests();
            return Ok(result);
        }

        [HttpGet("GetRequestById")]
        public async Task<IActionResult> GetRequestById(Guid id)
        {
            var result = await _requestService.GetRequestById(id);
            return Ok(result);
        }

        [HttpPost("CreateRequest")]
        public async Task<IActionResult> CreateRequest([FromForm]CreateRequestDto input)
        {
            await _requestService.CreateRequest(input);
            return Ok();
        }

        [HttpPut("UpdateRequest")]
        public async Task<IActionResult> UpdateRequest(Guid id, UpdateRequestDto input)
        {
            await _requestService.UpdateRequest(id, input);
            return Ok();
        }

        [HttpPut("UpdateTechnicianComment")]
        public async Task<IActionResult> UpdateTechnicianComment(Guid requestId, string techNotes)
        {
            await _requestService.UpdateTechnicianComment(requestId, techNotes);
            return Ok();
        }

        [HttpDelete("DeleteRequest")]
        public async Task<IActionResult> DeleteRequest(Guid id)
        {
            await _requestService.DeleteRequest(id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{requestId}/assign-technician/{technicianId}")]
        public async Task<IActionResult> AssignTechnician(Guid requestId, Guid technicianId)
        {
            await _requestService.AssignTechnicianToRequest(requestId, technicianId);
            return Ok();

        }
    }
}
