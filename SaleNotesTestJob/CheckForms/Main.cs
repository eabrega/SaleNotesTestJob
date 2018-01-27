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

namespace SaleNotesTestJob.CheckForms
{
    public partial class Main : Form
    {
        public Main()

        {
            InitializeComponent();

            DataSetInit(30, 12); // создание случайных чеков

            ChecksView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ChecksView.DataSource = Provider.GetChecksVisual().OrderBy(x => x.Data).ToList();

            ChecksView.CellMouseDoubleClick += ChecksView_CellMouseDoubleClick;

            tabControl1.Selected += TabSelect_Click;
            Provider.ChecksListUpdated += Provider_ChecksListUpdated;         
        }

        private void Provider_ChecksListUpdated(DataProvider.Sale.Check obj)
        {
            ChecksView.DataSource = null;
            ChecksView.DataSource = Provider.GetChecksVisual().OrderBy(x => x.Data).ToList();
        }

        void TabSelect_Click(object sender, EventArgs e)
        {
            var m = (sender as TabControl).SelectedIndex;

            if (m == 0) {
                ReportMonth.DataSource = Provider.GetChecksVisual().OrderBy(x => x.Data).ToList();
            }
            if (m == 1)
            {
                ReportMonth.DataSource = Provider.GetReportsByMonths(2016);
            }
            if (m == 2)
            {
                ReportCustomer.DataSource = Provider.GetReportsByCustomers(2016);
            }
            if (m == 3)
            {
                ReportRemided.DataSource = Provider.GetReportReminder();
            }
        }
        void ChecksView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var row_index = Provider.GetCheckByIndex(e.RowIndex);

            Form info = new CheckInfo(row_index);
            info.Show();
        }
        void DataSetInit(int count_googs, int checks_count)
        {
            Provider.AddCustomer("Брега", "eabrega@gmail.com");
            Provider.AddCustomer("Ушакова", "babyy@gmail.com");
            Provider.AddCustomer("Ломакина", "lomakin@gmail.com");
            Provider.AddCustomer("Пушкин", "puskin@gmail.com");

            Provider.AddGoods("Шайба М10", 2);
            Provider.AddGoods("Винт М10", 25);
            Provider.AddGoods("Шуруповерт Makita f56", 11430.40);
            Provider.AddGoods("Молоток", 720.72);
            Provider.AddGoods("Бензин АИ-95", 38.40);
            Provider.AddGoods("Гвоздь", 10.86);
            Provider.AddGoods("Шуруп", 17.40);

            Random rnd = new Random(DateTime.Now.Millisecond);

            for (int i = 0, m = 1; i != checks_count; i++, m++)
            {
                var count = Provider.GetCustomers().Count - 1;
                var customer_index = rnd.Next(0, count);

                m = (m > 12 ? m = 1 : m);

                DateTime data = new DateTime(2016, m, 12);

                var newChecks = Provider.MakeCheck(Provider.GetCustomers()[customer_index], data);

                foreach (var item in Provider.GetGoods())
                {
                    var quantity = rnd.Next(0, count_googs);
                    Provider.AddCheckOrdeItem(newChecks, item, quantity);
                }

                // виртуально совершим оплаты по всем чекам

                foreach (var item in Provider.GetChecks())
                {
                    Provider.CloseCheck(item, DataProvider.Sale.ePayment.MasterCard);
                }
            }
        }
        void NewCkeck_Click(object sender, EventArgs e)
        {
            Form newChecks = new NewCheck();
            newChecks.Show();
        }
    }
}
