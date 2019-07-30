using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Buyit.BOL.DTO
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        [Display(Name = "Is Preferend")]
        public bool IsPrefferedDrink { get; set; }
        [Required]
        [Display(Name = "Items In Stock")]
        public int InStock { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
