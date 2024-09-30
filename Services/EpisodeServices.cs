using System.Data;
using AutoMapper;
using Backend.Contracts;
using Backend.Interface;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.Services
{

    public class EpisodeServices : IEpisodesServices
    {
        private readonly RickDbContext _context;
        private readonly ILogger<EpisodeServices> _logger;
        private readonly IMapper _mapper;

        public EpisodeServices(RickDbContext context, ILogger<EpisodeServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Episode> CreateEpisodeAsync(CreateEpisodeRequest request)
        {
            try
            {
                var episode = _mapper.Map<Episode>(request);
                // Asigna las fechas aquí si es necesario
                episode.Created = DateTime.UtcNow;

                await _context.Episodes.AddAsync(episode);
                await _context.SaveChangesAsync();

                return episode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the Todo item.");
                throw new Exception("An error occurred while creating the Todo item.");
            }
        }

        public async Task DeleteEpisodeAsync(Guid id)
        {
            try
            {
                var episode = await _context.Episodes.FindAsync(id);
                if (episode == null)
                {
                    throw new Exception($"Rick with id {id} not found");
                }

                _context.Episodes.Remove(episode);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting Rick with id {id}");
                throw new Exception($"An error occurred while deleting Rick with id {id}");
            }
        }

        public async Task<(IEnumerable<Episode> Characters, int TotalCount)> GetAllAsync(int page = 1, int pageSize = 10, string searchTerm = "")
        {
            var query = _context.Episodes.AsQueryable();

            // Aplicar filtro de búsqueda solo si se proporciona un término
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                //Console.WriteLine($"Search term: {searchTerm}"); // Verifica el valor
                query = query.Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalCount = await query.CountAsync();

            var episodes = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (episodes, totalCount);
        }

        public async Task<int> GetTotalCountAsync(string searchTerm = "")
        {
            var query = _context.Episodes.AsQueryable();



            // Aplicar filtro de búsqueda solo si se proporciona un término
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {

                query = query.Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return await query.CountAsync();
        }

        public async Task<Episode> GetByIdAsync(Guid id)
        {
            try
            {
                var episode = await _context.Episodes.FindAsync(id);
                if (episode == null)
                {
                    throw new Exception($"Rick with id {id} not found");
                }
                return episode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting episode with id {id}");
                throw new Exception($"An error occurred while getting episode with id {id}");
            }
        }

        public async Task UpdateEpisodeAsync(Guid id, UpdateEpisodeRequest request)
        {
            try
            {
                var episode = await _context.Episodes.FindAsync(id);
                if (episode == null)
                {
                    throw new Exception($"Rick with id {id} not found");
                }

                _mapper.Map(request, episode);


                _context.Episodes.Update(episode);

                // Use a transaction to ensure data consistency
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    throw new DBConcurrencyException($"Concurrency error while updating Rick with id {id}");
                }
                catch (DbUpdateException ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, $"Database error occurred while updating Rick with id {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while updating Rick with id {id}");
                throw new Exception($"An unexpected error occurred while updating Rick with id {id}", ex);
            }
        }
    }
}