namespace AgricultureEnergyConnect.Models
{
    public class Farmer
    {
        public int FarmerProductId { get; set; }
        public string FarmerName { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public ICollection<FarmerProduct> FarmerProducts { get; set; }
        public ICollection<Product> Product { get; set; }
    } 
}
