using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Absence.Service.PythonService
{
    public interface IPythonService
    {
        /// <summary>
        /// Starts the absence python script on the camera with the with the matching <paramref name="IP"/>
        /// </summary>
        Task<Process> StartAbsenceScript(string IP);

        /// <summary>
        /// Stops the absence python script with the matching <paramref name="absenceProcess"/>
        /// </summary>
        Task StopAbsenceScript(Process absenceProcess);
    }
}
