using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ViewModels
{
    //ViewModels are to be used to pass on data from/to the View & Controllers
    //ViewModels usually contain a selection of the properties we have in our database, therefore
    // we will be hiding any properties containing sensitive data

    //Models are there to model/shape/engineer the database
    public class ItemViewModel
    {
       
        public int Id { get; set; }
      
        public string Name { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }

        public int Stock { get; set; }
        public string ImagePath { get; set; }

     
    }
}
