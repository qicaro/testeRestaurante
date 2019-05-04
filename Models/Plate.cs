
namespace TestRestaurant.Models
{
    public class Plate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

    }
}
