using Application.Services.RequestHistoryService.DTOs;

namespace Application.Services.RequestHistoryService
{
    public interface IRequestHistoryService
    {
        Task<GetRequestHistoryDto> GetRequestHistory(Guid Id);
    }
}
