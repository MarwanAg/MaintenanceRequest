using Application.Repositories;
using Application.Services.RequestHistoryService.DTOs;
using Domain.Entities;

namespace Application.Services.RequestHistoryService
{
    public class RequestHistoryService : IRequestHistoryService
    {
        private readonly IGenericRepository<RequestHistory> _genericRepository;
        public RequestHistoryService(IGenericRepository<RequestHistory> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<GetRequestHistoryDto> GetRequestHistory(Guid Id)
        {
            var data = await _genericRepository.GetByIdAsync(Id);
            var result = new GetRequestHistoryDto
            {
                Id = data.Id,
                UserId = data.UserId,
                RequestId = data.RequestId,
                OldStatus = data.OldStatus,
                NewStatus = data.NewStatus,
                Comment = data.Comment,
                CreatedAt = data.CreatedAt
            };
            return result;
        }
    }
}
