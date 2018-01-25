using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataProvider;
using DataProvider.Sale;
using DataProvider.Users;
using DataProvider.Reports;

namespace SaleNotesTestJob.CheckForms
{
    public partial class Main : Form
    {
        Provider SaleDataSet = new Provider();

        List<Goods> goods = new List<Goods>();
        List<Customer> customers = new List<Customer>();
        public Main()

        {
            InitializeComponent();

            customers.Add(new Customer("Брега", "eabrega@gmail.com"));
            customers.Add(new Customer("Ушакова", "babyy@gmail.com"));
            customers.Add(new Customer("Ломакина", "lomakin@gmail.com"));
            customers.Add(new Customer("Пушкин", "puskin@gmail.com"));

            goods.Add(new Goods("Гвоздь", 10.86));
            goods.Add(new Goods("Шуруп", 17.40));
            goods.Add(new Goods("Шайба М10", 2));
            goods.Add(new Goods("Винт М10", 25.95));
            goods.Add(new Goods("Шуруповерт Makita f56", 11430.40));
            goods.Add(new Goods("Пылесос Bosch", 6726.72));
            goods.Add(new Goods("Бензин АИ-95", 38.40));


            Random rnd = new Random(DateTime.Now.Millisecond);

            for (int i = 0, m = 1; i != 70; i++,m++)
            {
                List<CheckItem> checkItems = new List<CheckItem>();

                foreach (var item in goods)
                {
                    var quantity = rnd.Next(0, 20);
                    checkItems.Add(new CheckItem(item, quantity));
                }
                var customer_index = rnd.Next(0, 3);

                m = (m > 12 ? m = 1 : m);

                DateTime data = new DateTime(2016, m, 12);

                SaleDataSet.MakeCheck(checkItems, customers[customer_index], data, ePayment.Cash);
            }

            ChecksView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ChecksView.DataSource = SaleDataSet.GetChecksVisual();

            ChecksView.CellMouseDoubleClick += ChecksView_CellMouseDoubleClick;


            List<ReportByMonth> ReportsByMonth = new List<ReportByMonth>();

            for (int i = 1; i < 13; i++)
            {
                ReportsByMonth.Add(new ReportByMonth(SaleDataSet.GetChecks(), (byte)i));
            }


            ReportMonth.DataSource = ReportsByMonth;

        }

        void ChecksView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var row_index = SaleDataSet.GetCheckByIndex(e.RowIndex);

            Form info = new CheckInfo.CheckInfo(row_index);
            info.Show();
        }
    }
}
