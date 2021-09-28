using System;
using System.Diagnostics;
using System.IO;

namespace PersonCounter.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            string cmd = @"C:\Users\jimm1576\source\repos\IT-GreenDeal\Src\PersonCounter\PersonCounter.Py\count_people.py";
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Users\jimm1576\AppData\Local\Programs\Python\Python37\python.exe",
                    Arguments = cmd,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };
            process.ErrorDataReceived += Process_OutputDataReceived;
            process.OutputDataReceived += Process_OutputDataReceived;

            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.WaitForExit();
            Console.Read();
        }

        static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
            string filename = $"{e.Data.Split(';')[1]}.txt";
            string filepath = Path.Combine("Files", filename);
            if (File.Exists(filepath))
            {
                File.ReadAllText()
            }
        }
    }
}
