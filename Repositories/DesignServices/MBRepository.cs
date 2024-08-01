using PartsInfoWebApi.core.DTOs.DesignServices;
using PartsInfoWebApi.core.Interfaces.DesignServices;
using PartsInfoWebApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartsInfoWebApi.infrastructure.Repositories.DesignServices
{
    public class MBRepository : IMBRepository
    {
        private readonly ApplicationDbContext _context;

        public MBRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MBDto>> GetMBDataAsync(int pageIndex, int pageSize)
        {
            return await Task.Run(() =>
            {
                return _context.MB
                    .Where(m => m.RefItem.StartsWith("D03-") && !string.IsNullOrEmpty(m.DwgNo) && m.DwgNo != "NONE")
                    .OrderBy(m => m.DwgNo)
                    .ThenBy(m => m.RefItem)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new MBDto
                    {
                        DwgNo = m.DwgNo,
                        RefItem = m.RefItem,
                        Size = m.SIZE,
                        CutType = m.CutType,
                        QtyPer = m.QtyPer,
                        Tested = m.TESTED,
                        Notes = m.NOTES
                    })
                    .ToList();
            });
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await Task.Run(() =>
            {
                return _context.MB
                    .Where(m => m.RefItem.StartsWith("D03-") && !string.IsNullOrEmpty(m.DwgNo) && m.DwgNo != "NONE")
                    .Count();
            });
        }
        public async Task<IEnumerable<MBDto>> GetMBDataByRefItemAsync(int pageIndex, int pageSize)
        {
            return await Task.Run(() =>
            {
                return _context.MB
                    .Where(m => m.RefItem.StartsWith("D03-"))
                    .OrderBy(m => m.RefItem)
                    .ThenBy(m => m.RefItem)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new MBDto
                    {
                        DwgNo = m.DwgNo,
                        RefItem = m.RefItem,
                        Size = m.SIZE,
                        CutType = m.CutType,
                        QtyPer = m.QtyPer,
                        Tested = m.TESTED,
                        Notes = m.NOTES
                    })
                    .ToList();
            });
        }

        public async Task<int> GetTotalCountByRefItemAsync()
        {
            return await Task.Run(() =>
            {
                return _context.MB
                    .Where(m => m.RefItem.StartsWith("D03-"))
                    .Count();
            });
        }
    }
}
