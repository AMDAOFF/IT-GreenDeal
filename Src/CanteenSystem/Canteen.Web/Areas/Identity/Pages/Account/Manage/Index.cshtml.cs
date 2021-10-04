using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Canteen.Service.UserService;
using Canteen.Service.UserService.Dto;
using System.Collections.Generic;
using Canteen.Service.AllergyService.Dto;
using Canteen.Service.AllergyService;
using Microsoft.AspNetCore.Components;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Canteen.Service.UserAllergyService;
using Canteen.Service.UserAllergyService.Dto;
using System.Linq;

namespace Canteen.Web.Areas.Identity.Pages.Account.Manage
{
	public partial class IndexModel : PageModel
	{
		private readonly IUserService _userService;
		private readonly IAllergyService _allergyService;
		private readonly IUserAllergyService _userAllergyService;

		public IndexModel(
			IUserService userService,
			IAllergyService allergyService,
			IUserAllergyService userAllergyService)
		{
			_userService = userService;
			_allergyService = allergyService;
			_userAllergyService = userAllergyService;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public List<FullAllergyDTO> AllergiesDTO { get; set; } = new();

		public class InputModel
		{
			public SlimApplicationUserDTO UserDTO { get; set; } = new();
			public List<SelectListItem> Allergies { get; set; } = new();
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
			List<FullAllergyDTO> allergies = await _allergyService.GetAllergiesAsync();
			List<FullUserAllergyDTO> userAllergies = await _userAllergyService.GetAllUserAllergy();
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{user.Id}'.");
			}

			Input = new();

			foreach (var allergy in allergies)
			{
				bool selected = false;
				foreach (var userAllergy in userAllergies)
				{
					if (userAllergy.AllergyId == allergy.AllergyId && userAllergy.UserId == user.Id)
					{
						selected = true;
					}
				}
				Input.Allergies.Add(new SelectListItem()
				{
					Value = allergy.AllergyId.ToString(),
					Text = allergy.AllergyName,
					Selected = selected
				});
			}

			Input.UserDTO = user;

			//await LoadAsync(user);
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			foreach (var item in this.Input.Allergies)
			{
				if (item.Selected)
				{
					AllergiesDTO.Add(new FullAllergyDTO()
					{
						AllergyId = Convert.ToInt32(item.Value),
						AllergyName = item.Text
					});
				}
			}

			string result = await _userService.ChangeUserAsync(ModelState, Input.UserDTO, AllergiesDTO);

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
		}
	}
}
