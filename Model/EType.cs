using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace recipeconfigurationservice.Model
{
    public enum EType
    {
        Sql,
        Api
    }

    public class ConfigurationValidation :ValidationAttribute
    {
         protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            Extract model = validationContext.ObjectInstance as Extract;
 
            if (model == null)
                return new ValidationResult("Object extractConfiguration null");
 
            foreach(var configuration in model.extractConfiguration)
            {
                switch(configuration.type)
                {
                    case EType.Api:
                        if(configuration.apiConfiguration == null)
                            return new ValidationResult("Object apiConfiguration null");
 
                    break;
                    case EType.Sql:
                        if(configuration.sqlConfiguration == null)
                            return new ValidationResult("Object sqlConfiguration null");
                        break;
                    default:
                        return new ValidationResult("type configuration null");
                }
            
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            // Message that was supplied
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;
 
            // Use generic message: i.e. The field {0} is invalid
            //return this.FormatErrorMessage(validationContext.DisplayName);
 
            // Custom message
            return $"{validationContext.DisplayName} can't be in future";
        }

    }
}