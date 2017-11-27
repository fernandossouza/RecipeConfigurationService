using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace recipeconfigurationservice.Model
{
    public class ApiLoad
    {
        public int apiLoadId{get;set;}
         [MaxLength(100)]
        public string endPoint{get;set;}
         [MaxLength(10)]
        public string method{get;set;}
        public ICollection<ParameterLoad> parameterLoad{get;set;}

    }
}