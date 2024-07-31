using PartsInfoWebApi.core.Interfaces.DesignServices;
using PartsInfoWebApi.core.Models.DesignServices;
using PartsInfoWebApi.Infrastructure.Repositories;
using PartsInfoWebApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PartsInfoWebApi.infrastructure.Repositories.DesignServices
{
    public class StdPartIndexRepository : Repository<StdPartIndex>, IStdPartIndexRepository
    {
        private readonly ApplicationDbContext _context;

        public StdPartIndexRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StdPartIndex>> GetStdPartIndexesAsync(int pageIndex, int pageSize)
        {
            return await Task.Run(() => _context.StdPartIndex
                .OrderBy(s => s.Number)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList());
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await Task.Run(() => _context.StdPartIndex
                .Count());
        }
        public async Task<IEnumerable<StdPartIndex>> GetStdPartIndexesAsyncByTitle(int pageIndex, int pageSize)
        {
            return await Task.Run(() => _context.StdPartIndex
                .Where(s => s.Title != null && s.Title !="")
                .OrderBy(s => s.Title)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList());
        }

        public async Task<int> GetTotalCountAsyncByTitle()
        {
            return await Task.Run(() => _context.StdPartIndex
                .Where(s => s.Title != null && s.Title != "")
                .Count());
        }
    }
}
