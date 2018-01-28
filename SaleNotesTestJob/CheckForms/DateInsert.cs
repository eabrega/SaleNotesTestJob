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
    public partial class DateInsert : Form
    {
        public DateInsert()
        {
            InitializeComponent();
            textBox1.Text = DateTime.Now.Year.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Provider.ReportYear = new DateTime(Convert.ToInt16(textBox1.Text), 1, 1).Year;
                Close();
            }
            catch (Exception err) {
                MessageBox.Show("Веденное значение не является корретным для года!");
            }
        }
    }
}
