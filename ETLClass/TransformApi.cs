using System;
using System.Collections;
using System.Web;
using System.Threading.Tasks;
using recipeconfigurationservice.Model;
using recipeconfigurationservice.ETLClass.Interface;
using System.Collections.Generic;

namespace recipeconfigurationservice.ETLClass
{
    public class TransformApi : TransformFactory
    {
        private ApiConfiguration _apiConfiguration;
        private dynamic _jsonExtract;
        private readonly IHttpOtherApi _httpOtherApi;

        private readonly IJson _json;

        public TransformApi (ApiConfiguration apiConfiguration, IHttpOtherApi httpOtherApi, IJson json)
        {
            _apiConfiguration = apiConfiguration;
            _httpOtherApi = httpOtherApi;
            _json = json;

        }
        public override async void Extract(ObjExtract objExtract)
        {
            //Obtém a url
            var url = ConstructUrl(objExtract.jsonExtractDynamic);
            
            // Comunicaçao com a api externa
            string jsonRetornApiString = await _httpOtherApi.RestCommunication(_apiConfiguration.method,url);

            if( jsonRetornApiString == null)
            {
                // adicionar Erro Pendencia
            }

            // Criação do dicionario com os valores da extração
            var dicExtractApi = await ComposeDictionaryOfExtract(jsonRetornApiString,objExtract.dicExtract);


        }
        public override void Load()
        {

        }

        private string ConstructUrl(dynamic jsonExtract)
        {
            UriBuilder url = new UriBuilder(_apiConfiguration.endPoint);
            var query = HttpUtility.ParseQueryString(url.Query);
            foreach(var input in _apiConfiguration.input)
            {
                var value = GetValueParameter(input,jsonExtract);
                query[input.nameParameter] = value;
            }
            url.Query = query.ToString();

            return url.ToString();
        }

        private async Task<IDictionary<string,string>> ComposeDictionaryOfExtract(string jsonString
        , Dictionary<string,string> dicExtract)
        {           

            foreach(var output in _apiConfiguration.output)
            {
                var value = await _json.GetValuePath(jsonString,output.path);

                dicExtract.Add(output.localName,value);

            }
            return dicExtract;
        }

    }
}