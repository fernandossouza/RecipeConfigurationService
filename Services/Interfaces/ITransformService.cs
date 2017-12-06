using System.Collections.Generic;
using System.Threading.Tasks;
using recipeconfigurationservice.Model;
namespace recipeconfigurationservice.Services.Interfaces
{
    public interface ITransformService
    {
        Task<IDictionary<string,string>> Extraction(int extractId, dynamic jsonExtract);
        Task<bool> Loading(int loadId, Dictionary<string,string> dicExtract);
    }
}