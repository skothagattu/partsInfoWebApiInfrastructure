using PartsInfoWebApi.core.Interfaces.DesignServices;
using PartsInfoWebApi.core.Models.DesignServices;
using PartsInfoWebApi.Infrastructure.Repositories;
using PartsInfoWebApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PartsInfoWebApi.infrastructure.Repositories.DesignServices
{
    public class CMIDescRepository : Repository<CMI_DESC>, ICMIDescRepository
    {
        private readonly ApplicationDbContext _context;

        public CMIDescRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CMI_DESC>> SearchAsync(string searchTerm, int pageIndex, int pageSize)
        {
            return await _context.CMI_DESC
                .Where(d => string.IsNullOrEmpty(searchTerm) || d.CMINO.Contains(searchTerm))
                .OrderBy(d => d.CMINO)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

    }
}
