using recipeconfigurationservice.Model;
using recipeconfigurationservice.ETLClass.Interface;
using recipeconfigurationservice.ETLClass;
using System.Threading.Tasks;
namespace recipeconfigurationservice.ETLClass
{
    public abstract class TransformFactory
    {
        public static TransformFactory Instance(ExtractConfiguration extractConfiguration)
        {
            IHttpOtherApi httpOtherApi = new HttpOtherApi();
            IJson jsonService = new Json();
            
            switch(extractConfiguration.type)
            {
                case EType.Api:
                    return new TransformApi(extractConfiguration.apiConfiguration,httpOtherApi,jsonService);

                case EType.Sql:
                    return TransformSqlFactory.InstanceSQL(extractConfiguration,jsonService);
            }
            return null;

        }


        protected async Task<string> GetValueParameter(ExtractInParameter input,dynamic jsonExtract)
        {
            switch(input.type)
            {
                case ETypeParameter.Static:
                return input.value;

                case ETypeParameter.Dynamic:
                return await GetValueParameterDynamic(input,jsonExtract);

                default:
                return null;
            }
        }

        private async Task<string> GetValueParameterDynamic(ExtractInParameter input, dynamic jsonExtract)
        {
            string value = string.Empty;
            IJson jsonService = new Json();
            value = await jsonService.GetValuePath(jsonExtract.ToString(),input.path);

            return value;

        }
        public abstract Task<bool> Extract(ObjExtract objExtract);

        public abstract void Load();


    }
}