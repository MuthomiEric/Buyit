using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyIt.Admin.Models.ViewModels.Product
{
    public class EditProductVM : AddProductVM
    {
        public Guid Id { get; set; }
        public string ExistingImage { get; set; }
    }
}
