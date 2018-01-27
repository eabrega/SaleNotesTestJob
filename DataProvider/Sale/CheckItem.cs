using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Users;
using System.Diagnostics;
using System.Collections;

namespace DataProvider.Sale
{
    /// <summary>
    /// Строка в чеке
    /// </summary>
    [DebuggerDisplay("{SaleGoods.Name} {Price} х {Quantity} = {Cost}")]
    public sealed class CheckItem
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
        /// Цена за еденицу, вычисляемое поле
        /// </summary>
        public string Price { get { return SaleGoods.Price.ToString("C"); } }
        /// <summary>
        /// Стоимость, вычисляемое поле
        /// </summary>
        public string Cost {
            get {
                return (SaleGoods.Price * Quantity).ToString("C");
            }
        }
        public CheckItem() {

        }
        public CheckItem(Goods goods, int quantity ) {
            this.SaleGoods = goods;
            this.Quantity = quantity;
        }
        public override string ToString()
        {
            return $"{SaleGoods.Name} {Price} x {Quantity} - {Cost} "; 
        }
    }
}
