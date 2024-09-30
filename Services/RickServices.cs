using System.Data;
using AutoMapper;
using Backend.Contracts;
using Backend.Interface;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.Services
{

    public class RickServices : IRickServices
    {
        private readonly RickDbContext _context;
        private readonly ILogger<RickServices> _logger;
        private readonly IMapper _mapper;

        public RickServices(RickDbContext context, ILogger<RickServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Rick> CreateRickAsync(CreateRickRequest request)
        {
            try
            {
                var rick = _mapper.Map<Rick>(request);
                // Asigna las fechas aquí si es necesario
                rick.Created = DateTime.UtcNow;
                rick.CreatedAt = DateTime.UtcNow;
                rick.UpdatedAt = DateTime.UtcNow;

                await _context.Ricks.AddAsync(rick);
                await _context.SaveChangesAsync();

                return rick;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the Todo item.");
                throw new Exception("An error occurred while creating the Todo item.");
            }
        }

        public async Task DeleteRickAsync(Guid id)
        {
            try
            {
                var rick = await _context.Ricks.FindAsync(id);
                if (rick == null)
                {
                    throw new Exception($"Rick with id {id} not found");
                }

                _context.Ricks.Remove(rick);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting Rick with id {id}");
                throw new Exception($"An error occurred while deleting Rick with id {id}");
            }
        }

        public async Task<(IEnumerable<Rick> Characters, int TotalCount)> GetAllAsync(int page = 1, int pageSize = 10, string searchTerm = "")
        {
            var query = _context.Ricks.AsQueryable();

            // Aplicar filtro de búsqueda solo si se proporciona un término
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                //Console.WriteLine($"Search term: {searchTerm}"); // Verifica el valor
                query = query.Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalCount = await query.CountAsync();

            var characters = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (characters, totalCount);
        }

        public async Task<int> GetTotalCountAsync(string searchTerm = "")
        {
            var query = _context.Ricks.AsQueryable();



            // Aplicar filtro de búsqueda solo si se proporciona un término
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {

                query = query.Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return await query.CountAsync();
        }

        public async Task<Rick> GetByIdAsync(Guid id)
        {
            try
            {
                var rick = await _context.Ricks.FindAsync(id);
                if (rick == null)
                {
                    throw new Exception($"Rick with id {id} not found");
                }
                return rick;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Rick with id {id}");
                throw new Exception($"An error occurred while getting Rick with id {id}");
            }
        }

        public async Task UpdateRickAsync(Guid id, UpdateRickRequest request)
        {
            try
            {
                var rick = await _context.Ricks.FindAsync(id);
                if (rick == null)
                {
                    throw new Exception($"Rick with id {id} not found");
                }

                _mapper.Map(request, rick);

                rick.UpdatedAt = DateTime.UtcNow;

                _context.Ricks.Update(rick);

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