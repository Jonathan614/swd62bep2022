using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    //Model i.e that will shape the database
    //note: class name in singular
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }

        public int Stock { get; set; }
        public string ImagePath { get; set; }
    }
}
