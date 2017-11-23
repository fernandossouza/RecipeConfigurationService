using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace recipeconfigurationservice.Model
{
    
    public class ExtractConfiguration
    {
        [Key]
        public int extractConfigurationId {get;set;}
        [Required]
        [MaxLength(50)]
        public string name{get;set;}
        [MaxLength(200)]
        public string description{get;set;}
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public EType type{get;set;}        
        public virtual SqlConfiguration sqlConfiguration{get;set;}         
        public ApiConfiguration apiConfiguration{get;set;}
        
    }
}