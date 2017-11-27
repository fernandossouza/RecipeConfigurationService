using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace recipeconfigurationservice.Model
{
    public class LoadConfiguration
    {
        [Key]
        public int loadConfigurationId{get;set;}
        [MaxLength(50)]
        public string name{get;set;}
        [MaxLength(100)]
        public string description{get;set;}
        [Required]
        public EType type{get;set;}
        public SqlLoad sqlLoad {get;set;}
        public ApiLoad apiLoad{get;set;}

    }
}