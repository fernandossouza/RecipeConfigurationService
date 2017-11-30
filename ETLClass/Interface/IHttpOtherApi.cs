using System.Collections.Generic;
using System.Threading.Tasks;


namespace recipeconfigurationservice.ETLClass.Interface
{
    public interface IHttpOtherApi
    {
        Task<string> RestCommunication(string method, string url, dynamic json = null);
    }
}