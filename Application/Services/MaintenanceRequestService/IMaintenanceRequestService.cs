using Application.Services.MaintenanceRequestService.DTOs;

namespace Application.Services.MaintenanceRequestService
{
    public interface IMaintenanceRequestService
    {
        Task<List<GetAllRequestsDto>> GetAllRequests();
        Task<GetRequestByIdDto> GetRequestById(Guid Id);
        Task CreateRequest(CreateRequestDto input);
        Task UpdateRequest(Guid Id,UpdateRequestDto input);
        Task AssignTechnicianToRequest(Guid requestId, Guid technicianId);
        Task UpdateTechnicianComment(Guid requestId, string techNotes);
        Task DeleteRequest(Guid Id);

    }
}
