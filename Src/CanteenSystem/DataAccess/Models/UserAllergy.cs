using Canteen.DataAccess.Identity;

namespace Canteen.DataAccess.Models
{
    public class UserAllergy
    {
        public int UserAllergyId { get; set; }

        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
