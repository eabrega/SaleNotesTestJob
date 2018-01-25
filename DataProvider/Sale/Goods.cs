using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataProvider.Sale
{
    /// <summary>
    /// Еденица товара
    /// </summary>
    [DebuggerDisplay("{Name} - {Price} р.")]
    public class Goods
    {
        public Guid Identity { get; }
        public string Name { get; }
        public double Price { get; }
        public Goods(string name, double price) {
            Identity = Guid.NewGuid();
            Name = name;
            Price = price;
        }
        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
