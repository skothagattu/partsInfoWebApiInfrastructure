using PartsInfoWebApi.Core.Models;
using PartsInfoWebApi.Core.Interfaces;
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
                .Where(t => t.ID.ToString().StartsWith(searchTerm))
                .OrderByDescending(t => t.ID);

            var containsSearchTerm = _context.D03numbers
                .Where(t => t.ID.ToString().Contains(searchTerm) && !t.ID.ToString().StartsWith(searchTerm))
                .OrderByDescending(t => t.ID);

            var result = await startsWithSearchTerm.Concat(containsSearchTerm).ToListAsync();
            return result;
        }

        public async Task<D03numbers> GetFirstAsync()
        {
            return await _context.D03numbers
                .OrderByDescending(t => t.ID)
                .FirstOrDefaultAsync();
        }

        public async Task<D03numbers> GetLastAsync()
        {
            return await _context.D03numbers
                .OrderBy(t => t.ID)
                .FirstOrDefaultAsync();
        }

        public async Task<D03numbers> GetNextAsync(int currentID)
        {
            return await _context.D03numbers
                .Where(t => t.ID < currentID)
                .OrderByDescending(t => t.ID)
                .FirstOrDefaultAsync();
        }

        public async Task<D03numbers> GetPreviousAsync(int currentID)
        {
            return await _context.D03numbers
                .Where(t => t.ID > currentID)
                .OrderBy(t => t.ID)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<D03numbers>> GetAllSortedAsync()
        {
            return await _context.D03numbers
                .OrderByDescending(t => t.ID)  // Fetch items in reverse order
                .ToListAsync();
        }
    }
}
