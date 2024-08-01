using PartsInfoWebApi.core.DTOs.DesignServices;
using PartsInfoWebApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartsInfoWebApi.core.Interfaces.DesignServices
{
    public class PromModelRepository : IPromModelRepository
    {
        private readonly ApplicationDbContext _context;

        public PromModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PromModelDto>> GetPromModelsByPromNoAsync(int pageIndex, int pageSize)
        {
            return await Task.Run(() =>
            {
                return _context.PromModel
                    .OrderBy(pm => pm.PromNo)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(pm => new PromModelDto
                    {
                        PromNo = pm.PromNo,
                        Model = pm.Model
                    })
                    .ToList();
            });
        }

        public async Task<IEnumerable<PromModelDto>> GetPromModelsByModelAsync(int pageIndex, int pageSize)
        {
            return await Task.Run(() =>
            {
                return _context.PromModel
                    .OrderBy(pm => pm.Model)
                    .ThenBy(pm => pm.PromNo)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(pm => new PromModelDto
                    {
                        PromNo = pm.PromNo,
                        Model = pm.Model
                    })
                    .ToList();
            });
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await Task.Run(() => _context.PromModel.Count());
        }
    }
}
