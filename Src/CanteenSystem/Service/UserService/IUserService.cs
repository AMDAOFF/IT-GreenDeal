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
		Task<List<SimpleApplicationUserDTO>> GetUsersAsync();
		Task<SimpleApplicationUserDTO> GetUserAsync();
		Task<string> ChangeUserAsync(ModelStateDictionary modelState);
		Task DeleteUser(SimpleApplicationUserDTO userDTO);
	}
}
