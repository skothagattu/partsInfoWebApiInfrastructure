using PartsInfoWebApi.core.DTOs.DesignServices;
using PartsInfoWebApi.core.Interfaces.DesignServices;
using PartsInfoWebApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartsInfoWebApi.infrastructure.Repositories.DesignServices
{
    public class PromRepository : IPromRepository
    {
        private readonly ApplicationDbContext _context;

        public PromRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PromListingDto>> GetPromListingAsync(int pageIndex, int pageSize)
        {
            return await Task.Run(() =>
            {
                var query = (from pi in _context.PromInfo
                             join pce in _context.PromCurrentEco
                             on pi.PromNo equals pce.PromNo into gj
                             from subpce in gj.DefaultIfEmpty()
                             select new PromListingDto
                             {
                                 PromNo = pi.PromNo,
                                 Rev = subpce != null ? subpce.REV : null,
                                 Description = pi.Description,
                                 Loc = pi.Loc,
                                 TypeA= pi.TypeA,
                                 Qty = pi.Qty,
                                 TypeB= pi.TypeB,
                                 QtyB= pi.QtyB

                             })
                            .Distinct()
                            .OrderBy(x => x.PromNo)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

                return query;
            });
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await Task.Run(() =>
            {
                var query = (from pi in _context.PromInfo
                             join pce in _context.PromCurrentEco
                             on pi.PromNo equals pce.PromNo into gj
                             from subpce in gj.DefaultIfEmpty()
                             select new { pi.PromNo })
                            .Distinct()
                            .Count();

                return query;
            });
        }

    }
}
