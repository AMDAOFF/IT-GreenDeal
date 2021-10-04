using System.Diagnostics;
using System.Threading.Tasks;

namespace Absence.Service.PythonService
{
    public class PythonService : LoggingService.LoggingService, IPythonService
    {
        private const string _scriptLocation = @"C:\Users\jimm1576\source\repos\IT-GreenDeal\Src\AbsenceSystem\Absence.Py\face_recognizer.py";
        private const string _pythonLocation = @"C:\Users\jimm1576\AppData\Local\Programs\Python\Python37\python.exe";

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
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = false
                    },
                    EnableRaisingEvents = true
                };
                process.ErrorDataReceived += ProcessErrorDataReceived;
                process.OutputDataReceived += ProcessOutputDataReceived;

                process.Start();
                process.BeginErrorReadLine();
                process.BeginOutputReadLine();
                return process;
            });
        }

        private void ProcessErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            LogWarning($"Python Script Failed:\n{e.Data}");
        }

        private void ProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            LogInformation($"Python Script Said:\n{e.Data}");
        }

        public async Task StopAbsenceScript(Process absenceProcess)
        {
            await Task.Run(() =>
            {
                absenceProcess.ErrorDataReceived -= ProcessErrorDataReceived;
                absenceProcess.OutputDataReceived -= ProcessOutputDataReceived;
                absenceProcess.Kill();
            });
        }
    }
}
