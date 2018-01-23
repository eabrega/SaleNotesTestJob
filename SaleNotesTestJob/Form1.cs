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

namespace SaleNotesTestJob
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Provider SaleDataSet = new Provider();

            Customer Brega = new Customer("Брега", "eabrega@gmail.com");
            Customer Babus = new Customer("Бабушкин", "babyy@gmail.com");
            Customer Lomakin = new Customer("Ломакина", "lomakin@gmail.com");

            SaleDataSet.AddGoods("Гвоздь", 10.86);
            SaleDataSet.AddGoods("Шуруп", 17.40);
            SaleDataSet.AddGoods("Шайба М10", 9);
            SaleDataSet.AddGoods("Винт М10", 25);


            SaleDataSet.AddCustomer("Ушакова", "yshakoff@gmail.com");



            List<CheckItem> ttt = new List<CheckItem>() {

                new CheckItem(new Goods("Гвоздь", 10.86), 534),
                new CheckItem(new Goods("Шайба М10", 5.05), 37),
                new CheckItem(new Goods("Винт", 25), 100),
            };

            SaleDataSet.MakeCheck(ttt, Brega, DateTime.Now);
            SaleDataSet.MakeCheck(ttt, Babus, DateTime.Now);
            SaleDataSet.MakeCheck(ttt, Lomakin, DateTime.Now);
            SaleDataSet.MakeCheck(ttt, Brega, DateTime.Now);


            var mmm = SaleDataSet.GetChecks();



        }
    }
}
