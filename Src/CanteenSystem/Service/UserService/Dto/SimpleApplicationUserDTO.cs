using System.Collections.Generic;

namespace Canteen.Service.UserService.Dto
{
	public class SimpleApplicationUserDTO
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public IList<string> Roles { get; set; }
	}
}
