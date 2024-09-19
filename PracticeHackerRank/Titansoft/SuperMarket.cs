using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticeHackerRank.Titansoft
{
    public class SuperMarket
    {
        [TestCase(1, ExpectedResult = 5)]
        [TestCase(2, ExpectedResult = 5)]
        [TestCase(3, ExpectedResult = 2)]
        [TestCase(4, ExpectedResult = 5)]

        public int PriceCheck(int amount)
        {
            var productA = new Product("A", 5, 3, 12);
            var price = productA.GetPrice(amount);
            return price;
        }
        private Cashier _cashier;
        private Product productA;
        private Product productB;
        private Product productC;

        [SetUp]
        public void Setup()
        {
            _cashier = new Cashier();
            productA = new Product("A", 5, 3, 12);
            productB = new Product("B", 4, 2, 6);
            productC = new Product("C", 3, 0, 0);
        }

        [Test]
        public void CashierDoWork()
        {
            var outputReceipt = new List<int>
            {
                _cashier.CheckOut(productA),
                _cashier.CheckOut(productA),
                _cashier.CheckOut(productB),
                _cashier.CheckOut(productC),
                _cashier.CheckOut(productA),
                _cashier.CheckOut(productB)
            };

            Assert.AreEqual(21, outputReceipt.Last());
        }


    }

    public class Product
    {
        public string Name { get; set; }
        public int SinglePrice { get; set; }
        public int DiscountAmount { get; set; }
        public int DiscountPrice { get; set; }

        public Product(string name, int singlePrice, int discountAmount, int discountPrice)
        {
            Name = name;
            SinglePrice = singlePrice;
            DiscountAmount = discountAmount;
            DiscountPrice = discountPrice;
        }

        private bool HasDiscount => DiscountAmount > 0 && DiscountPrice > 0;

        public int GetPrice(int checkedOut)
        {
            if (HasDiscount && checkedOut % DiscountAmount == 0)
            {
                return DiscountPrice - (DiscountAmount - 1) * SinglePrice;
            }
            return SinglePrice;
        }

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   Name == product.Name;
        }

    }

    public class Cashier
    {
        public int CurrentAmount { get; set; }
        private List<Product> CheckedOutList { get; set; }

        public Cashier()
        {
            CheckedOutList = new List<Product>();
            CurrentAmount = 0;
        }

        public int CheckOut(Product product)
        {
            CheckedOutList.Add(product);
            CurrentAmount += product.GetPrice(CheckedOutList.Count(x => x.Equals(product)));
            return CurrentAmount;
        }
    }

}
