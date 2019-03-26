using System;
using Xunit;

namespace VendingMachineKata.UnitTests
{
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

    }
}
