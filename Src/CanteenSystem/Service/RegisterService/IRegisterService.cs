using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RegisterService
{
	public interface IRegisterService
	{
		Task Register(byte[] name, string email, byte[] surname, string password, string returnUrl, ModelStateDictionary ModelState);
	}
}
