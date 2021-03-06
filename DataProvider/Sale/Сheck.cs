﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Users;
using System.Diagnostics;

namespace DataProvider.Sale
{
    /// <summary>
    /// Товарный чек
    /// </summary>
    [DebuggerDisplay("{Number} {Customer.Name} - {Total} р.")]
     public class Check
    {
        static int _number = 0;
        public Guid Identity { get; } = Guid.NewGuid();
        public DateTime Date { get; }
        public long Number { get; }
        public Customer Customer { get; }
        public List<CheckItem> Items { get; } = new List<CheckItem>();
        public ePayment PaymentType { get; set; }
        /// <summary>
        ///  Цена всех позиций в чеке, вычисляемое поле
        /// </summary>
        public double Total {
            get {
                return Items.Sum(x => x.Quantity * x.SaleGoods.Price);
            }
        }
        /// <summary>
        /// Форматированная строка таблицы
        /// </summary>
        public CheckVisualiser LikeTableRow { get; }
        public Check(Customer customer, DateTime date)
        {
            _number++;
            Number = _number;
            Customer = customer;
            Date = date;
            PaymentType = ePayment.NoPayment;

            LikeTableRow = new CheckVisualiser(this);
        }
        public Check() {
            _number++;
            Number = _number;
        }

    }

    /// <summary>
    /// Конвертер для вывода в DataGrid
    /// </summary>
    public class CheckVisualiser
    {
        Check _check { get; }
        public long Number { get { return _check.Number; } }
        public DateTime Data { get { return _check.Date; } }
        public string Name { get { return $"{_check.Customer.Name}"; } }
        public string Email { get { return _check.Customer.Email; } }
        public string Cost { get { return $"{_check.Total.ToString("C")}"; } }
        public ePayment Payment { get { return _check.PaymentType; } }
        public CheckVisualiser(Check check) {
            _check = check;
        }
    }

    public enum ePayment {

        NoPayment,
        Cash,
        Visa,
        MasterCard
    }
}
