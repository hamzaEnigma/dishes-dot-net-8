

namespace Restaurants.Domin.Entities
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public Guid RestauratId { get; set; }
        public int? KiloCalories { get; set; }


    }
}
