using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Canteen.Service.LoginService
{
	public interface ILoginService
	{
		Task<SignInResult> Login(string Email, string Password, bool RememberMe);
		Task Logout();
	}
}
