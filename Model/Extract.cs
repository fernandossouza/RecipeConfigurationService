using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace recipeconfigurationservice.Model
{
    public class Extract
    {
        [Key]
        public int extractId{get;set;}
        [Required]
        [MaxLength(50)]
        public string name{get;set;}
        [Required]
        [MaxLength(10)]
        public string enabled{get;set;}
        [MaxLength(200)]
        public string description{get;set;}
        [ExtractConfigurationValidation]
        public ICollection<ExtractConfiguration> extractConfiguration{get;set;}
    }
}