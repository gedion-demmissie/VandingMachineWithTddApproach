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
    }
}
