using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Canteen.DataAccess.Models
{
    public class Allergy
    {
        public int AllergyId { get; set; }
        public string AllergyName { get; set; }

        public Ingredient Ingredient { get; set; }
        public ICollection<UserAllergy> UserAllergies { get; set; }
    }
}
 