using System.Diagnostics;
using System.Threading.Tasks;

namespace Absence.Service.PythonService
{
    public class PythonService : LoggingService.LoggingService, IPythonService
    {
        private const string _scriptLocation = @"C:\Users\jimmy\source\repos\IT-GreenDeal\Src\PersonCounter\PersonCounter.Py\count_people.py";
        private const string _pythonLocation = @"C:\Users\jimmy\AppData\Local\Programs\Python\Python37\python.exe;";

        public async Task<Process> StartAbsenceScript(string IP)
        {
            return await Task.Run(() =>
            {
                string scriptParameters = $" -ip {IP}";
                string command = _scriptLocation + scriptParameters;
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = _pythonLocation,
                        Arguments = command,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    },
                    EnableRaisingEvents = true
                };

                process.Start();
                return process;
            });
        }

        public async Task StopAbsenceScript(Process absenceProcess) => await Task.Run(() => { absenceProcess.Kill(); });
    }
}
