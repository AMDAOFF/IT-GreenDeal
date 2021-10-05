using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Canteen.DataAccess.Models
{
    public class Allergy
    {
        public int AllergyId { get; set; }
        public string AllergyName { get; set; }

        public List<IngredientAllergy> IngredientAllergies { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
    }
}
 