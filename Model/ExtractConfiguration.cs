using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace recipeconfigurationservice.Model
{
    public class ExtractConfiguration
    {
        [Key]
        public long extractConfigurationId {get;set;}
        [Required]
        [MaxLength(50)]
        public string name{get;set;}
        [MaxLength(200)]
        public string description{get;set;}
        [Required]
        public string type{get;set;}
        public string commandSQL{get;set;}
        public string stringConection{get;set;}
        public string endPoint{get;set;}
        public string method{get;set;}
        public ICollection<ExtractInParameter> parameterIn{get;set;}
        public ICollection<ExtractOutParameter> parameterOut{get;set;}
        
    }
}