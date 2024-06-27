using PartsInfoWebApi.Core.Interfaces;
using PartsInfoWebApi.Core.Models;
using PartsInfoWebApi.Infrastructure.Repositories;
using PartsInfoWebApi.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PartsInfoWebApi.Infrastructure.Repositories
{
    public class DWGnumberRepository : Repository<DWGnumbers>, IDWGnumberRepository
    {
        private readonly ApplicationDbContext _context;
        public DWGnumberRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<DWGnumbers>> SearchAsync(string searchTerm)
        {
            var startsWithSearchTerm = _context.DWGnumbers
                .Where(t => t.NO.StartsWith(searchTerm))
                .OrderByDescending(t => t.NO);

            var containsSearchTerm = _context.DWGnumbers
                .Where(t => t.NO.Contains(searchTerm) && !t.NO.StartsWith(searchTerm))
                .OrderByDescending(t => t.NO);

            var result = await startsWithSearchTerm.Concat(containsSearchTerm).ToListAsync();
            return result;
        }

        public async Task<DWGnumbers> GetFirstAsync()
        {
            return await _context.DWGnumbers
                .OrderByDescending(t => t.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<DWGnumbers> GetLastAsync()
        {
            return await _context.DWGnumbers
                .OrderBy(t => t.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<DWGnumbers> GetNextAsync(string currentNO)
        {
            return await _context.DWGnumbers
                .Where(t => string.Compare(t.NO, currentNO) < 0)
                .OrderByDescending(t => t.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<DWGnumbers> GetPreviousAsync(string currentNO)
        {
            return await _context.DWGnumbers
                .Where(t => string.Compare(t.NO, currentNO) > 0)
                .OrderBy(t => t.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DWGnumbers>> GetAllSortedAsync()
        {
            return await _context.DWGnumbers
                .OrderByDescending(t => t.NO)  // Fetch items in descending order
                .ToListAsync();
        }
    }
}
