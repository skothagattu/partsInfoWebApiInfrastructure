using Microsoft.EntityFrameworkCore;
using PartsInfoWebApi.Core.Interfaces;
using PartsInfoWebApi.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartsInfoWebApi.Infrastructure.Repositories
{
    public class EcrLogRepository : Repository<EcrLog>, IEcrLogRepository
    {
        private readonly ApplicationDbContext _context;

        public EcrLogRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<EcrLog>> SearchAsync(string searchTerm)
        {
            return await _context.EcrLog
                .Where(e => e.DESC.Contains(searchTerm) || e.MODEL.Contains(searchTerm) || e.NAME.Contains(searchTerm) || e.ECO.Contains(searchTerm))
                .OrderByDescending(e => e.NO)
                .ToListAsync();
        }

        public async Task<EcrLog> GetFirstAsync()
        {
            return await _context.EcrLog
                .OrderByDescending(e => e.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<EcrLog> GetLastAsync()
        {
            return await _context.EcrLog
                .OrderBy(e => e.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<EcrLog> GetNextAsync(int currentNO)
        {
            return await _context.EcrLog
                .Where(e => e.NO < currentNO)
                .OrderByDescending(e => e.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<EcrLog> GetPreviousAsync(int currentNO)
        {
            return await _context.EcrLog
                .Where(e => e.NO > currentNO)
                .OrderBy(e => e.NO)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EcrLog>> GetAllSortedAsync()
        {
            return await _context.EcrLog
                .OrderByDescending(e => e.NO)
                .ToListAsync();
        }
    }
}
