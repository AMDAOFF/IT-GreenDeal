using System.Collections.Generic;

namespace Canteen.DataAccess.Models
{
    public class Allergy
    {
        public int AllergyId { get; set; }
        public string AllergyName { get; set; }

        public Ingredient Ingredient { get; set; }
        public List<UserAllergy> UserAllergies { get; set; }
    }
}
