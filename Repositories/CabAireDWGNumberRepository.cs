using PartsInfoWebApi.Core.Models;
using PartsInfoWebApi.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PartsInfoWebApi.core.Models;

namespace PartsInfoWebApi.Infrastructure.Repositories
{
    public class CabAireDWGNumberRepository : Repository<CabAireDWGNumber>, ICabAireDWGNumberRepository
    {
        private readonly ApplicationDbContext _context;
        public CabAireDWGNumberRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<CabAireDWGNumber>> SearchAsync(string searchTerm)
        {
            var startsWithSearchTerm = _context.CabAireDWGNumbers
                .Where(t => t.NO.ToString().StartsWith(searchTerm))
                .OrderByDescending(t => t.NO);

            var containsSearchTerm = _context.CabAireDWGNumbers
                .Where(t => t.NO.ToString().Contains(searchTerm) && !t.NO.ToString().StartsWith(searchTerm))
                .OrderByDescending(t => t.NO);

            var result = await startsWithSearchTerm.Concat(containsSearchTerm).ToListAsync();
            return result;
        }

        public async Task<CabAireDWGNumber> GetFirstAsync()
        {
            var result = await _context.CabAireDWGNumbers
                .OrderBy(c => c.NO)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<CabAireDWGNumber> GetLastAsync()
        {
            var result = await _context.CabAireDWGNumbers
                .OrderBy(t => t.NO)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<CabAireDWGNumber> GetNextAsync(int currentNO)
        {
            var result = await _context.CabAireDWGNumbers
                .Where(t => t.NO < currentNO)
                .OrderByDescending(t => t.NO)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<CabAireDWGNumber> GetPreviousAsync(int currentNO)
        {
            var result = await _context.CabAireDWGNumbers
                .Where(t => t.NO > currentNO)
                .OrderBy(t => t.NO)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<CabAireDWGNumber>> GetAllSortedAsync()
        {
            var result = await _context.CabAireDWGNumbers
                .OrderByDescending(t => t.NO)
                .ToListAsync();
            return result;
        }
    }
}
