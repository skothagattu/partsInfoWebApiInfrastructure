using PartsInfoWebApi.Core.Interfaces;
using PartsInfoWebApi.Infrastructure.Repositories;
using PartsInfoWebApi.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PartsInfoWebApi.core.Models;

namespace PartsInfoWebApi.Infrastructure.Repositories
{
    public class D03numberRepository : Repository<D03numbers>, ID03numberRepository
    {
        private readonly ApplicationDbContext _context;
        public D03numberRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<D03numbers>> SearchAsync(string searchTerm)
        {
            var startsWithSearchTerm = _context.D03numbers
                .Where(t => t.ID.StartsWith(searchTerm))
                .OrderBy(t => t.ID);

            var containsSearchTerm = _context.D03numbers
                .Where(t => t.ID.Contains(searchTerm) && !t.ID.StartsWith(searchTerm))
                .OrderBy(t => t.ID);

            var result = await startsWithSearchTerm.Concat(containsSearchTerm).ToListAsync();
            return result;
        }

        public async Task<D03numbers> GetFirstAsync()
        {
            return await _context.D03numbers
                .OrderBy(t => t.ID)
                .FirstOrDefaultAsync();
        }

        public async Task<D03numbers> GetLastAsync()
        {
            return await _context.D03numbers
                .OrderByDescending(t => t.ID)
                .FirstOrDefaultAsync();
        }

        public async Task<D03numbers> GetNextAsync(string currentID)
        {
            return await _context.D03numbers
                .Where(t => string.Compare(t.ID, currentID) > 0)
                .OrderBy(t => t.ID)
                .FirstOrDefaultAsync();
        }

        public async Task<D03numbers> GetPreviousAsync(string currentID)
        {
            return await _context.D03numbers
                .Where(t => string.Compare(t.ID, currentID) < 0)
                .OrderByDescending(t => t.ID)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<D03numbers>> GetAllSortedAsync()
        {
            return await _context.D03numbers
                .OrderBy(t => t.ID)  // Fetch items in ascending order
                .ToListAsync();
        }
    }
}
