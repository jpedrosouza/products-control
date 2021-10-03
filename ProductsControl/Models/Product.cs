using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsControl.Models
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ean { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
