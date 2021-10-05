using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Canteen.DataAccess.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }

        public List<IngredientAllergy> IngredientAllergies { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
    }
}
