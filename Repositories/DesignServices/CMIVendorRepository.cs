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
    public class CMIVendorRepository : Repository<CMI_VENDOR>, ICMIVendorRepository
    {
        private readonly ApplicationDbContext _context;

        public CMIVendorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CMI_VENDOR>> SearchAsync(string searchTerm, int pageIndex, int pageSize)
        {
            return await _context.CMI_VENDOR
                .Where(v => string.IsNullOrEmpty(searchTerm) || v.CMINO.Contains(searchTerm))
                .OrderBy(v => v.CMINO)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            /*return await _context.CMI_VENDOR
                .Where(d => d.CMINO.Contains(searchTerm))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();*/
        }
/*
        public async Task<IEnumerable<CMI_VENDOR>> GetAllAsync()
        {
            return await _context.CMI_VENDOR
                .OrderBy(v => v.CMINO)
                .ToListAsync();
        }*/
    }
}
