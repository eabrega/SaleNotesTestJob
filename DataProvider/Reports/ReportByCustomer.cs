using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Sale;
using DataProvider.Users;
using System.Globalization;

namespace DataProvider.Reports
{
    public sealed class ReportByCustomer
    {
        /// <summary>
        /// Чеки по которым будет совершенна выборка
        /// </summary>
        static public List<Check> Checks;
        static public int Year;
        int _monthNumber = 0;
        List<Check> _СurentChecks { get; }
        Customer CurentCustomer { get; }
        public string Name { get { return CurentCustomer.Name; } }
        public string Average {
            get {
                if (_СurentChecks.Count > 0)
                {
                    return _СurentChecks.Average(v => v.Total).ToString("C") ;
                }
                else return (0).ToString("C");
            }
        }
        public string CostByYear {

            get {
                return _СurentChecks.Sum(x => x.Total).ToString("C");
            }
        }
        public string MaxCheckCost {
            get {


                var aveCostCheckByMonth = _СurentChecks.GroupBy(x=>x.Date.Month)
                            .Select(c=> 
                            new { month = c.Key,
                                  cost = _СurentChecks
                                            .Where(x=>x.Customer == CurentCustomer && x.Date.Month == c.Key)
                                            .Average(g=>g.Total)
                                }
                            )
                            .OrderByDescending(c=>c.cost)
                            .FirstOrDefault();


                if (aveCostCheckByMonth == null) return (0).ToString("C");
                else
                {
                    //return $"{ttt.cost.ToString("C")} в {new DateTime(1, ttt.month, 1).ToString("MMMM", new CultureInfo("ru-RU"))}";
                    _monthNumber = aveCostCheckByMonth.month;
                    return $"{aveCostCheckByMonth.cost.ToString("C")}";
                }
            }
        }
        public int MonthNumberMaxCheck { get { return _monthNumber; } }
        public ReportByCustomer(Customer customer) {

            CurentCustomer = customer;

            _СurentChecks = Checks.Where(x => x.Customer == CurentCustomer && x.Date.Year == Year).ToList();  
        }
    }
}
