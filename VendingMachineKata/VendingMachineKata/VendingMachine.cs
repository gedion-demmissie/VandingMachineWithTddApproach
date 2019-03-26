using System;
using System.Collections.Generic;

namespace VendingMachineKata
{
    /// <summary>
    /// Represents Vending Machine 
    /// </summary>
    public class VendingMachine
    {
        private List<Coin> Coins = new List<Coin>();
        private decimal amount = 0m;
        private string INSERTCOIN = "INSERT COIN";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public bool Insert(Coin coin)
        {
            if (coin == Coin.Penny)
            {
                return false;
            }
            else
            {
                Coins.Add(coin);
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

        public decimal Amount{get{ return amount / 100m; } }
    }
}
