using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Sale;

namespace DataProvider.Reports
{
    public sealed class ReportByMonth
    {
        /// <summary>
        /// Чеки по которым будет совершенна выборка
        /// </summary>
        static public List<Check> Checks;
        static public int Year;
        List<Check> _CurentChecks { get; }
        public byte MonthNumber { get; }
        public string PaymentCost { get { return _CurentChecks.Sum(x => x.Total).ToString("C"); } }
        public string Average {
            get {
                if (_CurentChecks.Count > 0)
                {

                    return _CurentChecks.Average(v => v.Total).ToString("C");
                }
                else return null;
            }
        }
        public string TopSelling {
            get {

                List<CheckItem> goods = _CurentChecks.SelectMany(x => x.Items).ToList();

                var topsalling = goods.GroupBy(x => x.SaleGoods)
                    .Select(sel => new { Name = sel.Key, Quantity = sel.Sum(v => v.Quantity) })
                    .OrderByDescending(x => x.Quantity)
                    .FirstOrDefault();


                return $"{topsalling?.Name} {topsalling?.Quantity.ToString() ?? "0"} шт.";
            }

        }
        public string BestCustomer {
            get {

                var result = _CurentChecks
                    .Select(
                        x => new
                        {
                            Customer = x.Customer,
                            CountSale = _CurentChecks.Where(c => c.Customer == x.Customer).Count(),
                        }
                     )
                     .OrderByDescending(x => x.CountSale)
                     .FirstOrDefault();

                return $"{result?.Customer?.Name} Всего закрытых чеков: {result?.CountSale ?? 0}";
            }
        }
        public ReportByMonth(byte month)
        {
            this.MonthNumber = month;
            _CurentChecks = Checks.Where(x => x.Date.Month == month && x.Date.Year == Year).ToList();


           // if (_CurentChecks.Count() < 1 && Year > 0) throw new Exception("За указанный год нет чеков");
        }
    }
}
