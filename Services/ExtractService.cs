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
    public class ExtractService : IExtractService
    {
        private readonly ApplicationDbContext _context;

        public ExtractService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Extract>> getExtracts(int startat, int quantity)
        {
            var extractId = await _context.Extracts
                     .OrderBy(x => x.extractId)
                     .Skip(startat).Take(quantity)
                     .Select(x => x.extractId)
                     .ToListAsync();
            List<Extract> extracts = new List<Extract>();
            foreach (var item in extractId)
            {
                var extract = await getExtract(item);
                if (extract != null)
                    extracts.Add(extract);
            }

            return extracts;
        }

        public async Task<Extract> getExtract(int extractId)
        {
            var extract = await _context.Extracts
                       .Include(x => x.extractConfiguration)
                         .ThenInclude(b=>b.apiConfiguration)
                             .ThenInclude(a=>a.input)
                    .Include(x => x.extractConfiguration)
                        .ThenInclude(x=>x.apiConfiguration)
                            .ThenInclude(x=>x.output)
                    .Include(x => x.extractConfiguration)
                        .ThenInclude(x=>x.sqlConfiguration)
                            .ThenInclude(x=>x.input)
                    .Include(x => x.extractConfiguration)
                        .ThenInclude(x=>x.sqlConfiguration)
                            .ThenInclude(x=>x.output)                     
                     .Where(x => x.extractId == extractId)
                     .FirstOrDefaultAsync();

                     //extract.extractConfiguration
            
            
            // var extractWithIncludes = _context.Entry(extract)
            //                         .Collection(x=>x.extractConfiguration)                                                                     
            //                         .LoadAsync();

                     
            
            return extract;
        }

        public async Task<Extract> addExtract(Extract extract)
        {
            _context.Extracts.Add(extract);
            await _context.SaveChangesAsync();
            return extract;
        }

        public async Task<Extract> updateExtract(int extractId, Extract extract)
        {
            var extractDB = await _context.Extracts
                    .Include(x => x.extractConfiguration)
                        .ThenInclude(x=>x.apiConfiguration)
                            .ThenInclude(x=>x.input)
                    .Include(x => x.extractConfiguration)
                        .ThenInclude(x=>x.apiConfiguration)
                            .ThenInclude(x=>x.output)
                    .Include(x => x.extractConfiguration)
                        .ThenInclude(x=>x.sqlConfiguration)
                            .ThenInclude(x=>x.input)
                    .Include(x => x.extractConfiguration)
                        .ThenInclude(x=>x.sqlConfiguration)
                            .ThenInclude(x=>x.output)
                     .Where(x => x.extractId == extractId )
                     .AsNoTracking()
                     .FirstOrDefaultAsync();


            if (extractId != extractDB.extractId && extractDB == null)
            {
                return null;
            }

            _context.Extracts.Update(extract);
            await _context.SaveChangesAsync();
            return extract;
        }

        public async Task<Extract> deleteExtract(int extractId)
        {
            var extractDB = await _context.Extracts
                    .Include(x => x.extractConfiguration)                    
                     .Where(x => x.extractId == extractId)
                     .FirstOrDefaultAsync();


            if (extractDB != null)
            {
               extractDB.enabled="false";
               await updateExtract(extractId,extractDB);
            }
            return extractDB;
        }

    }
}