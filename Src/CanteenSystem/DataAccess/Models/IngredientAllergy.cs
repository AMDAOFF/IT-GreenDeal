using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.DataAccess.Models
{
    public class IngredientAllergy
    {
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; }
    }
}
