using System;
using System.Collections.Generic;
using Xunit;

namespace VendingMachineKata.UnitTests
{
    /// <summary>
    /// Unit tests related to functionalities of Vending Machine
    /// </summary>
    public class VendingMachineTests
    {
        #region Accept Coin Feature Unit Tests
        [Fact]
        public void VendingMachineRejectsPennyCoin()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var penny = new Coin(CoinSize.Penny, CoinWeight.Penny);

            //Act
            var isPennylInserted = vendingMachine.Insert(penny);

            //Assert
            Assert.False(isPennylInserted);
        }

        [Fact]
        public void VendingMachineAcceptsDimeCoin()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var dime = new Coin(CoinSize.Dime, CoinWeight.Dime);

            //Act
            var isDimeInserted = vendingMachine.Insert(dime);

            //Assert
            Assert.True(isDimeInserted);
        }

        [Fact]
        public void VendingMachineAcceptsNickelCoin()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var nickel = new Coin(CoinSize.Nickel, CoinWeight.Nickel);
           
            //Act
            var isNickelInserted = vendingMachine.Insert(nickel);

            //Assert
            Assert.True(isNickelInserted);
        }

      
        [Fact]
        public void VendingMachineAcceptsQuarterCoin()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var quarter = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            
            //Act
            var isQuarterInserted = vendingMachine.Insert(quarter);

            //Assert
            Assert.True(isQuarterInserted);
        }

        [Fact]
        public void CoinDenominationssHaveCorrectDenominations()
        {
            //Arrange


            //Act


            //Assert
            Assert.Equal(1, (int)CoinDenomination.Penny);
            Assert.Equal(5, (int)CoinDenomination.Dime);
            Assert.Equal(10, (int)CoinDenomination.Nickel);
            Assert.Equal(25, (int)CoinDenomination.Quarter);
        }
        //When a valid coin is inserted the amount of the coin will be added to the current amount 

        [Fact]
        public void WhenValidCoinInsertedAmountOfCoinAddedToTheCurrentAmount()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            const decimal expectedAmount = (int)CoinDenomination.Quarter / 100m;
            var quarter = new Coin(CoinSize.Quarter, CoinWeight.Quarter);


            //Act
            var isQuarterInserted = vendingMachine.Insert(quarter);
         
            //Assert
            Assert.Equal(expectedAmount, vendingMachine.Amount);
        }

        [Fact]
        public void WhenNoCoinsInsertedThenMachineDisplaysINSERTCOIN()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            string expectedDisplayContent = MachineState.INSERTCOIN.ToString();

            //Act
            var actualDisplayedContent = vendingMachine.Display();

            //Assert
            Assert.Equal(expectedDisplayContent, actualDisplayedContent);
        }

        [Fact]
        public void WhenValidCoinsInsertedThenMachineDisplaysCorrectAmount()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            const decimal expectedAmount=(int) CoinDenomination.Quarter/100m;
            var quarter = new Coin(CoinSize.Quarter, CoinWeight.Quarter);


            //Act
            var isQuarterInserted = vendingMachine.Insert(quarter);
            var actualDisplayedContent = vendingMachine.Display();

            //Assert
            Assert.Equal(expectedAmount.ToString(), actualDisplayedContent);
        }

        [Fact]
        public void RejectedCoinsArePlacedInTheCoinReturn()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var penny1 = new Coin(CoinSize.Penny, CoinWeight.Penny);
            var penny2 = new Coin(CoinSize.Penny, CoinWeight.Penny);


            var expectedRecturnedCoins =new List<Coin>{penny1,penny2};

            //Act>
            var isFirstPennyInserted = vendingMachine.Insert(penny1);
            var isSecondPennyInserted = vendingMachine.Insert(penny2);
            var returnedCoins = vendingMachine.ReturnedCoins;

            //Assert
            Assert.Equal(expectedRecturnedCoins, returnedCoins);
            Assert.False(isFirstPennyInserted);
            Assert.False(isSecondPennyInserted);
        }
        #endregion

        #region Select Product Feature Unit Tests
        [Fact]
        public void PurchaseOfCandyWithSufficientFundDisplaysThankYouMessage()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var expectedMessage = MachineState.THANKYOU.ToString();
            var quarter1 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter2 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter3 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var isFirstQuartreInserted = vendingMachine.Insert(quarter1);
            var isSecondQuarterInserted = vendingMachine.Insert(quarter2);
            var isThirdQuarterInserted = vendingMachine.Insert(quarter3);
            var isPurchased = vendingMachine.Purchase("3C");
            Assert.True(isPurchased);

            //Act
            var displayMessage = vendingMachine.Display();


            //Assert
            Assert.Equal(expectedMessage, displayMessage);
          
        }

        [Fact]
        public void SuccesiveDisplayAfterThankYouMessageShowsInsertCoin()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var expectedMessage = MachineState.INSERTCOIN.ToString();
            var quarter1 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter2 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter3 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var isFirstQuartreInserted = vendingMachine.Insert(quarter1);
            var isSecondQuarterInserted = vendingMachine.Insert(quarter2);
            var isThirdQuarterInserted = vendingMachine.Insert(quarter3);
            var isPurchased = vendingMachine.Purchase("3C");
            Assert.True(isPurchased);
            var displayMessage = vendingMachine.Display();
            Assert.Equal(MachineState.THANKYOU.ToString(), displayMessage);

            //Act
            displayMessage = vendingMachine.Display();


            //Assert
            Assert.Equal(expectedMessage, displayMessage);


        }


        [Fact]
        public void PurchaseOfCandyWithInsufficientFundDisplaysProductPriceAsMessage()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();            
            var ShelfIdOfCandyToPurchase = "3C";
            var expectedPriceOfCandy = 0.65m;
            var expectedMessage = $"PRICE: { expectedPriceOfCandy.ToString()}";
            var quarter1 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter2 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var isFirstQuartreInserted = vendingMachine.Insert(quarter1);
            var isSecondQuarterInserted = vendingMachine.Insert(quarter2);
            var isPurchased = vendingMachine.Purchase(ShelfIdOfCandyToPurchase);
            Assert.False(isPurchased);

            //Act
            var displayContent = vendingMachine.Display();


            //Assert
            Assert.Equal(expectedMessage.ToString(), displayContent);

        }

        [Fact]
        public void PurchaseOfCandyWithZeroAmountDisplaysINSERTCOINMessage()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var expectedMessage = MachineState.INSERTCOIN.ToString();
            var isPurchased = vendingMachine.Purchase("3C");

            //Act>       
            var displayMessage = vendingMachine.Display();

            //Assert
            Assert.Equal(expectedMessage, displayMessage);

        }
        #endregion

        #region Make Change Feature Unit Tests
        [Fact]
        public void VendingMachineMakesCorrectChange()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var ShelfIdOfCandyToPurchase = "3C";
            var expectedReturnAmount = 0.35m;
           
            var quarter1 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter2 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter3 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter4 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);

            var returnedQuarter = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var returnedNickel = new Coin(CoinSize.Nickel, CoinWeight.Nickel);

            var expectedCoinsToBeReturned =new  List<Coin> { returnedQuarter, returnedNickel };
            var candy = new Product { Name = "Candy", Price = 0.65m };

            //Act
            vendingMachine.Insert(quarter1);
            vendingMachine.Insert(quarter2);
            vendingMachine.Insert(quarter3);
            vendingMachine.Insert(quarter4);
           // vendingMachine.Purchase(ShelfIdOfCandyToPurchase);
            var returnedCoins= vendingMachine.MakeChange(candy);
            var returnedAmount = vendingMachine.ReturnAmount;


            //Assert
            Assert.Equal(expectedCoinsToBeReturned.Count, returnedCoins.Count);            
            //Size of the first expectd coin could be equal to the 1st or the 2nd actually returned coin
            Assert.True(expectedCoinsToBeReturned[0].Size == returnedCoins[0].Size || expectedCoinsToBeReturned[0].Size == returnedCoins[1].Size);
            //Weight of the first expectd coin could be equal to the 1st or the 2nd actually returned coin
            Assert.True(expectedCoinsToBeReturned[0].Weight == returnedCoins[0].Weight || expectedCoinsToBeReturned[0].Weight == returnedCoins[1].Weight);
            //Size of the second expectd coin could be equal to the 1st or the 2nd actually returned coin
            Assert.True(expectedCoinsToBeReturned[1].Size == returnedCoins[1].Size || expectedCoinsToBeReturned[1].Size == returnedCoins[0].Size);
            //Weight of the second expectd coin could be equal to the 1st or the 2nd actually returned coin
            Assert.True(expectedCoinsToBeReturned[1].Weight == returnedCoins[1].Weight || expectedCoinsToBeReturned[1].Weight == returnedCoins[0].Weight);

            Assert.Equal(expectedReturnAmount, returnedAmount);
        }
        #endregion

        #region Return Coins Feature Unit Tests
        [Fact]
        public void VendingMachineReturnsCoinsCorrectly()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var expectedReturnAmount = 1m;
            var quarter1 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter2 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter3 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter4 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);

            var expectedCoinsToBeReturned = new List<Coin> { quarter1, quarter2, quarter3, quarter4 };
            var expectedDisplayMessage = MachineState.INSERTCOIN.ToString();
            //Act
            vendingMachine.Insert(quarter1);
            vendingMachine.Insert(quarter2);
            vendingMachine.Insert(quarter3);
            vendingMachine.Insert(quarter4);
            
            var returnedCoinsAfterPurchase = vendingMachine.ReturnCoins();
            var returnedAmount = vendingMachine.ReturnAmount;
            var displayedMessage = vendingMachine.Display();

            //Assert
            Assert.Equal(expectedCoinsToBeReturned, returnedCoinsAfterPurchase);
            Assert.Equal(expectedReturnAmount, returnedAmount);
            Assert.Equal(expectedDisplayMessage, displayedMessage);
        }
        #endregion

        #region Sold Out Feature Unit Tests
        [Fact]
        public void VendingMachineShowsSoldOutWhenWewantToPurchaseUnavailableProduct()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var ShelfIdOfUnavailableProducsToPurchase = "4C";
            var expectedPurchaseMessage = MachineState.SOLDOUT.ToString();

            var quarter1 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter2 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter3 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter4 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);

            vendingMachine.Insert(quarter1);
            vendingMachine.Insert(quarter2);
            vendingMachine.Insert(quarter3);
            vendingMachine.Insert(quarter4);
            var isPurchased = vendingMachine.Purchase(ShelfIdOfUnavailableProducsToPurchase);


            //Act
            var displayMessage = vendingMachine.Display();

            //Assert
            Assert.Equal(expectedPurchaseMessage, displayMessage);
            Assert.Equal(MachineState.INSERTCOIN, vendingMachine.MachineState);           
        }

        [Fact]
        public void VendingMachineShowsAmountOfInsertedCoinstAfterSoldOutDisplayWhenCoinWasInserted()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var ShelfIdOfUnavailableProducsToPurchase = "4C";
            var expectedPurchaseMessage = MachineState.SOLDOUT.ToString();
          
            var quarter1 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter2 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter3 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);
            var quarter4 = new Coin(CoinSize.Quarter, CoinWeight.Quarter);

            vendingMachine.Insert(quarter1);
            vendingMachine.Insert(quarter2);
            vendingMachine.Insert(quarter3);
            vendingMachine.Insert(quarter4);
            var isPurchased = vendingMachine.Purchase(ShelfIdOfUnavailableProducsToPurchase);
            var expectedInsertAmount = vendingMachine.Amount.ToString();

            var displayMessage = vendingMachine.Display();
            Assert.Equal(expectedPurchaseMessage, displayMessage);
            Assert.Equal(MachineState.INSERTCOIN, vendingMachine.MachineState);

            //Act
             displayMessage = vendingMachine.Display();

            //Assert
            Assert.Equal(expectedInsertAmount, displayMessage);
            Assert.Equal(MachineState.INSERTCOIN, vendingMachine.MachineState);

        }

        [Fact]
        public void VendingMachineShowsInsertCoinAfterSoldOutDisplayWhenNoCoinWasInserted()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var ShelfIdOfUnavailableProducsToPurchase = "4C";
            var expectedPurchaseMessage = MachineState.SOLDOUT.ToString();
            var expectedInsertCoinMessage = MachineState.INSERTCOIN.ToString();

           
            var isPurchased = vendingMachine.Purchase(ShelfIdOfUnavailableProducsToPurchase);
            var displayMessage = vendingMachine.Display();
            Assert.Equal(expectedPurchaseMessage, displayMessage);
            Assert.Equal(MachineState.INSERTCOIN, vendingMachine.MachineState);

            //Act
            displayMessage = vendingMachine.Display();

            //Assert
            Assert.Equal(expectedInsertCoinMessage, displayMessage);
            Assert.Equal(MachineState.INSERTCOIN, vendingMachine.MachineState);

        }


        #endregion
    }
}
