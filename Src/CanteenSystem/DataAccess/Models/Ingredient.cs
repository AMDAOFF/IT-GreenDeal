using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Canteen.DataAccess.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }

        public List<Allergy> Allergies { get; set; }
        public Dish Dish { get; set; }
    }
}
