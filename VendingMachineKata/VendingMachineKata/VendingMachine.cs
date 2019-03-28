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
        /// Valid Coins Dictionary to Identify legal denomination based on CoinSize and CoinWeight
        /// </summary>
        Dictionary<Tuple<CoinSize, CoinWeight>, CoinDenomination> LegalCoins = new Dictionary<Tuple<CoinSize, CoinWeight>, CoinDenomination>() {
            { Tuple.Create(CoinSize.Dime, CoinWeight.Dime),CoinDenomination.Dime },
            { Tuple.Create(CoinSize.Nickel, CoinWeight.Nickel),CoinDenomination.Nickel },
            { Tuple.Create(CoinSize.Quarter, CoinWeight.Quarter),CoinDenomination.Quarter }
        };

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
        private Shelf Shelf { get; set; }

        /// <summary>
        /// Represents machine level state.
        /// </summary>
        private static MachineState _machineState { get;set;}

        /// <summary>
        /// Constant representing No Coin inserted in the vending machine and to display "INSERT COIN" in the screen.
        /// </summary>
        private string INSERTCOIN = "INSERT COIN";

        /// <summary>
        /// Constant representing the Product is out of stock in the vending machine
        /// </summary>
        private string SOLDOUT = "SOLD OUT";

        /// <summary>
        /// item price indicate the price of the item to be purchased for  display purpose.
        /// </summary>
        private string _itemPrice;
        /// <summary>
        /// Constant representing Thank You upon successful product purchse.
        /// </summary>
       // private string THANKYOU = "THANK YOU";

        public VendingMachine()
        {
            Shelf = new Shelf
            {
                ShelfEnrties = new Dictionary<string, ShelfEnrty> {
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
        }
            };
            _machineState = MachineState.INSERTCOIN;

        }

        public VendingMachine(Shelf shelf) => this.Shelf = shelf;


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public bool Insert(Coin coin)
        {
            var coinIdentificationKey = Tuple.Create(coin.Size, coin.Weight);
            
            if (LegalCoins.ContainsKey(coinIdentificationKey))
            {
                amount +=(int)LegalCoins[coinIdentificationKey];
                coins.Add(coin);
                return true;
            }
            returnedCoins.Add(coin);
            return false;         
        }
        public string Display()
        {
            if (_machineState == MachineState.INSERTCOIN)
            {
                if (amount > 0)
                {
                    return Amount.ToString();
                }
                else
                {
                    return MachineState.INSERTCOIN.ToString();
                }
            }
            else if(_machineState == MachineState.PURCHASED)
            {
                //Convert the machine state to Insert coin once displaying "THANKYOU".
                _machineState = MachineState.INSERTCOIN;
                return MachineState.THANKYOU.ToString();
            }
            else if(_machineState == MachineState.SOLDOUT)
            {
                //Convert the machine state to Insert coin once displaying "Sold out".
                _machineState = MachineState.INSERTCOIN;
                return MachineState.SOLDOUT.ToString();
            }
            else if(_machineState == MachineState.INSUFFICIENTFUND)
            {                 
                var output=  $"PRICE: { _itemPrice.ToString()}";
                //Convert the machine state to Insert coin  after "INSUFFICIENTFUND" state.
                _machineState = MachineState.INSERTCOIN;
                _itemPrice = string.Empty;
                return output;
            }

            //Default message
            return MachineState.INSERTCOIN.ToString();

           
        }

        public bool Purchase(string uniqueShelfId )
        {
            if (this.Shelf.ShelfEnrties.ContainsKey(uniqueShelfId) && this.Shelf.ShelfEnrties[uniqueShelfId].Quntity > 0)
            {
                if (this.Amount >= this.Shelf.ShelfEnrties[uniqueShelfId].Product.Price)
                {
                    amount -= this.Shelf.ShelfEnrties[uniqueShelfId].Product.Price * 100m;
                    _machineState = MachineState.PURCHASED;
                    
                    //Return  remaining coins.
                    returnedCoins = this.ReturnCoins();
                    return true;
                }
                else if(this.Amount > 0)                
                {
                    _machineState = MachineState.INSUFFICIENTFUND;
                    _itemPrice = this.Shelf.ShelfEnrties[uniqueShelfId].Product.Price.ToString();
                    return false;
                }
                else
                {
                    _machineState = MachineState.INSERTCOIN;
                    return false;
                }
            }
            else
            {
                _machineState = MachineState.SOLDOUT;
                 return false;
            }
           
        }
        /// <summary>
        /// Helper to create denomination of coin. 
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private List<Coin> PopulateCoins(CoinSize coinSize, CoinWeight coinWeigh,  int length)
        {
            List<Coin> coins = new List<Coin>();
            for (int i = 0; i < length; i++)
            {

                coins.Add(new Coin(coinSize,coinWeigh));
            }
            return coins;
        }
        
        /// <summary>
        /// Compute the denomination of coins to be returned based on the remainin balance amount.
        /// </summary>
        /// <returns></returns>
        public List<Coin> MakeChange(Product selectedProduct)
        {
            if(selectedProduct.Price >= this.Amount)
            {
                //When there is no enough amount, then we won't have a return coins.
                return new List<Coin>();
            }
            this.returnAmount = this.amount - selectedProduct.Price*100;
            this.amount = selectedProduct.Price * 100;
           
            var returnedQuarters = (int)(this.returnAmount / (int)CoinDenomination.Quarter);
            var returnedNickels = (int) (this.returnAmount - returnedQuarters * (int)CoinDenomination.Quarter)/(int)CoinDenomination.Nickel;
            var returnedDimes = (int)(this.returnAmount - returnedQuarters * (int)CoinDenomination.Quarter - returnedNickels * (int)CoinDenomination.Nickel) / (int)CoinDenomination.Dime;
            var returnedPennies = (int)(this.returnAmount - returnedQuarters * (int)CoinDenomination.Quarter - returnedNickels * (int)CoinDenomination.Nickel - returnedDimes * (int)CoinDenomination.Dime) / (int)CoinDenomination.Penny;
            this.returnedCoins = new List<Coin>(returnedQuarters + returnedNickels + returnedDimes + returnedPennies);
            if (returnedQuarters > 0)
            {
                this.returnedCoins.AddRange(PopulateCoins(CoinSize.Quarter,CoinWeight.Quarter, returnedQuarters));
            }

            if (returnedNickels > 0)
            {
                this.returnedCoins.AddRange(PopulateCoins(CoinSize.Nickel, CoinWeight.Nickel, returnedNickels));

            }

            if (returnedDimes > 0)
            {
                this.returnedCoins.AddRange(PopulateCoins(CoinSize.Quarter, CoinWeight.Quarter, returnedDimes));

            }

            if (returnedPennies > 0)
            {
                this.returnedCoins.AddRange(PopulateCoins(CoinSize.Quarter, CoinWeight.Quarter, returnedPennies));
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

            //Preserve state if it's in a purchased state
            if (_machineState != MachineState.PURCHASED)
            {
                _machineState = MachineState.INSERTCOIN;
            }
            return this.ReturnedCoins;
        }       

        public decimal Amount{get{ return amount / 100m; } }
        public decimal ReturnAmount { get { return returnAmount / 100m; } }
        public List<Coin> ReturnedCoins { get {return returnedCoins; } }
        public MachineState MachineState { get { return _machineState; } }
    }

    public enum MachineState
    {
        INSERTCOIN,
        INSUFFICIENTFUND,
        SOLDOUT,
        THANKYOU,
        PURCHASED
    }
}
