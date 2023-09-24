namespace RentApartmentsAPI.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity{ get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
