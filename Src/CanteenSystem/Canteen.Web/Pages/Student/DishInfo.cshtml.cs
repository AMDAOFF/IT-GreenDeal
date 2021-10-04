using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Canteen.Web.Pages.Student
{
    public class DishInfoModel : PageModel
    {
        public int? DishId { get; set; }

        public void OnGet(int? dishId)
        {
            DishId = dishId;
        }
    }
}
