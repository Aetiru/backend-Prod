using Backend.Contracts;
using Backend.Models;

namespace Backend.Interface
{
    public interface IRickServices
    {
        Task<(IEnumerable<Rick> Characters, int TotalCount)> GetAllAsync(int page = 1, int pageSize = 10, string searchTerm = "");
        Task<Rick> GetByIdAsync(Guid id);
        Task<Rick> CreateRickAsync(CreateRickRequest request);
        Task UpdateRickAsync(Guid id, UpdateRickRequest request);
        Task DeleteRickAsync(Guid id);
        Task<int> GetTotalCountAsync(string searchTerm = "");
    }
}
