using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.UserService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserService
{
	public interface IUserService
	{
		Task<List<SlimApplicationUserDTO>> GetUsersAsync();
		Task<SlimApplicationUserDTO> GetUserAsync();
		//Task<string> GetCurrentUserRole(string currentUserId);
		Task<string> ChangeUserAsync(ModelStateDictionary modelState, SlimApplicationUserDTO userDTO);
		Task EditUser(SlimApplicationUserDTO userDTO);
		Task DeleteUser(SlimApplicationUserDTO userDTO);
		Task<IEnumerable<string>> GetRoles();
	}
}
