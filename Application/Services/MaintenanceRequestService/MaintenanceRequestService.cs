using Application.Repositories;
using Application.Services.MaintenanceRequestService.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace Application.Services.MaintenanceRequestService
{
    public class MaintenanceRequestService : IMaintenanceRequestService
    {
        private readonly IGenericRepository<MaintenanceRequest> _requestRepository;
        private readonly IGenericRepository<TechnicianCategory> _techCategoryRepository;

        public MaintenanceRequestService(IGenericRepository<MaintenanceRequest> requestRepository, IGenericRepository<TechnicianCategory> techCategoryRepository)
        {
            _requestRepository = requestRepository;
            _techCategoryRepository = techCategoryRepository;
        }

        public async Task CreateRequest(CreateRequestDto input)
        {
            var data = new MaintenanceRequest
            {
                Id = Guid.NewGuid(),
                Title = input.Title,
                Description = input.Description,
                Location = input.Location,
                EmployeeId = input.EmployeeId,
                CategoryId = input.CategoryId,
                Status = RequestStatus.New,
                CreatedAt = DateTime.UtcNow,

                RequestDetail = new RequestDetail
                {
                    Id = Guid.NewGuid(),
                    Location = input.Location, 
                    EmployeeNotes = input.Description, 
                    TechnicianNotes = "", 
                    ImageURL = null
                }

            };

            await _requestRepository.InsertAsync(data);
            await _requestRepository.SaveChangesAsync();
        }

        public async Task AssignTechnicianToRequest(Guid requestId, Guid technicianId)
        {
            var request = await _requestRepository.GetByIdAsync(requestId);
            if (request == null) throw new Exception("Request Not Found!");

            var isTechnicianInCategory = await _techCategoryRepository.GetAll()
                .AnyAsync(x => x.TechnicianId == technicianId && x.CategoryId == request.CategoryId);

            if (!isTechnicianInCategory)
            {
                throw new Exception("This technician cannot be assigned because his specialization does not match the order department!");
            }

            request.TechnicianId = technicianId;
            request.Status = RequestStatus.InProgress;

            await _requestRepository.UpdateAsync(request);
            await _requestRepository.SaveChangesAsync();
        }

        public async Task<List<GetAllRequestsDto>> GetAllRequests()
        {
            var data = await _requestRepository.GetAll()
                .Include(x => x.Employee)
                .Include(x => x.Technician)
                .Include(x => x.Category)
                .Select(x => new GetAllRequestsDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Location = x.Location,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    EmployeeName = x.Employee.Username,
                    TechnicianName = x.Technician != null ? x.Technician.Username : null,
                    CategoryName = x.Category.Name
                })
                .ToListAsync();

            return data;
        }

        public async Task<GetRequestByIdDto> GetRequestById(Guid Id)
        {
            var data = await _requestRepository.GetAll()
                .Include(x => x.Employee)
                .Include(x => x.Technician)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (data == null)
            {
                throw new Exception("Request Not Found!");
            }

            var result = new GetRequestByIdDto
            {
                Id = data.Id,
                Title = data.Title,
                Description = data.Description,
                Location = data.Location,
                Status = data.Status,
                CreatedAt = data.CreatedAt,
                EmployeeId = data.Employee.Id,
                TechnicianId = data.Technician != null ? data.Technician.Id : null,
                CategoryId = data.Category.Id
            };
            return result;
        }

        public async Task UpdateRequest(Guid Id, UpdateRequestDto input)
        {
            var data = await _requestRepository.GetByIdAsync(Id);
            if (data == null) throw new Exception("Request Not Found!");

            data.Title = input.Title;
            data.Description = input.Description;
            data.Location = input.Location;
            data.CategoryId = input.CategoryId;

            await _requestRepository.UpdateAsync(data);
            await _requestRepository.SaveChangesAsync();
        }

        public async Task UpdateTechnicianComment(Guid requestId, string techNotes)
        {
            var request = await _requestRepository.GetAll()
                .Include(x => x.RequestDetail)
                .FirstOrDefaultAsync(x => x.Id == requestId);

            if (request == null) throw new Exception("Request Not Found!");

            request.RequestDetail.TechnicianNotes = techNotes;

            await _requestRepository.UpdateAsync(request);
            await _requestRepository.SaveChangesAsync();
        }

        public async Task DeleteRequest(Guid Id)
        {
            var data = await _requestRepository.GetByIdAsync(Id);
            if (data == null)
            {
                throw new Exception("Request Not Found!");
            }

            _requestRepository.Delete(data);
            await _requestRepository.SaveChangesAsync();
        }
    }
}
