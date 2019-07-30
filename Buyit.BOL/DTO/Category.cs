using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Buyit.BOL.DTO
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
