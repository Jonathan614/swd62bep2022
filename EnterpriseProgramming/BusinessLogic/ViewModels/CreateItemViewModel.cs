using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ViewModels
{
    public class CreateItemViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }

        public int Stock { get; set; }
        public string ImagePath { get; set; }

    }
}
