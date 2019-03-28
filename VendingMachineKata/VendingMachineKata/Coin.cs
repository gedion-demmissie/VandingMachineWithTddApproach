using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineKata
{
    /// <summary>
    /// Represents Coin.
    /// </summary>
    public class Coin
    {

        public CoinSize Size { get; private set; }

        public CoinWeight Weight { get; private set; }


        /// <summary>
        /// Creates Immutable Coin object of certain denomination.
        /// </summary>
        /// <param name="size">Size of the coin.</param>
        /// <param name="weight">Weight of the coin.</param>     
        public Coin(CoinSize size, CoinWeight weight)
        {
            Size = size;
            Weight = weight;
        }


    }

    /// <summary>
    /// Represents Coin Size in ideal unit.
    /// </summary>
    public enum CoinSize
    {
        Penny = 1,
        Dime = 5,
        Nickel = 10,
        Quarter = 25
    }

    /// <summary>
    /// Represents Coin Weight in ideal unit.
    /// </summary>
    public enum CoinWeight
    {
        Penny = 1,
        Dime = 5,
        Nickel = 10,
        Quarter = 25
    }

    /// <summary>
    /// Represents Coin Denomination
    /// </summary>
    public enum CoinDenomination
    {
        Penny = 1,
        Dime = 5,
        Nickel = 10,
        Quarter = 25
    }
}
