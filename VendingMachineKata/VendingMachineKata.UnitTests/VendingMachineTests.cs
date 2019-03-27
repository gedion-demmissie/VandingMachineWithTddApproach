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

            //Act
            var isPennylInserted = vendingMachine.Insert(Coin.Penny);

            //Assert
            Assert.False(isPennylInserted);
        }

        [Fact]
        public void VendingMachineAcceptsDimeCoin()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();

            //Act
            var isDimeInserted = vendingMachine.Insert(Coin.Dime);

            //Assert
            Assert.True(isDimeInserted);
        }

        [Fact]
        public void VendingMachineAcceptsNickelCoin()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();

            //Act
            var isNickelInserted = vendingMachine.Insert(Coin.Nickel);

            //Assert
            Assert.True(isNickelInserted);
        }

      
        [Fact]
        public void VendingMachineAcceptsQuarterCoin()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();

            //Act
            var isQuarterInserted = vendingMachine.Insert(Coin.Quarter);

            //Assert
            Assert.True(isQuarterInserted);
        }

        [Fact]
        public void CoinsHaveCorrectDenominations()
        {
            //Arrange


            //Act


            //Assert
            Assert.Equal(1, (int)Coin.Penny);
            Assert.Equal(5, (int)Coin.Dime);
            Assert.Equal(10, (int)Coin.Nickel);
            Assert.Equal(25, (int)Coin.Quarter);
        }
        //When a valid coin is inserted the amount of the coin will be added to the current amount 

        [Fact]
        public void WhenValidCoinInsertedAmountOfCoinAddedToTheCurrentAmount()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            const decimal expectedAmount = (int)Coin.Quarter / 100m;
           
            //Act
            var isQuarterInserted = vendingMachine.Insert(Coin.Quarter);
         
            //Assert
            Assert.Equal(expectedAmount, vendingMachine.Amount);
        }

        [Fact]
        public void WhenNoCoinsInsertedThenMachineDisplaysINSERTCOIN()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            const string expectedDisplayContent = "INSERT COIN";

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
            const decimal expectedAmount=(int) Coin.Quarter/100m;

            //Act
            var isQuarterInserted = vendingMachine.Insert(Coin.Quarter);
            var actualDisplayedContent = vendingMachine.Display();

            //Assert
            Assert.Equal(expectedAmount.ToString(), actualDisplayedContent);
        }

        [Fact]
        public void RejectedCoinsArePlacedInTheCoinReturn()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var expectedRecturnedCoins=new List<Coin>{Coin.Penny,Coin.Penny};

            //Act>
            var isFirstPennyInserted = vendingMachine.Insert(Coin.Penny);
            var isSecondPennyInserted = vendingMachine.Insert(Coin.Penny);
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
            var expectedRecturnedCoins = new List<Coin> { Coin.Penny, Coin.Penny };
            var expectedMessage = "THANK YOU";

            //Act
            var isFirstQuartreInserted = vendingMachine.Insert(Coin.Quarter);
            var isSecondQuarterInserted = vendingMachine.Insert(Coin.Quarter);
            var isThirdQuarterInserted = vendingMachine.Insert(Coin.Quarter);
            var purchaseStateContent = vendingMachine.Purchase("3C");


            //Assert
           Assert.Equal(expectedMessage, purchaseStateContent);
          
        }

        [Fact]
        public void PurchaseOfCandyWithInsufficientFundDisplaysProductPriceAsMessage()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();            
            var ShelfIdOfCandyToPurchase = "3C";
            var expectedPriceOfCandy = 0.65m;
            var expectedMessage = $"PRICE: { expectedPriceOfCandy.ToString()}";

            //Act
            var isFirstQuartreInserted = vendingMachine.Insert(Coin.Quarter);
            var isSecondQuarterInserted = vendingMachine.Insert(Coin.Quarter);
           
            var purchaseStateContent = vendingMachine.Purchase(ShelfIdOfCandyToPurchase);


            //Assert
            Assert.Equal(expectedMessage.ToString(), purchaseStateContent);

        }

        [Fact]
        public void PurchaseOfCandyWithZeroAmountDisplaysINSERTCOINMessage()
        {
            //Arrange
            VendingMachine vendingMachine = new VendingMachine();
            var expectedRecturnedCoins = new List<Coin> { Coin.Penny, Coin.Penny };
            var expectedMessage = "INSERT COIN";

            //Act>       
            var purchaseStateContent = vendingMachine.Purchase("3C");


            //Assert
            Assert.Equal(expectedMessage, purchaseStateContent);

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
            var expectedCoinsToBeReturned =new  List<Coin> { Coin.Quarter,Coin.Nickel};

            //Act
            vendingMachine.Insert(Coin.Quarter);
            vendingMachine.Insert(Coin.Quarter);
            vendingMachine.Insert(Coin.Quarter);
            vendingMachine.Insert(Coin.Quarter);
            vendingMachine.Purchase(ShelfIdOfCandyToPurchase);
            var returnedCoinsAfterPurchase= vendingMachine.MakeChange();
            var returnedAmount = vendingMachine.ReturnAmount;

            //Assert
            Assert.Equal(expectedCoinsToBeReturned , returnedCoinsAfterPurchase);
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
            var expectedCoinsToBeReturned = new List<Coin> { Coin.Quarter, Coin.Quarter, Coin.Quarter, Coin.Quarter };
            //Act
            vendingMachine.Insert(Coin.Quarter);
            vendingMachine.Insert(Coin.Quarter);
            vendingMachine.Insert(Coin.Quarter);
            vendingMachine.Insert(Coin.Quarter);
            
            var returnedCoinsAfterPurchase = vendingMachine.ReturnCoins();
            var returnedAmount = vendingMachine.ReturnAmount;
            var displayedMessage = vendingMachine.Display();

            //Assert
            Assert.Equal(expectedCoinsToBeReturned, returnedCoinsAfterPurchase);
            Assert.Equal(expectedReturnAmount, returnedAmount);
            Assert.Equal("INSERT COIN", displayedMessage);
        }
        #endregion
    }
}
