using System.ComponentModel.DataAnnotations;

namespace recipeconfigurationservice.Model
{
    public class ExtractInParameter
    {
        [Key]
        public int extractInParameterId {get;set;}
        public string path{get;set;}
        [Required]
        public string type{get;set;}
        public string value{get;set;}
    }
}