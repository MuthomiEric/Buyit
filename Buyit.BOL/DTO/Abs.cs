using System;
using System.Collections.Generic;
using System.Text;

namespace Buyit.BOL.DTO
{
    public abstract class Abs
    {
        public Abs()
        {

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public abstract string Fullname(string firstName, string LastName);
    }

    public class Inheriting : Abs
    {
        public override string Fullname(string firstName, string lastName)
        {
            return firstName + ' ' + lastName;
        }
    }



}
