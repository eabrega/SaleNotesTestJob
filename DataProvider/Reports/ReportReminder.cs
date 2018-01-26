using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataProvider.Sale;
using DataProvider.Users;

namespace DataProvider.Reports
{
    public sealed class ReportReminder
    {
        static public List<Check> Checks;
        public List<ReportRow> reports { get; set; } = new List<ReportRow> ();
        public List<ReportRow> Reporting()
        {
            // все те кто когда либо купил инструмент
            var checks = Checks
                .SelectMany(u => u.Items, (c, s) => new { customer = c.Customer, sale = s.SaleGoods })
                .Where(x => x.sale.Name.Contains("Шуруповерт") || x.sale.Name.Contains("Молоток"))
                .Select(x => x.customer)
                .GroupBy(e => e.Name)
                .ToList();

            foreach (var item in checks)
            {
                var checks2 = Checks
                    .SelectMany(u => u.Items, (c, s) => new { check = c, sale = s.SaleGoods.Name })
                    .Where(x => (x.sale == "Гвоздь" || x.sale == "Шуруп") && x.check.Customer.Name == item.Key)
                    .Select(x => new { number = x.check.Number, goods = x.sale, data = x.check.Date })
                    .OrderByDescending(x => x.data.Month);

                var m = DateTime.Now.Month - 2 > 0 ? DateTime.Now.Month : 12 + DateTime.Now.Month - 2;

                var gvozd = checks2.Where(x => x.goods == "Гвоздь" && checks2.Max(v => v.data.Month) < m).FirstOrDefault();
                var shurup = checks2.Where(x => x.goods == "Шуруп" && checks2.Max(v => v.data.Month) < m).FirstOrDefault();

                if (gvozd!=null) reports.Add(new ReportRow(item.Key, gvozd.goods, gvozd.data));
                if (shurup != null) reports.Add(new ReportRow(item.Key, shurup.goods, shurup.data));
            }

            return reports;
        }
    }

    public sealed class ReportRow{
        public string Name { get; }
        public string Goods { get; }
        public DateTime Date { get; }

        public ReportRow(string name, string goods, DateTime date) {

            this.Name = name;
            this.Goods = goods;
            this.Date = date;
        }
    } 
}




