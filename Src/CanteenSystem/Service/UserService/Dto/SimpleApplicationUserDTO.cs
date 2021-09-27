using DataAccess.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserService.Dto
{
	public class SimpleApplicationUserDTO
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
	}
}
