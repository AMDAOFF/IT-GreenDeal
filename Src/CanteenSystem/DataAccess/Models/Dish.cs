using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }
        public string DishName { get; set; }
        public int DishCO2 { get; set; }
        public bool DishOfTheDay { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
