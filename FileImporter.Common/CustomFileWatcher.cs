using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;


namespace FileImporter.Common
{
    public delegate void WatcherElapsedInterval(object sender);

    public class CustomFileWatcher
    { 
        private Timer timer;
        private int checkInterval;
        private string checkPath;

        public event WatcherElapsedInterval ElapsedInterval;
        //we dont need concurrent list here
        public List<string> FileList { get; set; }

        public CustomFileWatcher(int interval, string checkPath)
        {
            this.FileList = new List<string>();
            this.checkInterval = interval;
            this.checkPath = checkPath;
            this.timer = new System.Timers.Timer();
            this.timer.AutoReset = false;
            this.timer.Elapsed += new ElapsedEventHandler(TimeElapsed);
        }

        private void TimeElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var files = System.IO.Directory.GetFiles(this.checkPath, "*", System.IO.SearchOption.AllDirectories);

            foreach (var file in files.Where(file => !FileList.Contains(file)))
            {
                FileList.Add(file);
            }

            if (ElapsedInterval != null)
                ElapsedInterval(this);
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        public void Start()
        {
            this.timer.Interval = this.checkInterval;
            this.timer.Start();
        }
    }
}
