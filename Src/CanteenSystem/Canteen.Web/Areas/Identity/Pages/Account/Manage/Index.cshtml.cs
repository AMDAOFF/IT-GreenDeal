using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.UserService;
using Service.UserService.Dto;

namespace Canteen.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(
            IUserService userService)
        {
            _userService = userService;
        }

        public string Name { get; set; }
        public string Surname { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public SlimApplicationUserDTO UserDTO { get; set; } = new();

        }

        [TempData]
        public string StatusMessage { get; set; }

        //private async Task LoadAsync(SlimApplicationUserDTO user)
        //{
        //    //var userName = await _userService.GetUserAsync();

        //    Name = user.Name;
        //    Surname = user.Surname;
        //}

        public async Task<IActionResult> OnGetAsync()
        {
            SlimApplicationUserDTO user = await _userService.GetUserAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{user.Id}'.");
            }

            Input = new InputModel
            {
                UserDTO = user
            };

            //await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string result = await _userService.ChangeUserAsync(ModelState, Input.UserDTO);

			switch (result)
			{
                case "Not Valid":
                    return NotFound("Unable to load user.");
                case "Success":
                    StatusMessage = "Your profile has been updated";
                    return RedirectToPage();
				default:
                    return Page();
			}


			//var user = await _userManager.GetUserAsync(User);
			//if (user == null)
			//{
			//	return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			//}

			//if (!ModelState.IsValid)
			//{
			//	await LoadAsync(user);
			//	return Page();
			//}

			//var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
			//if (Input.PhoneNumber != phoneNumber)
			//{
			//	var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
			//	if (!setPhoneResult.Succeeded)
			//	{
			//		StatusMessage = "Unexpected error when trying to set phone number.";
			//		return RedirectToPage();
			//	}
			//}

			//await _signInManager.RefreshSignInAsync(user);
			//StatusMessage = "Your profile has been updated";
			//return RedirectToPage();
		}
    }
}
