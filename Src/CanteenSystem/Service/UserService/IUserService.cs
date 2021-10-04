using Microsoft.AspNetCore.Mvc.ModelBinding;
using Canteen.Service.UserService.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Canteen.Service.UserService
{
	public interface IUserService
	{
		Task<List<SimpleApplicationUserDTO>> GetUsersAsync();
		Task<SimpleApplicationUserDTO> GetUserAsync();
		Task<string> ChangeUserAsync(ModelStateDictionary modelState);
		Task DeleteUser(SimpleApplicationUserDTO userDTO);
	}
}
