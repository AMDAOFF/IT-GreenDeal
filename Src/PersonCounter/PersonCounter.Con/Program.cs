using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace PersonCounter.Con
{
    class Program
    {
        private readonly object _lockObj = new object();
        static void Main(string[] args)
        {
            Program program = new Program();
            program.MainAsync();
        }

        private async void MainAsync()
        {
            List<string> list = GetData();
            foreach (string IP in list)
            {
                lock (_lockObj)
                {
                    GetPersonsFromRoom(IP);
                }
            }

            Console.Read();

            // Prøv med Rabbit MQ script i stedet
        }

        private void GetPersonsFromRoom(string cameraIP)
        {
                Console.WriteLine(Directory.GetCurrentDirectory());
                string cmd = @$"C:\Users\jimmy\source\repos\IT-GreenDeal\Src\PersonCounter\PersonCounter.Py\count_people.py -ip {cameraIP}";
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"C:\Users\jimmy\AppData\Local\Programs\Python\Python37\python.exe",
                        Arguments = cmd,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    },
                    EnableRaisingEvents = true
                };
                process.ErrorDataReceived += ProcessErrorDataReceived;
                process.OutputDataReceived += ProcessOutputDataReceived;

                process.Start();
                process.BeginErrorReadLine();
                process.BeginOutputReadLine();
                process.WaitForExit();
                process.ErrorDataReceived -= ProcessErrorDataReceived;
                process.OutputDataReceived -= ProcessOutputDataReceived;
        }

        private List<string> GetData()
        {
            return new List<string>() { "192.160.0.2", "192.160.0.6", "192.160.4.1" };
        }

        private static void ProcessErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine($"Python script failed:\n{e.Data}");
        }

        private static void ProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
            string[] pythonValues = e.Data.Split(';');
            string filename = $"{pythonValues[0]}.txt";
            string filepath = Path.Combine("Files", filename);

            if (int.TryParse(pythonValues[0], out int newPersonCount))
            {
                if (File.Exists(filepath))
                {
                    if (int.TryParse(File.ReadAllText(filepath), out int oldPersonCount))
                    {

                        if (newPersonCount != oldPersonCount)
                        {
                            File.WriteAllText(filepath, newPersonCount.ToString());

                            //TODO: Call RabbitMQ
                        }

                    }
                    else
                    {
                        //TODO: Log or Error
                    }
                }
                else
                {
                    File.WriteAllText(filepath, newPersonCount.ToString());

                    //TODO: Call RabbitMQ
                }
            }
            else
            {
                //TODO: Log or Error
            }
        }
    }
}
