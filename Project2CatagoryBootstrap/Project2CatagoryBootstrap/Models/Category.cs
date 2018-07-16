using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project2CatagoryBootstrap.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Category Name")]
        public String Name { get; set; }

        [Required]
        [Display(Name ="Display Order")]
        public int DisplayOrder { get; set; }

    }
}
