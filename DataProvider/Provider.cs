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
using System.Windows.Threading;

namespace DataProvider
{
    public static class Provider
    {
        static int _year = DateTime.Now.Year;
        static List<Customer> Customers { get; set; } = new List<Customer>();
        static List<Check> Checks { get; set; } = new List<Check>();
        static List<Goods> AllGoods { get; set; } = new List<Goods>();
        static List<ReportByMonth> ReportsByMonths { get; set; } = new List<ReportByMonth>();
        static List<ReportByCustomer> ReportsByCustomers { get; set; } = new List<ReportByCustomer>();
        static public int ReportYear {
            get {
                return _year;
            }
            set {
                if (value != _year)
                {
                    _year = value;
                    RebuildReport?.Invoke();
                }
            }
        } 

        static public event Action<Check> ChecksListUpdated;
        static public event Action RebuildReport;
        static Provider()
        {
            ReportByMonth.Checks = Checks;
            ReportByCustomer.Checks = Checks;
            ReportReminder.Checks = Checks;
        }
        /// <summary>
        /// Возвращает список покупателей
        /// </summary>
        /// <returns></returns>
        static public List<Customer> GetCustomers()
        {
            return Customers;
        }
        /// <summary>
        /// Возвращает табличное представление списка чеков
        /// </summary>
        /// <returns></returns>
        static public List<CheckVisualiser> GetChecksVisual() {
            return Checks.Select(x => x.LikeTableRow).ToList();
        }
        /// <summary>
        /// Возвращает список чеков
        /// </summary>
        /// <returns></returns>
        static public List<Check> GetChecks() {
            return Checks;
        }
        /// <summary>
        /// Взврвщает пользователя, по имени
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <returns></returns>
        static public Customer GetCustomerByName(string name)
        {
            return Customers.Where(x => x.Name == name).FirstOrDefault();
        }
        /// <summary>
        /// Возвращает чек по номеру
        /// </summary>
        /// <param name="index">Номер чека</param>
        /// <returns></returns>
        static public Check GetCheckByIndex(int index)
        {
            if (index < 0) return null;
            return Checks[index];
        }
        /// <summary>
        /// Возвращает список товаров 
        /// </summary>
        /// <returns></returns>
        static public List<Goods> GetGoods() {
            return AllGoods;
        }
        /// <summary>
        /// Возвращает вощь по ее ID
        /// </summary>
        /// <param name="id">ID Вещи</param>
        /// <returns></returns>
        static public Goods GetGoodsById(Guid? id) {

            if (id == null) return null;
            return AllGoods.Find(x=>x.Identity == id);
        }
        /// <summary>
        /// Создает Чек
        /// </summary>
        /// <param name="customer">Покупатель</param>
        /// <param name="date">Дата</param>
        static public Check MakeCheck(Customer customer, DateTime date)
        {
            Checks.Add(new Check(customer, date));
            ChecksListUpdated?.Invoke(Checks.Last());

            return Checks.Last();
        }
        /// <summary>
        /// Удаляет чек, если он не оплачен.
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        static public bool RemoveCheck(Check check) {
            if (check.PaymentType == ePayment.NoPayment)
            {
                Checks.Remove(check);
                ChecksListUpdated?.Invoke(null);
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Создает строку чека
        /// </summary>
        /// <param name="goods">Товар</param>
        /// <param name="guantity">количество</param>
        /// <returns></returns>
        static public void AddCheckOrdeItem(Check parent, Goods goods, int guantity)
        {
            if (goods == null || parent == null) throw new Exception("Чек не был создан, неверный параметр!");

            parent.Items.Add(new CheckItem(goods, guantity));
            ChecksListUpdated?.Invoke(parent);
        }
        /// <summary>
        /// Оплата чека
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="pay"></param>
        static public void CloseCheck(Check parent, ePayment pay) {
            parent.PaymentType = pay;
        }
        /// <summary>
        /// Добавляет товар
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="price">Цена</param>
        static public void AddGoods(string name, double price)
        {
            AllGoods.Add(new Goods(name, price));

        }
        /// <summary>
        /// Добавляет пользователя
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <param name="email">Емайл</param>
        static public void AddCustomer(string name, string email)
        {
            Customers.Add(new Customer(name, email));
        }
        /// <summary>
        /// Добавляет список пользователей
        /// </summary>
        /// <param name="customers"></param>
        static public void AddCustomers(List<Customer> customers)
        {
            Customers.AddRange(customers);
        }
        /// <summary>
        /// Возвращает отчет по месяцам
        /// </summary>
        /// <returns></returns>
        static public List<ReportByMonth> GetReportsByMonths() {

            try
            {
                ReportsByMonths.Clear();

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
        static public List<ReportByCustomer> GetReportsByCustomers() {

            ReportsByCustomers.Clear();
            
            foreach (Customer item in Customers)
            {
                ReportsByCustomers.Add(new ReportByCustomer(item));
            }

            return ReportsByCustomers;
        }
        /// <summary>
        /// Возвращает напоминание за два месяца от текущей даты о необходимости купит крепеж, тем у кого есть инструмент.
        /// </summary>
        /// <returns></returns>
        static public List<ReportRow> GetReportReminder() {

            var reports = new ReportReminder();

            return reports.Reporting();
        }
    }
}
