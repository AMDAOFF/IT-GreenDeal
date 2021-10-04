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

		public UserService(IdentityContext identityContext,
			IEncryptionService encryptionService,
			IHttpContextAccessor httpContextAccessor,
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			RoleManager<IdentityRole> roleManager)
		{
			_identityContext = identityContext;
			_encryptionService = encryptionService;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

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

		//public async Task<string> GetCurrentUserRole(string currentUserId)
		//{
		//	ApplicationUser user = _identityContext.Users.OfType<ApplicationUser>().FirstOrDefault(x => x.Id == currentUserId);
		//	List<string> userRoles = (List<string>)await _userManager.GetRolesAsync(user);
		//	return userRoles.FirstOrDefault();
		//}

		public async Task<string> ChangeUserAsync(ModelStateDictionary modelState, SlimApplicationUserDTO userDTO)
		{
			var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
			if (modelState.IsValid && userDTO != null)
			{

				byte[] encryptedName = _encryptionService.Encrypt(userDTO.Name);
				byte[] encryptedSurname = _encryptionService.Encrypt(userDTO.Surname);

				user.Name = Convert.ToBase64String(encryptedName);
				user.Surname = Convert.ToBase64String(encryptedSurname);
				await _userManager.UpdateAsync(user);
				await _signInManager.RefreshSignInAsync(user);
				return "Success";
			}
			return "Not Valid";

			//var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

			//if (modelState.IsValid && user != null)
			//{
			//	await _signInManager.RefreshSignInAsync(user);
			//	return "Success";
			//}

			//return "Not Valid";

		}

		public async Task EditUser(SlimApplicationUserDTO userDTO)
		{
			//List<ApplicationUser> users = await _identityContext.Users.OfType<ApplicationUser>().ToListAsync();
			//ApplicationUser user = users.Find(x => x.Id == userDTO.Id);

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

		public async Task DeleteUser(SlimApplicationUserDTO userDTO)
		{
			List<ApplicationUser> users = await _identityContext.Users.OfType<ApplicationUser>().ToListAsync();

			ApplicationUser user = users.Find(x => x.UserName == userDTO.Email);
			await _userManager.DeleteAsync(user);
		}

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
