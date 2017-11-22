using System.ComponentModel.DataAnnotations;
namespace recipeconfigurationservice.Model
{
    public class ApiConfiguration
    {
        [Key]
        public int apiConfigurationId{get;set;}
        [MaxLength(100)]
        public string endPoint{get;set;}
        [MaxLength(10)]
        public string method{get;set;}
        public ICollection<ExtractInParameter> input{get;set;}
        public ICollection<ExtractOutParameter> output{get;set;}
    }
}