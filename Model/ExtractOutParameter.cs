using System.ComponentModel.DataAnnotations;

namespace recipeconfigurationservice.Model
{
    public class ExtractOutParameter
    {
        [Key]
        public long extractOutParameterId {get;set;}
        public string path{get;set;}
        [Required]
        public string name{get;set;}
        public string value{get;set;}

    }
}