using System.ComponentModel.DataAnnotations;

namespace recipeconfigurationservice.Model
{
    public class SqlConfiguration
    {
        [Key]
        public int sqlConfigurationId{get;set;}
        [MaxLength(200)]
        public string commandSQL{get;set;}
        [MaxLength(100)]
        public string stringConection{get;set;}
        public EtypeDb typeDb{get;set;}
        public ICollection<ExtractInParameter> input{get;set;}
        public ICollection<ExtractOutParameter> output{get;set;}
    }
}