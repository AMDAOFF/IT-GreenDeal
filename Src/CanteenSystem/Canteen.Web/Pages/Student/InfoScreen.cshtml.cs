using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;
using Service.DishService;
using Service.DishService.Dto;

namespace Canteen.Web.Pages.Student
{
    public class InfoScreenModel : PageModel
    {

        public string BaseUrl { get; set; }

        public List<FullDishDTO> DishesToday { get; set; }

        private IDishService _dishService { get; set; }

        public InfoScreenModel(IHttpContextAccessor httpContextAccessor, IDishService dishService)
        {
            var request = httpContextAccessor.HttpContext.Request;
            BaseUrl = $"{request.Scheme}://{request.Host}/";

            _dishService = dishService;
        }

        public async void OnGetAsync()
        {
            DishesToday = await _dishService.GetDishesOfTheDay();
        }
    }
}
