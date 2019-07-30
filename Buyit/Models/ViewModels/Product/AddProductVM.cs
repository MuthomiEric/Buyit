using Buyit.BOL.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Buyit.Models.ViewModels.Product
{
    public class AddProductVM
    {
        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }


        public IFormFile Photo { get; set; }
        [Display(Name = "Category")]
        public virtual int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
