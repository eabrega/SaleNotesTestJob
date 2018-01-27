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
using DataProvider.Users;
using DataProvider.Sale;

namespace SaleNotesTestJob.CheckForms
{
    public partial class NewCheck : Form
    {
        List<CheckItem> items = new List<CheckItem>();
        DateTime Date { get; set; }
        Customer Customer { get; set; }
        public NewCheck()
        {
            InitializeComponent();

            comboBox1.DataSource = Provider.GetCustomers();

            //CheckItems.DataSource = items;
            CheckItems.AllowUserToAddRows = false;
            CheckItems.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;


            Goods.DataSource = Provider.GetGoods();

            Goods.DisplayMember = "Name";
            Goods.ValueType = typeof(Guid);
            Goods.ValueMember = "Identity";
            Goods.FlatStyle = FlatStyle.Popup;

            Cost.ReadOnly = true;
            Price.ReadOnly = true;

            CheckItems.CellValueChanged += CheckItems_CellValueChanged;
        }
        void CheckItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Guid selectGuid = Guid.Empty;
            Goods selectedGoods = null;

            double quantity = Convert.ToDouble(CheckItems.CurrentRow.Cells[Quantity.Name].Value);

            if (CheckItems.CurrentRow.Cells[Goods.Name].Value != null)
            {
                selectGuid = (Guid)CheckItems?.CurrentRow?.Cells[Goods.Name]?.Value;
                selectedGoods = Provider.GetGoods().Find(x => x.Identity == selectGuid);

                CheckItems.CurrentRow.Cells[Price.Name].Value = selectedGoods.Price.ToString("C");
            }

            if (selectedGoods != null && quantity > 0)
            {
                CheckItems.CurrentRow.Cells[Cost.Name].Value = selectedGoods.Price * quantity;
            }
        }
        void CustomerSelected(object sender, EventArgs e)
        {
            Customer = comboBox1.SelectedItem as Customer ?? null;
        }
        void DateSelect(object sender, EventArgs e)
        {
            Date = dateTimePicker1.Value;
        }
        void CheckItems_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                var selectedRowIndex = CheckItems.SelectedCells.Cast<DataGridViewCell>().Select(cell => cell.RowIndex);

                foreach (var item in selectedRowIndex)
                {
                    CheckItems.Rows.RemoveAt(item);
                }
            }
        }
        void AddGoods(object sender, EventArgs e)
        {
            items.Add(new CheckItem());
        }
        void CreateCheck(object sender, EventArgs e)
        {
            var check = Provider.MakeCheck(Customer, Date);


            var goods = CheckItems.Rows;



           // Provider.AddCheckOrdeItem(check, );
        }
    }
}
