using Buyit.BOL.DTO.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Buyit.BOL.DTO.Order
{
    public class Order
    {

        public int OrderId { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime PlacedAt { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        //Not Functonal
        public Guid UserId { get; set; }
        public virtual User User { get; set; }


    }
}
