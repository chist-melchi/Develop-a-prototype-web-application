using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AgricultureEnergyConnect.Models
{
    public class FarmerProduct
    {
        public int FarmerProductId { get; set; }

        public string FarmerName { get; set; }

        public string Location { get; set; }
        public Farmer Farmer { get; set; }


        public int ProductId { get; set; }
        //public Product Product { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
