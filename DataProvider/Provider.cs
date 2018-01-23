using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Users;
using DataProvider.Sale;
namespace DataProvider
{
    public class Provider
    {
        List<Customer> Customers { get; set; } = new List<Customer>();
        List<Check> Checks { get; set; } = new List<Check>();
        List<Goods> AllGoods { get; set; } = new List<Goods>();
        public Provider() {
        }
        /// <summary>
        /// Загрузить список покупателей
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers() {
            return Customers;
        }
        public Customer GetCustomerByName(string name)
        {
            return Customers.Where(x=>x.Name == name).FirstOrDefault();
        }
        /// <summary>
        /// Загрузить список продаж
        /// </summary>
        /// <returns></returns>
        public List<Check> GetChecks() {
            return Checks;
        }
        public void MakeCheck(List<CheckItem> orderitem, Customer customer, DateTime date) {


            Checks.Add(new Check(orderitem, customer, date));

        }
        public CheckItem newOrdeItem(Goods goods, int guantity)
        {
            return new CheckItem(goods, guantity);
        }
        public void AddGoods(string name, double price) {
            AllGoods.Add(new Goods(name, price));

        }
        public void AddCustomer(string name, string email) {

            Customers.Add(new Customer(name, email));

        }
    }
}
