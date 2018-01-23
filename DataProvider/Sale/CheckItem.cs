using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Users;
using System.Diagnostics;

namespace DataProvider.Sale
{
    /// <summary>
    /// Строка в чеке
    /// </summary>
    [DebuggerDisplay("{SaleGoods.Name} х {Quantity} = {Cost} р.")]
    public class CheckItem
    {
        public Guid Identity { get; } = Guid.NewGuid();
        /// <summary>
        /// Товар
        /// </summary>
        public Goods SaleGoods { get; }
        /// <summary>
        /// Количество
        /// </summary>
        public int Quantity { get; }
        /// <summary>
        /// Стоимость 
        /// </summary>
        public double Cost {
            get {
                return SaleGoods.Price * Quantity;
            }
        }

        public CheckItem(Goods goods, int quantity ) {
            this.SaleGoods = goods;
            this.Quantity = quantity;
        }
    }
}
