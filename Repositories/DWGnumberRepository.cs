using PartsInfoWebApi.Core.Interfaces;
using PartsInfoWebApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PartsInfoWebApi.Infrastructure.Repositories
{
    public class DWGnumberRepository : Repository<DWGnumbers>, IDWGnumberRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DWGnumberRepository> _logger;

        public DWGnumberRepository(ApplicationDbContext dbContext, ILogger<DWGnumberRepository> logger)
            : base(dbContext)
        {
            _context = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<DWGnumbers>> SearchAsync(string searchTerm)
        {
            var result = await _context.DWGnumbers
                .Where(t => t.NO.ToString().Contains(searchTerm))
                .OrderByDescending(t => t.NO)
                .ToListAsync();
            
            return result;
        }

        public async Task<DWGnumbers> GetFirstAsync()
        {
            var result = await _context.DWGnumbers
                .OrderByDescending(t => t.NO)
                .FirstOrDefaultAsync();
            
            return result;
        }

        public async Task<DWGnumbers> GetLastAsync()
        {
            var result = await _context.DWGnumbers
                .OrderBy(t => t.NO)
                .FirstOrDefaultAsync();
           
            return result;
        }

        public async Task<DWGnumbers> GetNextAsync(int currentNO)
        {
            var result = await _context.DWGnumbers
                .Where(t => t.NO < currentNO)
                .OrderByDescending(t => t.NO)
                .FirstOrDefaultAsync();
           
            return result;
        }

        public async Task<DWGnumbers> GetPreviousAsync(int currentNO)
        {
            var result = await _context.DWGnumbers
                .Where(t => t.NO > currentNO)
                .OrderBy(t => t.NO)
                .FirstOrDefaultAsync();
            
            return result;
        }

        public async Task<IEnumerable<DWGnumbers>> GetAllSortedAsync()
        {
            var result = await _context.DWGnumbers
                .OrderByDescending(t => t.NO)
                .ToListAsync();
            
            return result;
        }
    }
}
