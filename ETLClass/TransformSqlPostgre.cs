using recipeconfigurationservice.Model;
using recipeconfigurationservice.ETLClass.Interface;
using Npgsql;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;


namespace recipeconfigurationservice.ETLClass
{
    public class TransformSqlPostgre : TransformSqlFactory
    {

        private SqlConfiguration _sqlConfiguration;
        private SqlLoad _sqlLoad;
        private readonly IHttpOtherApi _httpOtherApi;
        private readonly IJson _json;

        public TransformSqlPostgre (SqlConfiguration sqlConfiguration,SqlLoad sqlLoad, IJson json)
        {
            _sqlConfiguration = sqlConfiguration;
            _sqlLoad = sqlLoad;
            _json = json;

        }

        public override async Task<bool> Extract(ObjExtract objExtract)
        {
            var commandSQL = await ConstructCommand(objExtract.jsonExtractDynamic,_sqlConfiguration.commandSQL,"extract");

            IEnumerable<dynamic> dbResult = await ExecuteCommand(commandSQL,_sqlConfiguration.stringConnection);

            if(dbResult == null)
            {
                //pendencia
            }

            await AddInDictionary(objExtract.dicExtract,dbResult);
            return true;
        }

         public override async Task<bool> Load(Dictionary<string,string> dicExtract)
        {
            // Converte dicionario em json
            dynamic jsonConvertDic = JsonConvert.SerializeObject(dicExtract);
            
            var commandSQL = await ConstructCommand(jsonConvertDic,_sqlLoad.commandSQL,"load");
             IEnumerable<dynamic> dbResult = await ExecuteCommand(commandSQL,_sqlLoad.stringConnection);

            if(dbResult == null)
            {
                //pendencia
            }
            return true;

        }

        private async Task<string> ConstructCommand(dynamic jsonDynamic,string command,string typeProcess)
        {
            string commandSQL = command;
            
            commandSQL = commandSQL + " (";
            
            if(typeProcess.ToLower() == "extract")
            {
                commandSQL = await GetParameterExtract(commandSQL,jsonDynamic);
            }
            else if (typeProcess.ToLower() == "load")
            {
                commandSQL = await GetParameterLoad(commandSQL,jsonDynamic);
            }
            commandSQL = commandSQL + " )";

            return commandSQL;

        }

         private async Task<string> GetParameterLoad(string command, dynamic jsonDynamic)
        {
            string commandSQL = command;
            bool first = true;
            foreach(var parameter in _sqlLoad.parameterLoad)
            {
                if(!first)
                    commandSQL = commandSQL + ",";
                    
                string value = await GetValueParameter(parameter.type,parameter.localName,parameter.value,jsonDynamic);
                commandSQL = commandSQL + "'"+ value +"'";

                

                first = false;
            }
            return commandSQL;
        }

        private async Task<string> GetParameterExtract(string command, dynamic jsonExtract)
        {
            string commandSQL = command;
            bool first = true;
            foreach(var input in _sqlConfiguration.input)
            {
                 if(!first)
                    commandSQL = commandSQL + ",";

                string value = await GetValueParameter(input.type,input.path,input.value,jsonExtract);
                commandSQL = commandSQL + "'"+ value +"'";

               

                first = false;
            }
            return commandSQL;
        }


        private async Task<IEnumerable<dynamic>> ExecuteCommand(string commandSQL,string stringConnection)
        {
           try
           {
            IEnumerable<dynamic> dbResult;
            using(IDbConnection dbConnection = new NpgsqlConnection(stringConnection))
            {
                dbConnection.Open();
                dbResult = await dbConnection.QueryAsync<dynamic>(commandSQL);
            }

            return dbResult;
           }
            catch (Exception ex)
            {
               return null;
            }
        }

        private async Task<bool> AddInDictionary(Dictionary<string,string> dicExtract,IEnumerable<dynamic> dbResult)
        {

            foreach(var output in _sqlConfiguration.output)
            {
                string value = await GetValueDbResultPath(dbResult,output.path);

                dicExtract.Add(output.localName,value);
            }

            return true;
        }

        private async Task<string> GetValueDbResultPath(IEnumerable<object> dbResult,string path)
        {
            string value = string.Empty;

            var returnDb = ((IDictionary<string, object>)dbResult.FirstOrDefault());
            value = (from d in returnDb where d.Key.ToLower() == path.ToLower() select d.Value)
                    .FirstOrDefault()
                    .ToString();           

            return value;
        }
    }
}