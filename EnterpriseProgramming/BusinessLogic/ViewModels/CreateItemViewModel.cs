using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BusinessLogic.Validators;
namespace BusinessLogic.ViewModels
{
    public class CreateItemViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please input a valid name")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        //[RegularExpression("^[a-zA-Z]{1}[a-zA-Z0-9]*$", ErrorMessage = "Name must not start with a number")] //[a-zA-Z_][a-zA-Z0-9_]*
        public string Name { get; set; }
       
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please input a valid price")]
        [Range(1, 10000, ErrorMessage = "Price has to be between 1 and 10000")]
        public double Price { get; set; }

        [CategoryValidator]
        public int CategoryId { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please input a valid stock amount")]
        [Range(1, 10000, ErrorMessage = "Stock has to be between 0 and 10000")]
        public int Stock { get; set; }
        public string ImagePath { get; set; }

        public IQueryable<CategoryViewModel> Categories { get; set; }

    }
}
