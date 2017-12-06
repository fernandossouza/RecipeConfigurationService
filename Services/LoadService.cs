using System;
using System.Collections.Generic;
using System.Linq;
using recipeconfigurationservice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using recipeconfigurationservice.Data;
using recipeconfigurationservice.Model;
using System.Threading.Tasks;
namespace recipeconfigurationservice.Services
{
    public class LoadService :ILoadService
    {
        private readonly ApplicationDbContext _context;

        public LoadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Load>> getLoads(int startat, int quantity)
        {
            var loadId = await _context.Loads
                     .OrderBy(x => x.loadId)
                     .Skip(startat).Take(quantity)
                     .Select(x => x.loadId)
                     .ToListAsync();
            List<Load> loads = new List<Load>();
            foreach (var item in loadId)
            {
                var load = await getLoad(item);
                if (load != null)
                    loads.Add(load);
            }

            return loads;
        }

         public async Task<List<Load>> getLoadsPerExtractId(int extractId)
        {
            var loadId = await _context.Loads
                     .Where(x=>x.extractId == extractId)
                     .OrderBy(x => x.loadId)
                     .Select(x => x.loadId)
                     .ToListAsync();
            List<Load> loads = new List<Load>();
            foreach (var item in loadId)
            {
                var load = await getLoad(item);
                if (load != null)
                    loads.Add(load);
            }

            return loads;
        }

        public async Task<Load> getLoad(int loadId)
        {
            var load = await _context.Loads
                       .Where(x=>x.loadId == loadId)
                       .Include(x => x.loadConfiguration)
                         .ThenInclude(b=>b.sqlLoad)
                             .ThenInclude(a=>a.parameterLoad)
                    .Include(x => x.loadConfiguration)
                        .ThenInclude(x=>x.apiLoad)
                            .ThenInclude(x=>x.parameterLoad)                    
                     .FirstOrDefaultAsync();

            
            return load;
        }

        public async Task<Load> addLoad(Load load)
        {
            _context.Loads.Add(load);
            await _context.SaveChangesAsync();
            return load;
        }

        public async Task<Load> updateLoad(int loadId, Load load)
        {
             var loadDb = await _context.Loads
                    .Include(x => x.loadConfiguration)
                         .ThenInclude(b=>b.sqlLoad)
                             .ThenInclude(a=>a.parameterLoad)
                    .Include(x => x.loadConfiguration)
                        .ThenInclude(x=>x.apiLoad)
                            .ThenInclude(x=>x.parameterLoad)
                    .AsNoTracking()                    
                    .FirstOrDefaultAsync();


            if (loadId != loadDb.loadId && loadDb == null)
            {
                return null;
            }

            _context.Loads.Update(load);
            await _context.SaveChangesAsync();
            return load;
        }

        public async Task<Load> deleteLoad(int loadId)
        {
            var loadDB = await _context.Loads
                    .Include(x => x.loadConfiguration)                    
                     .Where(x => x.loadId == loadId)
                     .FirstOrDefaultAsync();


            if (loadDB != null)
            {
                _context.Entry(loadDB).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            return loadDB;
        }
  
    }
}