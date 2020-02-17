using System.IO;
using System.Threading;

namespace SpeedyPass.Services
{
    //----------------------------------------------------
    // FileChangeWatcherService
    //----------------------------------------------------
    // This class supports the watching of a single file.
    // Changes to the files creation time or write time
    // trigger the call -back event to be invokes.
    // *The call-back happens on a non STA thread*
    public class FileChangeWatcherService
    {
        // A delay to ensure syncing utilities, such as OneDrive, 
        // have released the file once finished.
        private const int PROCESS_RELEASE_DELAY = 1000;

        // Public event that can be subscribed 
        // to for when a file change is detected.
        public delegate void FileChangeArgs();
        public event FileChangeArgs OnFileChanged;

        // Private variables
        private FileSystemWatcher fileSystemWatcher;

        // Method initialises the file watcher if 
        // passed param is valid and 
        // configures it to watch a specific file.
        public void Watch(string filePath)
        {
            try
            {
                string fileName = Path.GetFileName(filePath);
                string directoryPath = Path.GetDirectoryName(filePath);

                if (fileName != null && fileName != string.Empty &&
                    directoryPath != null && directoryPath != string.Empty)
                {
                    this.fileSystemWatcher = new FileSystemWatcher();
                    this.fileSystemWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
                    this.fileSystemWatcher.Changed += this.FileSystemWatcher_Changed;
                    this.fileSystemWatcher.Path = Path.GetDirectoryName(filePath);
                    this.fileSystemWatcher.Filter = Path.GetFileName(filePath);
                    this.fileSystemWatcher.EnableRaisingEvents = true;
                }
            }
            catch
            {
                throw;
            }
        }

        // File System Watcher call-back event
        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(FileChangeWatcherService.PROCESS_RELEASE_DELAY);

            this.OnFileChanged?.Invoke();
        }
    }
}