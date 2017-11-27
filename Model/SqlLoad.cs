using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace recipeconfigurationservice.Model
{
    public class SqlLoad
    {
        [Key]
        public int sqlLoadId{get;set;}
        [MaxLength(100)]
        public string commandSQL{get;set;}
        [MaxLength(100)]
        public string stringConnection{get;set;}
        [Required]
        public EtypeDb typeDB{get;set;}
        public ICollection<ParameterLoad> parameterLoad{get;set;}
    }
}