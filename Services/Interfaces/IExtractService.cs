using System.Collections.Generic;
using System.Threading.Tasks;
using recipeconfigurationservice.Model;

namespace recipeconfigurationservice.Services.Interfaces
{
    public interface IExtractService
    {
        Task<List<Extract>> getExtracts(int start,int quantity);
        Task<Extract> getExtract(long extractId);

         Task<Extract> addExtract(Extract extract);

         Task<Extract> updateExtract(long extractId,Extract extract);
         Task<Extract> deleteExtract(long extractId);

        
    }
}