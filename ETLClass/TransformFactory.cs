using recipeconfigurationservice.Model;
using recipeconfigurationservice.ETLClass.Interface;
using recipeconfigurationservice.ETLClass;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace recipeconfigurationservice.ETLClass
{
    public abstract class TransformFactory
    {
        public static TransformFactory Instance(ExtractConfiguration extractConfiguration, LoadConfiguration loadConfiguration,string typeInstance)
        {
            IHttpOtherApi httpOtherApi = new HttpOtherApi();
            IJson jsonService = new Json();
            
            if(typeInstance.ToLower() == "extract")
            {
                switch(extractConfiguration.type)
                {
                    case EType.Api:
                        return new TransformApi(extractConfiguration.apiConfiguration,null,httpOtherApi,jsonService);

                    case EType.Sql:
                        return TransformSqlFactory.InstanceSQL(extractConfiguration,null,jsonService,typeInstance);
                }
                return null;
            }
            else if(typeInstance.ToLower() == "load")
            {
                switch(loadConfiguration.type)
                {
                    case EType.Api:
                        return new TransformApi(null,loadConfiguration.apiLoad,httpOtherApi,jsonService);

                    case EType.Sql:
                        return TransformSqlFactory.InstanceSQL(null,loadConfiguration,jsonService,typeInstance);
                }
                return null;
            }

            return null;

        }


        protected async Task<string> GetValueParameter(ETypeParameter typeParameter,string path, string value,dynamic jsonExtract)
        {
            switch(typeParameter)
            {
                case ETypeParameter.Static:
                return value;

                case ETypeParameter.Dynamic:
                return await GetValueParameterDynamic(path,jsonExtract);

                default:
                return null;
            }
        }

        private async Task<string> GetValueParameterDynamic(string path, dynamic jsonExtract)
        {
            string value = string.Empty;
            IJson jsonService = new Json();
            value = await jsonService.GetValuePath(jsonExtract.ToString(),path);

            return value;

        }
        public abstract Task<bool> Extract(ObjExtract objExtract);

        public abstract Task<bool> Load(Dictionary<string,string> dicExtract);


    }
}