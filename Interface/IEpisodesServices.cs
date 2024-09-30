using Backend.Contracts;
using Backend.Models;

namespace Backend.Interface
{
    public interface IEpisodesServices
    {
        Task<(IEnumerable<Episode> Characters, int TotalCount)> GetAllAsync(int page = 1, int pageSize = 10, string searchTerm = "");
        Task<Episode> GetByIdAsync(Guid id);
        Task<Episode> CreateEpisodeAsync(CreateEpisodeRequest request);
        Task UpdateEpisodeAsync(Guid id, UpdateEpisodeRequest request);
        Task DeleteEpisodeAsync(Guid id);
        Task<int> GetTotalCountAsync(string searchTerm = "");
    }
}
