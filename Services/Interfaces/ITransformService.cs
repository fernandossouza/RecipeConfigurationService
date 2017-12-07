using System.Collections.Generic;
using System.Threading.Tasks;
using recipeconfigurationservice.Model;
namespace recipeconfigurationservice.Services.Interfaces
{
    public interface ITransformService
    {
        Task<IDictionary<string,string>> Extraction(int extractId, dynamic jsonExtract);
        Task<List<Task<string>>> Loading(int extractId, Dictionary<string,string> dicExtract);
    }
}