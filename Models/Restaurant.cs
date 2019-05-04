using System.Collections.Generic;

namespace TestRestaurant.Models
{
    public class Restaurant
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Plate> Plates { get; set; }
    }
}
