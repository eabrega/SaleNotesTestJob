using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Sale;
using DataProvider.Users;

namespace DataProvider.Reports
{
    public class ReportByCustomer
    {
        /// <summary>
        /// Чеки по которым будет совершенна выборка
        /// </summary>
        static public List<Check> Checks;
        List<Check> _curentChecks { get; }
        public Customer CurentCustomer { get; }
        public string CostByYear {

            get {


                return "";
            }
        }

        public ReportByCustomer(Customer customer) {

            this.CurentCustomer = customer;

            if (Checks != null) _curentChecks = Checks.Where(x => x.Customer == customer)?.ToList();
            else throw new Exception("Не указана коллекция для обработки");
        }
    }
}
