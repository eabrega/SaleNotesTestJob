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
    /// Товарный чек
    /// </summary>
    [DebuggerDisplay("{OrderNumber} {SaleCustomer.Name} - {Total} р.")]
    public class Check
    {
        static int _number = 0;
        public Guid Identity { get; } = Guid.NewGuid();
        public DateTime OrderDate { get; }
        public long OrderNumber { get; }
        public Customer SaleCustomer { get; }
        public List<CheckItem> CheckItems { get; }
        /// <summary>
        ///  Цена всех позиций в чеке
        /// </summary>
        public double Total {
            get {
                return CheckItems.Sum(x => x.Cost);
            }
        }

        public Check(List<CheckItem> checkItems, Customer customer, DateTime date)
        {
            _number++;
            OrderNumber = _number;
            SaleCustomer = customer;
            CheckItems = checkItems;
            OrderDate = date;
        }
    }
}
