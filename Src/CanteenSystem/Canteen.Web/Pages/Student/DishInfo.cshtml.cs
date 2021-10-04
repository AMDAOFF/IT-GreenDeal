using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Canteen.Service.AllergyService;
using Canteen.Service.AllergyService.Dto;
using Canteen.Service.DishService;
using Canteen.Service.DishService.Dto;
using Canteen.Service.IngridentsService;
using Canteen.Service.IngridentsService.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Canteen.Web.Pages.Student
{
    public class DishInfoModel : PageModel
    {
        private readonly IDishService _dishService;
        private readonly IIngredientsService _ingredientsService;
        private readonly IAllergyService _allergyService;

        public DishInfoModel(IDishService dishService,
            IIngredientsService ingredientsService,
            IAllergyService allergyService)
		{
            _dishService = dishService;
            _ingredientsService = ingredientsService;
            _allergyService = allergyService;
		}

        public FullDishDTO Dish { get; set; }
        public List<FullIngridientDTO> Ingredients { get; set; }
        public List<FullAllergyDTO> Allergies { get; set; }

        public async Task OnGetAsync(int dishId)
        {
            Dish = await _dishService.GetDishAsync(dishId);
            Ingredients = await _ingredientsService.GetDishIngridientsAsync(dishId);
            Allergies = await _allergyService.GetDishAllergiesAsync(dishId);          
        }
    }
}
