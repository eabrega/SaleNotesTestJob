using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataProvider.Users
{
    [DebuggerDisplay("{Name}")]
    public class Person
    {
        public Guid Identity { get; } = Guid.NewGuid();
        public virtual string Name { get; } = default(string);
        public byte Age { get; set; } = 0;


        public Person(string name, byte age = 0) {

            Name = name;
            Age = age;
        }
    }
}
