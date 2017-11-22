using System.ComponentModel.DataAnnotations;

namespace recipeconfigurationservice.Model
{
    public class ExtractOutParameter
    {
        [Key]
        public int extractOutParameterId {get;set;}
        [MaxLength(50)]
        public string path{get;set;}
        [Required]
        [MaxLength(50)]
        public string localName{get;set;}
        [MaxLength(50)]
        public string value{get;set;}

    }
}