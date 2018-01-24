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
        public ePayment PaymentType { get; }
        /// <summary>
        ///  Цена всех позиций в чеке
        /// </summary>
        public double Total {
            get {
                return CheckItems.Sum(x => x.Cost);
            }
        }
        public Check(List<CheckItem> checkItems, Customer customer, DateTime date, ePayment pay)
        {
            _number++;
            OrderNumber = _number;
            SaleCustomer = customer;
            CheckItems = checkItems;
            OrderDate = date;
            PaymentType = pay;
        }
    }

    /// <summary>
    /// Конвертер для вывода в DataGrid
    /// </summary>
    public class CheckToDataGrid
    {
        Check _check { get; }
        public long Number { get { return _check.OrderNumber; } }
        public DateTime Data { get { return _check.OrderDate; } }
        public string Name { get { return $"{_check.SaleCustomer.Name}"; } }
        public string Email { get { return _check.SaleCustomer.Email; } }
        public string Cost { get { return $"{_check.Total} p."; } }
        public ePayment Payment { get { return _check.PaymentType; } }
        public CheckToDataGrid(Check check) {
            _check = check;
        }
    }

    public enum ePayment {

        NoPayment,
        Cash,
        Visa,
        MasterCard
    }
}
