namespace AgricultureEnergyConnect.Models
{
    public class ForumPost
    {
        public int FarmerProductId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Author { get; set; }
    }
}
