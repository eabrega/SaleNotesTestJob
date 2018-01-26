using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Users
{
    public sealed class Customer : Person
    {
        public string Email { get; set; }

        public Customer(string name, string email) : base(name)
        {
            this.Email = email;
        }

        public Customer(string name, string email, byte age) : base(name, age)
        {
            this.Email = email;
        }

        public override string ToString()
        {
            return base.Name.ToString();
        }
    }
}
