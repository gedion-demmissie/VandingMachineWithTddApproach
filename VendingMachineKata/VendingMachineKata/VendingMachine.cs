using System;
using System.Collections.Generic;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        public List<Coin> Coins = new List<Coin>();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public bool Insert(Coin coin)
        {
           if(coin == Coin.Penny)
            {
                return false;
            }
            else
            {
                Coins.Add(coin);
                return true;
            }
        }
    }
}
