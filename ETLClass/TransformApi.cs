using System;
using System.Collections;
using System.Web;
using System.Threading.Tasks;
using recipeconfigurationservice.Model;
using recipeconfigurationservice.ETLClass.Interface;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft;
using Newtonsoft.Json;

namespace recipeconfigurationservice.ETLClass
{
    public class TransformApi : TransformFactory
    {
        private ApiConfiguration _apiConfiguration;

        private readonly IHttpOtherApi _httpOtherApi;

        private readonly ApiLoad _apiLoad;

        private readonly IJson _json;

        public TransformApi (ApiConfiguration apiConfiguration,ApiLoad apiLoad, IHttpOtherApi httpOtherApi, IJson json)
        {
            _apiConfiguration = apiConfiguration;
            _httpOtherApi = httpOtherApi;
            _apiLoad = apiLoad;
            _json = json;

        }
        /// <summary>
        /// Process to Extract of information
        /// </summary>
        /// <param name="objExtract">object of Extraction</param>
        /// <returns></returns>
        public override async Task<bool> Extract(ObjExtract objExtract)
        {
            //Obtém a url
            var url = await ConstructUrlExtract(objExtract.jsonExtractDynamic);
            
            // Comunicaçao com a api externa
            string jsonRetornApiString = await _httpOtherApi.RestCommunication(_apiConfiguration.method,url);

           

            // Criação do dicionario com os valores da extração
            var dicExtractApi = await ComposeDictionaryOfExtract(jsonRetornApiString,objExtract.dicExtract);

            return true;
        }
        /// <summary>
        /// Process load in commnication with API
        /// </summary>
        /// <param name="dicExtract">dictionary of Extract</param>
        /// <returns>string with information of process load</returns>
        public override async Task<string> Load(Dictionary<string,string> dicExtract)
        {
            try{
                // converte para json
                var jsonExtract = JsonConvert.SerializeObject(dicExtract);
                //Obtém a url
                var url = await ConstructUrlLoad(jsonExtract);

                var jsonSend = await ConstructJsonDynamic(jsonExtract);
                
                // Comunicaçao com a api externa
                string returnApi = await _httpOtherApi.RestCommunication(_apiLoad.method,url,jsonSend);

                // Pendencia
                return returnApi;
            }
            catch (Exception ex)
            {
                return "Load Api: " + ex.Message;
            }
        }
        /// <summary>
        /// Contructor of url to process extraction
        /// </summary>
        /// <param name="jsonExtract">json with parameters to construct of url</param>
        /// <returns>complete url</returns>
        private async Task<string> ConstructUrlExtract(dynamic jsonExtract)
        {
            UriBuilder url = new UriBuilder(_apiConfiguration.endPoint);
            var query = HttpUtility.ParseQueryString(url.Query);
            foreach(var input in _apiConfiguration.input)
            {
                var value =  await GetValueParameter(input.type,input.path,input.value,jsonExtract);

                if(input.nameParameter == "/")
                    {
                        url.Path += input.nameParameter+value;
                    }
                else
                    {
                         query[input.nameParameter] = value;
                    }
               
            }
            url.Query = query.ToString();

            return url.ToString();
        }
        /// <summary>
        /// Contructor of url to process load
        /// </summary>
        /// <param name="json">json with parameters to construct of url</param>
        /// <returns>complete url</returns>
        private async Task<string> ConstructUrlLoad(dynamic json)
        {
            UriBuilder url = new UriBuilder(_apiLoad.endPoint);
            var query = HttpUtility.ParseQueryString(url.Query);
            foreach(var parameter in _apiLoad.parameterLoad.Where(x=> !string.IsNullOrEmpty(x.queryParameter)))
            {
                var value =  await GetValueParameter(parameter.type,parameter.localName,parameter.value,json);
                query[parameter.queryParameter] = value;
            }
            url.Query = query.ToString();

            return url.ToString();
        }
        /// <summary>
        /// Constructor json with the configuration of parameters loading
        /// </summary>
        /// <param name="json">json dynamic with parameters of load</param>
        /// <returns>json dynamic</returns>
        private async Task<string> ConstructJsonDynamic(dynamic json)
        {
            Dictionary<string,object> dicJson = new Dictionary<string, object>();
            foreach(var parameter in _apiLoad.parameterLoad.Where(x=> !string.IsNullOrEmpty(x.jsonName)))
            {
                var value =  await GetValueParameter(parameter.type,parameter.localName,parameter.value,json);
                dicJson.Add(parameter.jsonName,GetTypeData(value,parameter.typeData));
            }

            return JsonConvert.SerializeObject(dicJson);
        }
        /// <summary>
        /// return the data with correct format 
        /// </summary>
        /// <param name="value">value data in string</param>
        /// <param name="typeData">enum with type data</param>
        /// <returns>return data in the format</returns>
        private object GetTypeData(string value, ETypeData typeData)
        {
            if(string.IsNullOrEmpty(value))
                return null;

            switch(typeData)
            {
                case ETypeData.Bool:
                return Convert.ToBoolean(value);

                case ETypeData.String:
                return value;

                case ETypeData.Integer:
                return Convert.ToInt64(value);
            }

            return null;
        }
        /// <summary>
        /// Add property of json of Extraction to dictionary of Extract
        /// </summary>
        /// <param name="jsonString">json dynamic</param>
        /// <param name="dicExtract">dictionary Extract</param>
        /// <returns>dictonary</returns>
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