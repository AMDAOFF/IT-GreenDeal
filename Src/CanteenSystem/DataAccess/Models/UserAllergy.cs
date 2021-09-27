using DataAccess.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
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
