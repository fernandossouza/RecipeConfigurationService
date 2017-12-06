using System.Collections.Generic;
using System.Threading.Tasks;
using recipeconfigurationservice.Model;
namespace recipeconfigurationservice.Services.Interfaces
{
    public interface ILoadService
    {
        Task<List<Load>> getLoads(int start,int quantity);
        Task<Load> getLoad(int loadId);

         Task<Load> addLoad(Load load);

         Task<Load> updateLoad(int loadId,Load load);
         Task<Load> deleteLoad(int loadId);
        Task<List<Load>> getLoadsPerExtractId(int extractId);
    }
}