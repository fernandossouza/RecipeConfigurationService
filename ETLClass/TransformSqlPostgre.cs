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

        /// <summary>
        /// Process extract in database postgreSQL
        /// </summary>
        /// <param name="objExtract">object of extract</param>
        /// <returns></returns>
        public override async Task<bool> Extract(ObjExtract objExtract)
        {
            var commandSQL = await ConstructCommand(objExtract.jsonExtractDynamic,_sqlConfiguration.commandSQL,"extract");

            IEnumerable<dynamic> dbResult = await ExecuteCommand(commandSQL,_sqlConfiguration.stringConnection);


            await AddInDictionary(objExtract.dicExtract,dbResult);
            return true;
        }
        /// <summary>
        /// Process of load in database postgreSQL
        /// </summary>
        /// <param name="dicExtract">dictionary with extract</param>
        /// <returns>return string with information of process</returns>
         public override async Task<string> Load(Dictionary<string,string> dicExtract)
        {
            // Converte dicionario em json
            try{
            dynamic jsonConvertDic = JsonConvert.SerializeObject(dicExtract);
            
            var commandSQL = await ConstructCommand(jsonConvertDic,_sqlLoad.commandSQL,"load");
             IEnumerable<dynamic> dbResult = await ExecuteCommand(commandSQL,_sqlLoad.stringConnection);
            
            return "ok";
            }
            catch (Exception ex)
            {
                return "Load Postgre: " + ex.Message;
            }
        }

        /// <summary>
        /// Contructor of command SQL for extract or load
        /// </summary>
        /// <param name="jsonDynamic">json with parameters of process</param>
        /// <param name="command">command information in register</param>
        /// <param name="typeProcess">type of process extract or load</param>
        /// <returns>return complete command</returns>
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

        
        /// <summary>
        /// Get parameters to complete command of process load
        /// </summary>
        /// <param name="command">command SQL</param>
        /// <param name="jsonDynamic">json with parameters load</param>
        /// <returns>return command complete</returns>
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
        /// <summary>
        ///  get input parameters for complete command SQL
        /// </summary>
        /// <param name="command">command SQL</param>
        /// <param name="jsonExtract">input in json to complete command</param>
        /// <returns></returns>
        private async Task<string> GetParameterExtract(string command, dynamic jsonExtract)
        {
            string commandSQL = command;
            bool first = true;
            foreach(var input in _sqlConfiguration.input.OrderBy(x=> x.order))
            {
                 if(!first)
                    commandSQL = commandSQL + ",";

                string value = await GetValueParameter(input.type,input.path,input.value,jsonExtract);
                commandSQL = commandSQL + "'"+ value +"'";

               

                first = false;
            }
            return commandSQL;
        }

        /// <summary>
        /// Execute command in database
        /// </summary>
        /// <param name="commandSQL">command SQL</param>
        /// <param name="stringConnection">string of connection with DB</param>
        /// <returns>ienumerable object with result of database</returns>
        private async Task<IEnumerable<dynamic>> ExecuteCommand(string commandSQL,string stringConnection)
        {
          
            IEnumerable<dynamic> dbResult;
            using(IDbConnection dbConnection = new NpgsqlConnection(stringConnection))
            {
                dbConnection.Open();
                dbResult = await dbConnection.QueryAsync<dynamic>(commandSQL);
            }

            return dbResult;
           
            
        }
        /// <summary>
        /// add output configuration in a dictionary
        /// </summary>
        /// <param name="dicExtract">dictionary of Extract for add</param>
        /// <param name="dbResult"> ienumerable with information of DB</param>
        /// <returns></returns>
        private async Task<bool> AddInDictionary(Dictionary<string,string> dicExtract,IEnumerable<dynamic> dbResult)
        {

            foreach(var output in _sqlConfiguration.output)
            {
                string value = await GetValueDbResultPath(dbResult,output.path);

                dicExtract.Add(output.localName,value);
            }

            return true;
        }
        /// <summary>
        /// Return the value of a path in an enumerable od object 
        /// </summary>
        /// <param name="dbResult">ienumerable object</param>
        /// <param name="path">path for search</param>
        /// <returns>return value in string</returns>
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