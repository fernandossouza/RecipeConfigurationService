using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace recipeconfigurationservice.Model
{
    public class ExtractInParameter
    {
        [Key]
        public int extractInParameterId {get;set;}
        [MaxLength(50)]
        public string path{get;set;}
        [MaxLength(50)]
        public string nameParameter{get;set;}
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public ETypeParameter type{get;set;}
        [MaxLength(50)]
        public string value{get;set;}
    }
}