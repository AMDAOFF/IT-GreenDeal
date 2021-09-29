using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
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
