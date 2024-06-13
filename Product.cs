using System;
using System.ComponentModel.DataAnnotations;

namespace AgricultureEnergyConnect.Models
{
    public class Product
    {
        public int ProductId { get; set; }
     
        public string FarmerName { get; set; }

        public string Category { get; set; }

        public DateTime ProductionDate { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }

        public int FarmerProductId { get; set; }
        public Farmer Farmer { get; set; }

        //public FarmerProduct Farmer { get; set; }

        public FarmerProduct FarmerProduct { get; set; }

        //public ICollection<Product> Product { get; set; }
        public ICollection<FarmerProduct> FarmerProducts { get; set; }
    }
}
