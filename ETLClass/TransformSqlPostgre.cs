using recipeconfigurationservice.Model;
using recipeconfigurationservice.ETLClass.Interface;
using Npgsql;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;


namespace recipeconfigurationservice.ETLClass
{
    public class TransformSqlPostgre : TransformSqlFactory
    {

        private SqlConfiguration _sqlConfiguration;
        private readonly IHttpOtherApi _httpOtherApi;
        private readonly IJson _json;

        public TransformSqlPostgre (SqlConfiguration sqlConfiguration, IJson json)
        {
            _sqlConfiguration = sqlConfiguration;
            _json = json;

        }

        public override async Task<bool> Extract(ObjExtract objExtract)
        {
            var commandSQL = await ConstructCommand(objExtract.jsonExtractDynamic);

            IEnumerable<dynamic> dbResult = await ExecuteCommand(commandSQL);

            if(dbResult == null)
            {
                //pendencia
            }

            await AddInDictionary(objExtract.dicExtract,dbResult);
            return true;
        }

         public override void Load()
        {
            // Pendencia

        }

        private async Task<string> ConstructCommand(dynamic jsonExtract)
        {
            string commandSQL = string.Empty;
            bool first = true;

            commandSQL = _sqlConfiguration.commandSQL;
            commandSQL = commandSQL + " (";
            
            foreach(var input in _sqlConfiguration.input)
            {
                string value = await GetValueParameter(input,jsonExtract);
                commandSQL = commandSQL + "'"+ value +"'";

                if(!first)
                    commandSQL = commandSQL + ",";

                first = false;
            }
            
            commandSQL = commandSQL + " )";

            return commandSQL;

        }


        private async Task<IEnumerable<dynamic>> ExecuteCommand(string commandSQL)
        {
           try
           {
            IEnumerable<dynamic> dbResult;
            using(IDbConnection dbConnection = new NpgsqlConnection(_sqlConfiguration.stringConnection))
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