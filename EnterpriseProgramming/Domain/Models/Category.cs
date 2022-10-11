using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      
        [StringLength(50)]
        public string Title { get; set; }

        //public string IconPath { get; set; }

        //public int ParentCategoryId { get; set; }
    }
}
