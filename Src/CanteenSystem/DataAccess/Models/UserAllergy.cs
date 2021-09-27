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
        public int UserId { get; set; }
        public string AllergyId { get; set; }

        public Allergy Allergy { get; set; }
        public ApplicationUser User { get; set; }
    }
}
