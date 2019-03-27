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

        private decimal returnAmount = 0m;
        /// <summary>
        /// Represents returned coins from the Vending machine.
        /// </summary>
        private List<Coin> returnedCoins = new List<Coin>();

        
        /// <summary>
        /// Represents the shelve containing the  Products inside the Vending machine.
        /// </summary>
        public Shelf Shelf = new Shelf { ShelfEnrties = new Dictionary<string, ShelfEnrty> {
            {"1C", new ShelfEnrty{
                 Product =new Product { Name = "Cola", Price = 1m },
                 Quntity=100,
                 UniqueShelfEntryId="1C"
                }
            },
            {"2C", new ShelfEnrty{
                 Product =new Product { Name="Chips", Price=0.5m },
                 Quntity=100,
                 UniqueShelfEntryId="2C"
                }
            },
            {"3C", new ShelfEnrty{
                 Product =new Product { Name="Candy", Price=0.65m },
                 Quntity=100,
                 UniqueShelfEntryId="3C"
                }
            }
        } };


        /// <summary>
        /// Constant representing No Coin inserted in the vending machine and to display "INSERT COIN" in the screen.
        /// </summary>
        private string INSERTCOIN = "INSERT COIN";

        /// <summary>
        /// Constant representing the Product is out of stock in the vending machine
        /// </summary>
        private string SOLDOUT = "SOLD OUT";

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
                returnedCoins.Add(coin);
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

        public string Purchase(string uniqueShelfId )
        {
            if (this.Shelf.ShelfEnrties.ContainsKey(uniqueShelfId) && this.Shelf.ShelfEnrties[uniqueShelfId].Quntity > 0)
            {
                if (this.Amount >= this.Shelf.ShelfEnrties[uniqueShelfId].Product.Price)
                {
                    amount -= this.Shelf.ShelfEnrties[uniqueShelfId].Product.Price * 100m;
                    return THANKYOU;
                }

                if (amount > 0)
                {
                    return $"PRICE: { this.Shelf.ShelfEnrties[uniqueShelfId].Product.Price.ToString()}";
                }
                return INSERTCOIN;
            }
            return SOLDOUT;
        }
        /// <summary>
        /// Helper to create denomination of coin. 
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private List<Coin> PopulateCoins(Coin coin, int length)
        {
            List<Coin> coins = new List<Coin>();
            for (int i = 0; i < length; i++)
            {

                coins.Add(coin);
            }
            return coins;
        }
        
        /// <summary>
        /// Compute the denomination of coins to be returned based on the remainin balance amount.
        /// </summary>
        /// <returns></returns>
        public List<Coin> MakeChange()
        {
            this.returnAmount = this.amount;
            this.amount = 0.0M;
           
            var returnedQuarters = (int)(this.returnAmount / (int)Coin.Quarter);
            var returnedNickels = (int) (this.returnAmount - returnedQuarters * (int)Coin.Quarter)/(int)Coin.Nickel;
            var returnedDimes = (int)(this.returnAmount - returnedQuarters * (int)Coin.Quarter - returnedNickels * (int)Coin.Nickel) / (int)Coin.Dime;
            var returnedPennies = (int)(this.returnAmount - returnedQuarters * (int)Coin.Quarter - returnedNickels * (int)Coin.Nickel - returnedDimes * (int)Coin.Dime) / (int)Coin.Penny;
            this.returnedCoins = new List<Coin>(returnedQuarters + returnedNickels + returnedDimes + returnedPennies);
            if (returnedQuarters > 0)
            {
                this.returnedCoins.AddRange(PopulateCoins(Coin.Quarter, returnedQuarters));
            }

            if (returnedNickels > 0)
            {
                this.returnedCoins.AddRange(PopulateCoins(Coin.Nickel, returnedNickels));

            }

            if (returnedDimes > 0)
            {
                this.returnedCoins.AddRange(PopulateCoins(Coin.Dime, returnedDimes));

            }

            if (returnedPennies > 0)
            {
                this.returnedCoins.AddRange(PopulateCoins(Coin.Penny, returnedPennies));

            }

            return this.returnedCoins;
        }
        
        /// <summary>
        /// Returns Coins that were not transacted.
        /// </summary>
        /// <returns>returns list of Coins.</returns>
        public List<Coin> ReturnCoins()
        {       
            this.returnedCoins = new List<Coin>(this.coins);
            this.returnAmount = this.amount;
                        
            coins.Clear();
            this.amount = 0m;

            return this.ReturnedCoins;
        }

        public decimal Amount{get{ return amount / 100m; } }
        public decimal ReturnAmount { get { return returnAmount / 100m; } }
        public List<Coin> ReturnedCoins { get {return returnedCoins; } }
    }
}
