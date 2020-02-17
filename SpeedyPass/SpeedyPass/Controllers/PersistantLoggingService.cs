using System;
using System.IO;

namespace SpeedyPass.Controllers
{
    public class PersistantLoggingService
    {
        private const string LOGS_PATH = "./logs.txt";

        public void Log(Exception ex)
        {
            try
            {
                File.AppendAllLines(PersistantLoggingService.LOGS_PATH, new string[] { ex.ToString() });
            }
            catch (Exception loggerEx)
            {
                Console.WriteLine(loggerEx);
            }
        }
    }
}