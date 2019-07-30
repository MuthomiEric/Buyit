
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buyit.Models.ViewModels.Home
{
    public class HomeVM
    {
        public IEnumerable<Buyit.BOL.DTO.Product> PreferedProducts { get; set; }
    }
}
