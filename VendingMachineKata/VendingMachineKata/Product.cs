using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineKata
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    public class ShelfEnrty{
       public Product Product { get; set; }
       public string UniqueShelfEntryId { get; set; }
       public int Quntity{ get; set; }
    }
    public class Shelf
    {
        public Dictionary<string,ShelfEnrty> ShelfEnrties { get; set; }
    }
}
