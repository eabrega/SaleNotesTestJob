using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Users;
using DataProvider.Sale;
using DataProvider.Reports;
using System.Collections;
using System.Windows.Forms;

namespace DataProvider
{
    public class Provider
    {
        List<Customer> Customers { get; set; } = new List<Customer>();
        List<Check> Checks { get; set; } = new List<Check>();
        List<Goods> AllGoods { get; set; } = new List<Goods>();  
        List<ReportByMonth> ReportsByMonths { get; set; } = new List<ReportByMonth>();
        List<ReportByCustomer> ReportsByCustomers { get; set; } = new List<ReportByCustomer>();
        public Provider()
        {
            ReportByMonth.Checks = Checks;
            ReportByCustomer.Checks = Checks;
        }
        /// <summary>
        /// Загрузить список покупателей
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            return Customers;
        }
        /// <summary>
        /// Возвращает табличное представление списка чеков
        /// </summary>
        /// <returns></returns>
        public List<CheckVisualiser> GetChecksVisual() {
            return Checks.Select(x => x.LikeTableRow).ToList();
        }
        /// <summary>
        /// Возвращает список чеков
        /// </summary>
        /// <returns></returns>
        public List<Check> GetChecks() {
            return Checks;
        }
        /// <summary>
        /// Взврвщает пользователя, по имени
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <returns></returns>
        public Customer GetCustomerByName(string name)
        {
            return Customers.Where(x => x.Name == name).FirstOrDefault();
        }
        /// <summary>
        /// Возвращает чек по номеру
        /// </summary>
        /// <param name="index">Номер чека</param>
        /// <returns></returns>
        public Check GetCheckByIndex(int index)
        {
            return Checks[index];
        }
        /// <summary>
        /// Создает Чек
        /// </summary>
        /// <param name="orderitem">Строки чека</param>
        /// <param name="customer">Покупатель</param>
        /// <param name="date">Дата</param>
        /// <param name="pay">Тип оплаты</param>
        public void MakeCheck(List<CheckItem> orderitem, Customer customer, DateTime date, ePayment pay)
        {
            Checks.Add(new Check(orderitem, customer, date, pay));
        }
        /// <summary>
        /// Создает строку чека
        /// </summary>
        /// <param name="goods">Товар</param>
        /// <param name="guantity">количество</param>
        /// <returns></returns>
        public CheckItem MakeOrdeItem(Goods goods, int guantity)
        {
            return new CheckItem(goods, guantity);
        }
        /// <summary>
        /// Добавляет товар
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="price">Цена</param>
        public void AddGoods(string name, double price)
        {
            AllGoods.Add(new Goods(name, price));

        }
        /// <summary>
        /// Добавляет пользователя
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <param name="email">Емайл</param>
        public void AddCustomer(string name, string email)
        {

            Customers.Add(new Customer(name, email));

        }
        /// <summary>
        /// Добавляет список пользователей
        /// </summary>
        /// <param name="customers"></param>
        public void AddCustomers(List<Customer> customers)
        {

            this.Customers = customers;

        }
        /// <summary>
        /// Возвращает отчет по месяцам
        /// </summary>
        /// <returns></returns>
        public List<ReportByMonth> GetReportsByMonths(int year) {

            try
            {
                ReportByMonth.Year = year;

                for (int i = 1; i < 13; i++)
                {
                    ReportsByMonths.Add(new ReportByMonth((byte)i));
                }
            }

            catch (Exception err) {

                Console.WriteLine(err.Message);
                MessageBox.Show(err.Message);
            };

            return ReportsByMonths;
        }
        /// <summary>
        /// Возвращает отчет по пользователям
        /// </summary>
        /// <returns></returns>
        public List<ReportByCustomer> GetReportsByCustomers(int year) {
            foreach (Customer item in Customers)
            {
                ReportsByCustomers.Add(new ReportByCustomer(item));
            }

            return ReportsByCustomers;
        }


    }
}
