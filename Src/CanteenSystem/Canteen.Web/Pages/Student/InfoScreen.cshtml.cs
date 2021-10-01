using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;

namespace Canteen.Web.Pages.Student
{
    public class InfoScreenModel : PageModel
    {
        public QRCodeGenerator qrGenerator { get; set; }

        public void OnGet()
        {

        }
    }
}
