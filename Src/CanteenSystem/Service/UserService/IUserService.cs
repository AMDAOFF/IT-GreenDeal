using Microsoft.AspNetCore.Mvc.ModelBinding;
using Canteen.Service.UserService.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Canteen.Service.AllergyService.Dto;

namespace Canteen.Service.UserService
{
	public interface IUserService
	{
		Task<List<SlimApplicationUserDTO>> GetUsersAsync();
		Task<SlimApplicationUserDTO> GetUserAsync();
		//Task<string> GetCurrentUserRole(string currentUserId);
		Task<string> ChangeUserAsync(ModelStateDictionary modelState, SlimApplicationUserDTO userDTO, List<FullAllergyDTO> allergiesDTO);
		Task EditUser(SlimApplicationUserDTO userDTO);
		Task DeleteUser(SlimApplicationUserDTO userDTO);
		Task<IEnumerable<string>> GetRoles();
	}
}
