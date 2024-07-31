using PartsInfoWebApi.core.DTOs.DesignServices;
using PartsInfoWebApi.Infrastructure;
using PartsInfoWebApi.core.Interfaces.DesignServices;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartsInfoWebApi.infrastructure.Repositories.DesignServices
{
    public class CMIDescVendorRepository : ICMIDescVendorRepository
    {
        private readonly ApplicationDbContext _context;

        public CMIDescVendorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CMIDescVendorDto>> GetCMIDescVendorDataAsync(int pageIndex, int pageSize)
        {
            var query = from desc in _context.CMI_DESC
                        join vendor in _context.CMI_VENDOR on desc.CMINO equals vendor.CMINO into descVendor
                        from dv in descVendor.DefaultIfEmpty()
                        orderby desc.CMINO
                        select new CMIDescVendorDto
                        {
                            CMINO = desc.CMINO,
                            DESCRIPTION = desc.DESCRIPTION,
                            CODE = dv.CODE,
                            MFGNO = dv.MFGNO,
                            SUB = dv.SUB,
                            DATE = dv.DATE,
                            NOTES = dv.NOTES
                        };

            return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<int> GetTotalCountAsync()
        {
            var query = from desc in _context.CMI_DESC
                        join vendor in _context.CMI_VENDOR on desc.CMINO equals vendor.CMINO into descVendor
                        from dv in descVendor.DefaultIfEmpty()
                        select desc;

            return await query.CountAsync();
        }
        public async Task<IEnumerable<CMIDescVendorDto>> GetCMIDescVendorByMfgNoAsync(int pageIndex, int pageSize)
        {
            var query = from desc in _context.CMI_DESC
                        join vendor in _context.CMI_VENDOR on desc.CMINO equals vendor.CMINO into descVendor
                        from dv in descVendor.DefaultIfEmpty()
                        where !string.IsNullOrEmpty(dv.MFGNO)
                        orderby dv.MFGNO
                        select new CMIDescVendorDto
                        {
                            CMINO = desc.CMINO,
                            DESCRIPTION = desc.DESCRIPTION,
                            CODE = dv.CODE,
                            MFGNO = dv.MFGNO,
                            SUB = dv.SUB
                        };

            return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<int> GetTotalCountByMfgNoAsync()
        {
            var query = from desc in _context.CMI_DESC
                        join vendor in _context.CMI_VENDOR on desc.CMINO equals vendor.CMINO into descVendor
                        from dv in descVendor.DefaultIfEmpty()
                        where !string.IsNullOrEmpty(dv.MFGNO)
                        select desc;

            return await query.CountAsync();
        }

        public async Task<IEnumerable<CMIDescVendorDto>> GetCMIDescVendorByDateAsync(DateTime date, int pageIndex, int pageSize)
        {
            var query = from desc in _context.CMI_DESC
                        join vendor in _context.CMI_VENDOR
                            on desc.CMINO equals vendor.CMINO into vendorGroup
                        from v in vendorGroup.DefaultIfEmpty()
                        where v.DATE != null && v.DATE > date
                        orderby desc.CMINO
                        select new CMIDescVendorDto
                        {
                            CMINO = desc.CMINO,
                            DESCRIPTION = desc.DESCRIPTION,
                            CODE = v.CODE,
                            MFGNO = v.MFGNO,
                            SUB = v.SUB,
                            DATE = v.DATE,
                            NOTES = v.NOTES
                        };

            return await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountByDateAsync(DateTime date)
        {
            var query = from desc in _context.CMI_DESC
                        join vendor in _context.CMI_VENDOR
                            on desc.CMINO equals vendor.CMINO into vendorGroup
                        from v in vendorGroup.DefaultIfEmpty()
                        where v.DATE != null && v.DATE > date
                        select desc;

            return await query.CountAsync();
        }

        public async Task<IEnumerable<CMIDescVendorDto>> GetCMIDescVendorByDateAsyncMfgNo(DateTime date, int pageIndex, int pageSize)
        {
            var query = from desc in _context.CMI_DESC
                        join vendor in _context.CMI_VENDOR
                            on desc.CMINO equals vendor.CMINO into vendorGroup
                        from v in vendorGroup.DefaultIfEmpty()
                        where v.DATE != null && v.DATE > date
                        orderby v.MFGNO
                        select new CMIDescVendorDto
                        {
                            CMINO = desc.CMINO,
                            DESCRIPTION = desc.DESCRIPTION,
                            CODE = v.CODE,
                            MFGNO = v.MFGNO,
                            SUB = v.SUB,
                            DATE = v.DATE,
                            NOTES = v.NOTES
                        };

            return await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountByDateAsyncMfgNo(DateTime date)
        {
            var query = from desc in _context.CMI_DESC
                        join vendor in _context.CMI_VENDOR
                            on desc.CMINO equals vendor.CMINO into vendorGroup
                        from v in vendorGroup.DefaultIfEmpty()
                        where v.DATE != null && v.DATE > date && v.MFGNO != null
                        select desc;

            return await query.CountAsync();
        }


    }
}
