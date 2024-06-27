using PartsInfoWebApi.core.Interfaces;
using PartsInfoWebApi.core.Models;
using PartsInfoWebApi.Infrastructure.Repositories;
using PartsInfoWebApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PartsInfoWebApi.infrastructure.Repositories
{
    public class SubLogRepository : Repository<SubLog>, ISubLogRepository
    {
        private readonly ApplicationDbContext _context;
        public SubLogRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<SubLog>> SearchAsync(string searchTerm)
        {
            var startsWithSearchTerm = _context.SubLog
                .Where(t => t.NO.StartsWith(searchTerm))
                .OrderBy(t => t.NO);

            var containsSearchTerm = _context.SubLog
                .Where(t => t.NO.Contains(searchTerm) && !t.NO.StartsWith(searchTerm))
                .OrderBy(t => t.NO);

            var result = await startsWithSearchTerm.Concat(containsSearchTerm).ToListAsync();
            return result;
        }

        public async Task<SubLog> GetFirstAsync()
        {
            return await _context.SubLog
                .OrderBy(t => t.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<SubLog> GetLastAsync()
        {
            return await _context.SubLog
                .OrderByDescending(t => t.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<SubLog> GetNextAsync(string currentNO)
        {
            return await _context.SubLog
                .Where(t => string.Compare(t.NO, currentNO) > 0)
                .OrderBy(t => t.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<SubLog> GetPreviousAsync(string currentNO)
        {
            return await _context.SubLog
                .Where(t => string.Compare(t.NO, currentNO) < 0)
                .OrderByDescending(t => t.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SubLog>> GetAllSortedAsync()
        {
            return await _context.SubLog
                .OrderBy(t => t.NO)
                .ToListAsync();
        }
    }

}
