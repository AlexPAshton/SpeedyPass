using System.IO;

namespace SpeedyPass.Services
{
    public class ServiceStorageAppConfig_File : IServiceStorageAppConfig
    {
        private const string CONFIG_PATH = "./Data/config.dat";

        public bool ConfigExists()
        {
            return File.Exists(ServiceStorageAppConfig_File.CONFIG_PATH);
        }
    }
}
