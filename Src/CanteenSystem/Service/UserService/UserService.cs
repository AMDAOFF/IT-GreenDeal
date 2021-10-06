using Canteen.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Canteen.Service.UserService.Dto;
using Canteen.Service.EncryptionService;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Canteen.Service.AllergyService.Dto;
using Canteen.DataAccess.Models;
using Canteen.Service.UserAllergyService;

namespace Canteen.Service.UserService
{
	public class UserService : IUserService
	{
		private readonly IdentityContext _identityContext;
		private readonly IEncryptionService _encryptionService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IUserAllergyService _userAllergyService;

		public UserService(IdentityContext identityContext,
			IEncryptionService encryptionService,
			IHttpContextAccessor httpContextAccessor,
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			RoleManager<IdentityRole> roleManager,
			IUserAllergyService userAllergyService)
		{
			_identityContext = identityContext;
			_encryptionService = encryptionService;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_userAllergyService = userAllergyService;
		}

		/// <summary>
		/// Gets all the users, which is stored in the database.
		/// </summary>
		/// <returns>A list of users.</returns>
		public async Task<List<SlimApplicationUserDTO>> GetUsersAsync()
		{
			List<SlimApplicationUserDTO> applicationUsers = new();
			List<ApplicationUser> users = await _identityContext.Users.OfType<ApplicationUser>().ToListAsync();

			foreach (var user in users)
			{
				List<string> userRoles = new();
				try
				{
					userRoles = (List<string>)await _userManager.GetRolesAsync(user);
				}
				catch (Exception)
				{
					userRoles.Add("User");
				}

				string decryptedName = _encryptionService.Decrypt(Convert.FromBase64String(user.Name));
				string decryptedSurname = _encryptionService.Decrypt(Convert.FromBase64String(user.Surname));

				applicationUsers.Add(new SlimApplicationUserDTO()
				{
					Id = user.Id,
					Name = decryptedName.Trim(),
					Surname = decryptedSurname.Trim(),
					Email = user.Email,
					Role = userRoles.FirstOrDefault()
				});
			}

			return applicationUsers;
		}

		/// <summary>
		/// Get the current logged in user.
		/// </summary>
		/// <returns></returns>
		public async Task<SlimApplicationUserDTO> GetUserAsync()
		{
			var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

			if (currentUser != null)
			{
				string decryptedName = _encryptionService.Decrypt(Convert.FromBase64String(currentUser.Name));
				string decryptedSurname = _encryptionService.Decrypt(Convert.FromBase64String(currentUser.Surname));

				SlimApplicationUserDTO simpleUser = new()
				{
					Id = currentUser.Id,
					Name = decryptedName,
					Surname = decryptedSurname,
					Email = currentUser.Email
				};

				return simpleUser;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// This method edit the current logged in user.
		/// </summary>
		/// <param name="modelState"></param>
		/// <param name="userDTO"></param>
		/// <param name="allergiesDTO"></param>
		/// <returns>A result string.</returns>
		public async Task<string> ChangeUserAsync(ModelStateDictionary modelState, SlimApplicationUserDTO userDTO, List<FullAllergyDTO> allergiesDTO)
		{
			// Get the current logged in user.
			var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

			// If what the user typed is valid return success, else return not valid.
			// This helps the user to know if what they submites is valid or not.
			if (modelState.IsValid && userDTO != null)
			{
				// Removes all the relations between the current logged in user and allergy.
				await _userAllergyService.RemoveUserAllergy(userDTO.Id);

				// Creates a realtion for each allery that has been submitet
				foreach (var allergy in allergiesDTO)
				{
					UserAllergy userAllergy = new UserAllergy
					{
						AllergyId = allergy.AllergyId,
						UserId = userDTO.Id
					};

					// Adds the User - Allergy relations to the context
					await _identityContext.UserAllergies.AddAsync(userAllergy);

					// Saves and submits the changes to the database
					await _identityContext.SaveChangesAsync();
				}

				// Encrypts the submittet name
				byte[] encryptedName = _encryptionService.Encrypt(userDTO.Name);

				// Encrypts the submittet surname
				byte[] encryptedSurname = _encryptionService.Encrypt(userDTO.Surname);

				// Converts the encryptet name and surname to a string
				user.Name = Convert.ToBase64String(encryptedName);
				user.Surname = Convert.ToBase64String(encryptedSurname);

				// Updates the user
				await _userManager.UpdateAsync(user);

				// Logges the user out and in again after the submit succeds
				await _signInManager.RefreshSignInAsync(user);
				return "Success";
			}
			return "Not Valid";

		}

		/// <summary>
		/// Edits the uses. This is used for the administration of users.
		/// </summary>
		/// <param name="userDTO"></param>
		/// <returns></returns>
		public async Task EditUser(SlimApplicationUserDTO userDTO)
		{
			byte[] encryptedName = _encryptionService.Encrypt(userDTO.Name);
			byte[] encryptedSurname = _encryptionService.Encrypt(userDTO.Surname);

			ApplicationUser user = await _userManager.FindByIdAsync(userDTO.Id);

			user.Name = Convert.ToBase64String(encryptedName);
			user.Surname = Convert.ToBase64String(encryptedSurname);
			user.Email = userDTO.Email;

			if (user != null)
			{
				foreach (var role in await GetRoles())
				{
					await _userManager.RemoveFromRoleAsync(user, role);
				}
				await _userManager.AddToRoleAsync(user, userDTO.Role);
			}

			await _userManager.UpdateAsync(user);
		}

		/// <summary>
		/// Delets a user. This is used by the admin of the site.
		/// </summary>
		/// <param name="userDTO"></param>
		/// <returns></returns>
		public async Task DeleteUser(SlimApplicationUserDTO userDTO)
		{
			List<ApplicationUser> users = await _identityContext.Users.OfType<ApplicationUser>().ToListAsync();

			ApplicationUser user = users.Find(x => x.UserName == userDTO.Email);
			await _userManager.DeleteAsync(user);
		}

		/// <summary>
		/// This method gets all the roles that are available.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<string>> GetRoles()
		{
			List<string> roles = new();
			var identityRoles = await _identityContext.Roles.ToListAsync();
			foreach (var role in identityRoles)
			{
				roles.Add(role.Name);
			}
			return roles.ToList();
		}
	}
}
