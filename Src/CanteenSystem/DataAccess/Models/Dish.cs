using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Canteen.DataAccess.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }
        public string DishName { get; set; }
        public int DishCO2 { get; set; }
        public bool DishOfTheDay { get; set; }

        public List<DishIngredient> DishIngredients { get; set; }
    }
}
