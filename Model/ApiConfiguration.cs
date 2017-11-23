using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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
        public IList<ExtractInParameter> input{get;set;}
        public IList<ExtractOutParameter> output{get;set;}
    }
}