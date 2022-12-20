using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Validators
{
    public class CategoryValidatorAttribute: ValidationAttribute
    {
     
 
        public string GetErrorMessage() =>
            $"Select a valid category";

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            try
            {

                CategoriesService cs = (CategoriesService)validationContext.GetService(typeof(CategoriesService));

                int max = cs.GetCategories().Max(x => x.Id);
                int min = cs.GetCategories().Min(x => x.Id);

                if (Convert.ToInt32(value) >= min && Convert.ToInt32(value) <= max)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(GetErrorMessage());

                }
            }
            catch
            {
                return new ValidationResult("Category cannot be verified at the moment");
            }
        }
    }

     
}
