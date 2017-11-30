using recipeconfigurationservice.Model;
using recipeconfigurationservice.ETLClass.Interface;
using recipeconfigurationservice.ETLClass;
namespace recipeconfigurationservice.ETLClass
{
    public abstract class TransformFactory
    {
        public static TransformFactory Instance(ExtractConfiguration extractConfiguration)
        {
            IHttpOtherApi httpOtherApi = new HttpOtherApi();
            IJson jsonService = new Json();
            
            if(extractConfiguration.type == EType.Api)
            {
                return new TransformApi(extractConfiguration.apiConfiguration,httpOtherApi,jsonService);
            }
            return null;

        }


        protected string GetValueParameter(ExtractInParameter input,dynamic jsonExtract)
        {
            switch(input.type)
            {
                case ETypeParameter.Static:
                return input.value;

                case ETypeParameter.Dynamic:
                return GetValueParameterDynamic(input,jsonExtract);

                default:
                return null;
            }
        }

        private string GetValueParameterDynamic(ExtractInParameter input, dynamic jsonExtract)
        {
            return "Falta Implementar";

        }
        public abstract void Extract(ObjExtract objExtract);

        public abstract void Load();


    }
}