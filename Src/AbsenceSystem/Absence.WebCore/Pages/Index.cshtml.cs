using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Absence.Service.StudentClassService;
using Absence.Service.SubjectService;
using Absence.Service.ScheduleService;
using Absence.Service.ClassroomService;
using Absence.Service.StudentService;
using Absence.Service.AbsenceReportService;
using System.Threading;
namespace Absence.WebCore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IClassroomService _classroomService;

        public IndexModel(ILogger<IndexModel> logger, IClassroomService classroomService)
        {
            _logger = logger;
            _classroomService = classroomService;
        }
        public List<FullClassroomDTO> Classrooms { get; set; }
        public async Task OnGet()
        {
            var classroom = await _classroomService.GetById(1);
            Classrooms = await _classroomService.GetAll();
        }
    }
}
