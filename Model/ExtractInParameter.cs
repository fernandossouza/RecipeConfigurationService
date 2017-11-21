using System.ComponentModel.DataAnnotations;

namespace recipeconfigurationservice.Model
{
    public class ExtractInParameter
    {
        [Key]
        public long extractInParameterId {get;set;}
        public string path{get;set;}
        [Required]
        public string type{get;set;}
        public string value{get;set;}
    }
}