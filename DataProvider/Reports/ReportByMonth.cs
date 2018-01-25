using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Sale;
using DataProvider.Users;

namespace DataProvider.Reports
{
    public class ReportByMonth
    {
        List<Check> _checks { get; }
        public byte MonthNumber { get; }
        public string PaymentCost { get { return _checks.Sum(x => x.Total).ToString("C"); } }
        public string Average {
            get {
                if (_checks.Count > 0)
                {

                    return _checks.Average(v => v.Total).ToString("C");
                }
                else return null;
            }
        }
        public string TopSelling {
            get {

                List<CheckItem> goods = _checks.SelectMany(x => x.Items).ToList();

                var topsalling = goods.GroupBy(x => x.SaleGoods)
                    .Select(sel => new { Name = sel.Key, Quantity = sel.Sum(v => v.Quantity) })
                    .OrderByDescending(x => x.Quantity)
                    .FirstOrDefault();


                return $"{topsalling.Name} - {topsalling.Quantity.ToString()} шт.";
            }

        }
        public string BestCustomer {
            get {

                var result = _checks
                    .Select(
                        x=> new
                            {
                                Custoner = x.Customer,
                                CountSale = _checks.Where(c => c.Customer == x.Customer).Count(),
                            }
                     )
                     .OrderByDescending(x=>x.CountSale)
                     .FirstOrDefault();

                return $"{result.Custoner.Name} - {result.CountSale}";
            }


        }

        public ReportByMonth(List<Check> checks, byte month)
        {

            this._checks = checks.Where(x => x.Date.Month == month).ToList();
            this.MonthNumber = month;
        }
    }
}
