using System.ComponentModel.DataAnnotations;
namespace recipeconfigurationservice.Model
{
    public class ParameterLoad
    {
        [Key]
        public int parameterLoadId{get;set;}
        [MaxLength(50)]
        public string localName{get;set;}
        [MaxLength(50)]
        public string jsonName{get;set;}
        [MaxLength(50)]
        public string queryParameter{get;set;}
        [Required]
        public ETypeData typeData{get;set;}
        [Required]
        public ETypeParameter type{get;set;}
        [MaxLength(50)]
        public string value{get;set;}

    }
}