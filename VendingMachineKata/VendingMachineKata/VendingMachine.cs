using System;
using System.Collections.Generic;

namespace VendingMachineKata
{
    /// <summary>
    /// Represents Vending Machine 
    /// </summary>
    public class VendingMachine
    {
        /// <summary>
        /// List of  Coins inserted.
        /// </summary>
        private List<Coin> coins = new List<Coin>();

        /// <summary>
        /// Amount of Coins inserted.
        /// </summary>
        private decimal amount = 0m;

        /// <summary>
        /// Represents returned coins from the Vending machine.
        /// </summary>
        private List<Coin> retutnedCoins = new List<Coin>();

        /// <summary>
        /// Represents available Products inside the Vending machine.
        /// </summary>
        private List<Product> products = new List<Product>
        {
            new Product{Name="Cola", Price=100},
            new Product{Name="Chips", Price=50},
            new Product{Name="Candy", Price=65}
        };

        /// <summary>
        /// Constant representing No Coin inserted in the vending machine and to display "INSERT COIN" in the screen.
        /// </summary>
        private string INSERTCOIN = "INSERT COIN";

        /// <summary>
        /// Constant representing Thank You upon successful product purchse.
        /// </summary>
        private string THANKYOU = "THANK YOU";

       

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public bool Insert(Coin coin)
        {
            if (coin == Coin.Penny)
            {
                retutnedCoins.Add(coin);
                return false;
            }
            else
            {
                coins.Add(coin);
                amount += (int)coin;
                return true;
            }
        }
        public string Display()
        {
            if (amount > 0)
            {
                return Amount.ToString();
            }
            return INSERTCOIN;
        }

        public string Purchase(Product product)
        {
            if(amount >= product.Price)
            {
                amount -= product.Price;
                return THANKYOU;
            }

            if(amount > 0)
            {
              return   Amount.ToString();
            }
            return INSERTCOIN;
        }

        public decimal Amount{get{ return amount / 100m; } }
        public List<Coin> ReturnedCoins { get {return retutnedCoins; } }
    }
}
