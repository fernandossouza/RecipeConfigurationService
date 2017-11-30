using System.Threading.Tasks;

namespace recipeconfigurationservice.ETLClass.Interface
{
    public interface IJson
    {
         Task<string> GetValuePath(string jsonDynamic, string path);
    }
}