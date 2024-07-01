using PartsInfoWebApi.Core.Interfaces;
using PartsInfoWebApi.Core.Models;
using PartsInfoWebApi.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PartsInfoWebApi.Infrastructure.Repositories
{
    public class EcoLogRepository : Repository<EcoLog>, IEcoLogRepository
    {
        private readonly ApplicationDbContext _context;

        public EcoLogRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

       public async Task<IEnumerable<EcoLog>> SearchAsync(string searchTerm)
        {
            var startsWithSearchTerm = _context.EcoLog
                .Where(t => t.NO.ToString().StartsWith(searchTerm) )
                .OrderByDescending(t => t.NO);

            var containsSearchTerm = _context.EcoLog
                .Where(t => t.NO.ToString().Contains(searchTerm) )
                .OrderByDescending(t => t.NO);

            var result = await startsWithSearchTerm.Concat(containsSearchTerm).ToListAsync();
            return result.Distinct().ToList();
        }

        public async Task<EcoLog> GetFirstAsync()
        {
            return await _context.EcoLog
                .OrderByDescending(t => t.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<EcoLog> GetLastAsync()
        {
            return await _context.EcoLog
                .OrderBy(t => t.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<EcoLog> GetNextAsync(int currentNO)
        {
            return await _context.EcoLog
                .Where(t => t.NO < currentNO)
                .OrderByDescending(t => t.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<EcoLog> GetPreviousAsync(int currentNO)
        {
            return await _context.EcoLog
                .Where(t => t.NO > currentNO)
                .OrderBy(t => t.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EcoLog>> GetAllSortedAsync()
        {
            return await _context.EcoLog
                .OrderByDescending(t => t.NO)
                .ToListAsync();
        }
    }
}
