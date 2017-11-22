using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public EType type{get;set;}
        public SqlConfiguration sqlConfiguration{get;set;} 
        public ApiConfiguration apiConfiguration{get;set;}
        public ICollection<ExtractInParameter> parameterIn{get;set;}
        public ICollection<ExtractOutParameter> parameterOut{get;set;}
        
    }
}