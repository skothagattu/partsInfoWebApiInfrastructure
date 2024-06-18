using Microsoft.EntityFrameworkCore;
using PartsInfoWebApi.Infrastructure;
using PartsInfoWebApi.Interfaces;
using PartsInfoWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartsInfoWebApi.Infrastructure.Repositories
{
    public class ThreeLetterCodeRepository : Repository<ThreeLetterCode>, IThreeLetterCodeRepository
    {
        private readonly ApplicationDbContext _context;
        public ThreeLetterCodeRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<ThreeLetterCode>> SearchAsync(string searchTerm)
        {
            var startsWithSearchTerm = _context.ThreeLetterCode
                .Where(t => t.CODE.StartsWith(searchTerm))
                .OrderBy(t => t.CODE);

            var containsSearchTerm = _context.ThreeLetterCode
                .Where(t => t.CODE.Contains(searchTerm) && !t.CODE.StartsWith(searchTerm))
                .OrderBy(t => t.CODE);

            var result = await startsWithSearchTerm.Concat(containsSearchTerm).ToListAsync();
            return result;
        }

        public async Task<ThreeLetterCode> GetFirstAsync()
        {
            return await _context.ThreeLetterCode
                .OrderBy(t => t.CODE)
                .FirstOrDefaultAsync();
        }

        public async Task<ThreeLetterCode> GetLastAsync()
        {
            return await _context.ThreeLetterCode
                .OrderByDescending(t => t.CODE)
                .FirstOrDefaultAsync();
        }

        public async Task<ThreeLetterCode> GetNextAsync(string currentCode)
        {
            return await _context.ThreeLetterCode
                .Where(t => string.Compare(t.CODE, currentCode) > 0)
                .OrderBy(t => t.CODE)
                .FirstOrDefaultAsync();
        }

        public async Task<ThreeLetterCode> GetPreviousAsync(string currentCode)
        {
            return await _context.ThreeLetterCode
                .Where(t => string.Compare(t.CODE, currentCode) < 0)
                .OrderByDescending(t => t.CODE)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ThreeLetterCode>> GetAllSortedAsync()
        {
            return await _context.ThreeLetterCode
                .OrderBy(t => t.CODE)
                .ToListAsync();
        }
    }
}
