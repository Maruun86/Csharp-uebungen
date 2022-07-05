using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dateiwächter
{
    internal class Guardian
    {
        FileSystemWatcher fileSystemWatcher;
        Queue<Message> queue = new Queue<Message>();
        List<Abonnent> abonnentens = new List<Abonnent>();


        public void SetAbo(Abonnent a)
        {
            abonnentens.Add(a);
        }
        public void RemoveAbo(Abonnent a)
        {
            abonnentens.Remove(a);
        }
        public Guardian()
        {
            fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = @"C:\Users\pierr\Desktop";
            fileSystemWatcher.IncludeSubdirectories = true;
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            fileSystemWatcher.Changed += OnChanged;
            fileSystemWatcher.Deleted += OnChanged;
            fileSystemWatcher.Renamed += OnRenamed;
            fileSystemWatcher.EnableRaisingEvents = true;
        }
        void OnChanged(object sender, FileSystemEventArgs e)
        {      
            InQueue(e.Name);
        }
        void OnRenamed(object sender, RenamedEventArgs e)
        {
            InQueue(e.Name);
        }

        void InQueue(string name)
        {
            queue.Enqueue(new Message(name));
            while (queue.Count > 20)
            {
                queue.Dequeue();
            }
            if (queue.Count == 20 
                && queue.Peek().date > DateTime.Now - TimeSpan.FromMinutes(1))
                {
                    //File.AppendAllText("log.txt", queue.Peek().ToString());
                    foreach (Abonnent abo in abonnentens)
                    {
                        abo.Notify(queue.Peek().ToString());
                    }
                }
        }
    }
}
