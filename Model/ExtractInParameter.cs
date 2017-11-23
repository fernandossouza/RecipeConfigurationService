using System.ComponentModel.DataAnnotations;

namespace recipeconfigurationservice.Model
{
    public class ExtractInParameter
    {
        [Key]
        public int extractInParameterId {get;set;}
        [MaxLength(50)]
        public string path{get;set;}
        [MaxLength(50)]
        public string nameParameter{get;set;}
        [Required]
        public string type{get;set;}
        [MaxLength(50)]
        public string value{get;set;}
    }
}