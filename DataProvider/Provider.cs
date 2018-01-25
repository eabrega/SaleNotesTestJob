using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Users;
using DataProvider.Sale;
using System.Collections;

namespace DataProvider
{
    public class Provider : IEnumerable
    {
        List<Customer> Customers { get; set; } = new List<Customer>();
        List<Check> Checks { get; set; } = new List<Check>();
        List<Goods> AllGoods { get; set; } = new List<Goods>();
        //public List<CheckVisualiser> CheckDataView { get { return GetChecksRows(); } }
        public Provider()
        {
        }
        /// <summary>
        /// Загрузить список покупателей
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            return Customers;
        }
        public List<CheckVisualiser> GetChecksVisual() {
            return Checks.Select(x => x.LikeTableRow).ToList();
        }
        public List<Check> GetChecks() {
            return Checks;
        }
        public Customer GetCustomerByName(string name)
        {
            return Customers.Where(x => x.Name == name).FirstOrDefault();
        }
        /// <summary>
        /// Загрузить список продаж
        /// </summary>
        /// <returns></returns>
        //public List<CheckVisualiser> GetChecksRows()
        //{

        //    List<CheckVisualiser> rows = new List<CheckVisualiser>();

        //    foreach (var item in Checks)
        //    {
        //        rows.Add(new CheckVisualiser(item));
        //    }


        //    return rows;
        //}
        public Check GetCheckByIndex(int index)
        {
            return Checks[index];
        }
        public void MakeCheck(List<CheckItem> orderitem, Customer customer, DateTime date, ePayment pay)
        {
            Checks.Add(new Check(orderitem, customer, date, pay));
        }
        public CheckItem MakeOrdeItem(Goods goods, int guantity)
        {
            return new CheckItem(goods, guantity);
        }
        public void AddGoods(string name, double price)
        {
            AllGoods.Add(new Goods(name, price));

        }
        public void AddCustomer(string name, string email)
        {

            Customers.Add(new Customer(name, email));

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var ttt = Checks.Select(x => x.LikeTableRow);

            return ttt.GetEnumerator();
        }
    }
}
