using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Concurrent;

namespace FileImporter.Common
{
    public class DirectoryScanner : IDisposable
    {
        private FileSystemWatcher watcher;
        private string landingDirectory;
        public DirectoryScanner(string landingDirrectory)
        {
            this.landingDirectory = landingDirrectory;
            watcher = new FileSystemWatcher(landingDirrectory);
            watcher.Created += this.OnCreate;
            //watcher.Error += this.OnError
            FileList = new ConcurrentQueue<string>();
        }

        public ConcurrentQueue<string> FileList { get; set; }

        private void OnCreate(object source, FileSystemEventArgs args)
        {
            FileList.Enqueue(args.FullPath);
        }

        public void Dispose()
        {
        }

        public void StartScanning()
        {
            foreach (var fileName in Directory.GetFiles(this.landingDirectory))
	        {
                File.Delete(fileName);
	        }

            watcher.EnableRaisingEvents = true;
        }

        public void StopScanning()
        {
            watcher.EnableRaisingEvents = false;
        }
    }
}
