namespace MobileTsar.Models
{
    public class Travel
    {
        public int TravelId { get; set; }
        public virtual Client Client { get; set; }
        public int Id { get; set; }

        public string MClientAddress { get; set; }
        public double TravelRate { get; set; }
        public string Distance { get; set; }
        public double TravelFee { get; set; }
    }
}