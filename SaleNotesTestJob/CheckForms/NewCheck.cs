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
        DateTime Date { get; set; }
        Customer Customer { get; set; }
        public NewCheck()
        {
            InitializeComponent();

            comboBox1.DataSource = Provider.GetCustomers();

            CheckItems.AllowUserToAddRows = false;
            CheckItems.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

            Goods.DataSource = Provider.GetGoods();

            Goods.DisplayMember = "Name";
            Goods.ValueType = typeof(Guid);
            Goods.ValueMember = "Identity";
            Goods.FlatStyle = FlatStyle.Popup;

            Cost.ReadOnly = true;
            Price.ReadOnly = true;

            CheckItems.CellValueChanged += GoodsSelect;

            Date = dateTimePicker1.Value;
        }
        void GoodsSelect(object sender, DataGridViewCellEventArgs e)
        {
            Guid selectGuid = Guid.Empty;
            Goods selectedGoods = null;

            int quantity = Convert.ToInt32(CheckItems.CurrentRow.Cells[Quantity.Name].Value);

            if (CheckItems.CurrentRow.Cells[Goods.Name].Value != null)
            {
                selectGuid = (Guid)CheckItems?.CurrentRow?.Cells[Goods.Name]?.Value;
                selectedGoods = Provider.GetGoods().Find(x => x.Identity == selectGuid);

                CheckItems.CurrentRow.Cells[Price.Name].Value = selectedGoods.Price.ToString("C");
            }

            if (selectedGoods != null && quantity > 0)
            {
                CheckItems.CurrentRow.Cells[Cost.Name].Value = (selectedGoods.Price * quantity).ToString("C");
            }

            label4.Text = GetGoodsCost();
        }
        void CustomerSelected(object sender, EventArgs e)
        {
            Customer = (Customer)comboBox1.SelectedItem;
        }
        void DateSelect(object sender, EventArgs e)
        {
            Date = dateTimePicker1.Value;
        }
        void DellRow(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                var selectedRowIndex = CheckItems.SelectedCells.Cast<DataGridViewCell>().Select(cell => cell.RowIndex);

                foreach (var item in selectedRowIndex)
                {
                    CheckItems.Rows.RemoveAt(item);
                }
            }

            label4.Text = GetGoodsCost();
        }
        void AddGoods(object sender, EventArgs e)
        {
            CheckItems.Rows.Add();
        }
        void CreateCheck(object sender, EventArgs e)
        {
            var check = Provider.MakeCheck(Customer, Date);

            foreach (DataGridViewRow item in CheckItems.Rows)
            {
                var goods = Provider.GetGoodsById((Guid?)item.Cells[Goods.Name].Value);
                var quantity = Convert.ToInt32(item.Cells[Quantity.Name].Value);

                if (goods != null) Provider.AddCheckOrdeItem(check, goods, quantity);
            }

            if (check.Items.Count < 1)
                Provider.RemoveCheck(check);
            Close();
        }
        string GetGoodsCost() {

            double cost = 0;

            foreach (DataGridViewRow item in CheckItems.Rows)
            {
                var goodsPrice = Provider.GetGoodsById((Guid?)item.Cells[Goods.Name].Value ?? null)?.Price ?? 0;
                var quantity = Convert.ToInt32(item.Cells[Quantity.Name].Value);

                cost += (goodsPrice * quantity);
            }

            return cost.ToString("C");
        }
    }
}
