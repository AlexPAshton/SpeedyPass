using System;
using System.IO;

namespace SpeedyPass.Controllers
{
    public class FileChangeWatcherService
    {
        public delegate void FileChangeArgs();
        public event FileChangeArgs OnFileChanged;

        private FileSystemWatcher fileSystemWatcher;

        public void Watch(string filePath)
        {
            this.fileSystemWatcher = new FileSystemWatcher();
            this.fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
            this.fileSystemWatcher.Changed += this.FileSystemWatcher_Changed;
            this.fileSystemWatcher.Path = Path.GetDirectoryName(filePath);
            this.fileSystemWatcher.Filter = Path.GetFileName(filePath);
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            this.OnFileChanged?.Invoke();
        }
    }
}