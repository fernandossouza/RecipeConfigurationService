using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace recipeconfigurationservice.Model
{
    public class Load
    {
        public int loadId{get;set;}
        [MaxLength(50)]
        public string name{get;set;}
        [MaxLength(100)]
        public string description{get;set;}
        [Required]
        public int extractId{get;set;}
        [LoadConfigurationValidation]
        public ICollection<LoadConfiguration> loadConfiguration {get;set;}
        
    }
}