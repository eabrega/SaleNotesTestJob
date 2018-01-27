
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataProvider.Sale;

namespace SaleNotesTestJob.CheckForms
{
    public partial class CheckInfo : Form
    {
        List<CheckItem> Googs { get; set; }
        public CheckInfo(Check check)
        {
            InitializeComponent();

            Googs = check.Items;

            Number.Text = check.Number.ToString();
            CustomerName.Text = check.Customer.Name;
            Date.Text = check.Date.ToString("dd.MM.yyyy");
            Statys.Text = check.PaymentType.ToString();
            TotalCost.Text = check.Total.ToString("C");

            dataGridView1.DataSource = Googs;            
        }
    }
}
