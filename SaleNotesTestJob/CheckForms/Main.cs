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
            ChecksView.DataSource = Provider.GetChecksVisual();

            ChecksView.CellMouseDoubleClick += ChecksView_CellMouseDoubleClick;

            tabControl1.Selected += TabSelect_Click;
            Provider.ChecksListUpdated += Provider_ChecksListUpdated;
            Provider.RebuildReport += Provider_RebuildReport;
        }

        private void Provider_RebuildReport()
        {
            ReportMonth.DataSource = null;
            ReportCustomer.DataSource = null;
            ReportRemided.DataSource = null;

            ReportMonth.DataSource = Provider.GetReportsByMonths();
            ReportCustomer.DataSource = Provider.GetReportsByCustomers();
            ReportRemided.DataSource = Provider.GetReportReminder();
        }

        void Provider_ChecksListUpdated(DataProvider.Sale.Check obj)
        {
            ChecksView.DataSource = null;
            ChecksView.DataSource = Provider.GetChecksVisual();
        }
        void TabSelect_Click(object sender, EventArgs e)
        {
            var m = (sender as TabControl).SelectedIndex;

            if (m == 0)
            {
                ReportMonth.DataSource = Provider.GetChecksVisual();
            }
            if (m == 1)
            {
                ReportMonth.DataSource = Provider.GetReportsByMonths();
            }
            if (m == 2)
            {
                ReportCustomer.DataSource = Provider.GetReportsByCustomers();
            }
            if (m == 3)
            {
                ReportRemided.DataSource = Provider.GetReportReminder();
            }
        }
        void ChecksView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var row_selected = Provider.GetCheckByIndex(e.RowIndex);

            if (row_selected != null)
            {
                Form info = new CheckInfo(row_selected);
                info.Show();
            }
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

            int y = DateTime.Now.Year;
            int d = DateTime.Now.Day;
            int m = DateTime.Now.Month;

            for (int i = 0;  i != checks_count; i++)
            {
                DateTime data = new DateTime(y, m, d);

                var count = Provider.GetCustomers().Count - 1;
                var customer_index = rnd.Next(0, count);

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

                m--;

                if (m < 1)
                {
                    m = 12;
                    y--;
                }
            }
        }
        void NewCkeck_Click(object sender, EventArgs e)
        {
            Form newChecks = new NewCheck();
            newChecks.Show();
        }

        void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form ttt = new DateInsert();
            ttt.Show();
        }
    }
}
