using Canteen.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace Canteen.Service.RegisterService
{
	public class RegisterService : IRegisterService
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public RegisterService(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		/// <summary>
		/// Registers a user, which will be stored in a database.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="email"></param>
		/// <param name="surname"></param>
		/// <param name="password"></param>
		/// <param name="returnUrl"></param>
		/// <param name="ModelState"></param>
		/// <returns></returns>
		public async Task Register(byte[] name, string email, byte[] surname, string password, string returnUrl, ModelStateDictionary ModelState)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser { UserName = email, Email = email, Name = Convert.ToBase64String(name), Surname = Convert.ToBase64String(surname), EmailConfirmed = true };
				var result = await _userManager.CreateAsync(user, password);

				// If the user can be created
				if (result.Succeeded)
				{

					// Creates the roles if they do not exists in the database
					await CreateRoleIfItDoesNotExist("Admin");
					await CreateRoleIfItDoesNotExist("Canteen");
					await CreateRoleIfItDoesNotExist("User");

					// PLEASE DELETE, THIS IS ONLY FOR TESTING
					if (email == "kenn8174@elevcampus.dk")
					{
						await _userManager.AddToRoleAsync(user, "Admin");
					}
					else
					{
						await _userManager.AddToRoleAsync(user, "User");
					}
					// ---------------------------------------

					await _signInManager.SignInAsync(user, isPersistent: false);
					return;
				}

				// If there is any errors
				foreach (var error in result.Errors)
				{
					// Display the errors for the user
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
		}

		/// <summary>
		/// Method for creating roles if the do not exist in the database
		/// </summary>
		/// <param name="roleName"></param>
		/// <returns></returns>
		public async Task CreateRoleIfItDoesNotExist(string roleName)
		{
			if (!await _roleManager.RoleExistsAsync(roleName))      // If role doesnt exist, it creates the role
			{
				await _roleManager.CreateAsync(new IdentityRole(roleName));
			}
		}

	}
}
