using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace recipeconfigurationservice.Model
{
    public class SqlConfiguration
    {
        [Key]
        public int sqlConfigurationId{get;set;}
        [MaxLength(200)]
        public string commandSQL{get;set;}
        [MaxLength(100)]
        public string stringConnection{get;set;}
        [JsonConverter(typeof(StringEnumConverter))]
        public EtypeDb typeDb{get;set;}
        public ICollection<ExtractInParameter> input{get;set;}
        public ICollection<ExtractOutParameter> output{get;set;}
    }
}