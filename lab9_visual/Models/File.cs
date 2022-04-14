using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab9_visual.Models
{
    public class File : INotifyPropertyChanged
    {
        private bool isHashed;
        public ObservableCollection<File>? FilesAndFolders { get; set; }
        public string NodeName { get; }
        public string FullPath { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public File(string _FullPath, bool isDisk)
        {
            FilesAndFolders = new ObservableCollection<File>();
            FullPath = _FullPath;
            if (!isDisk)
                NodeName = Path.GetFileName(_FullPath);
            else
                NodeName = "Диск " + _FullPath.Substring(0, _FullPath.IndexOf(":"));
            isHashed = false;
        }

        public void GetFilesAndFolders()
        {
            if (!isHashed)
            {
                try
                {
                    IEnumerable<string> subdirs = Directory.EnumerateDirectories(FullPath, "*", SearchOption.TopDirectoryOnly);
                    foreach (string dir in subdirs)
                    {
                        File thisnode = new File(dir, false);
                        FilesAndFolders.Add(thisnode);
                    }

                    string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    IEnumerable<string> files = Directory.EnumerateFiles(FullPath)
                        .Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
                        .ToList();

                    foreach (string file in files)
                    {
                        FilesAndFolders.Add(new File(file, false));
                    }
                }
                catch
                {

                }
                isHashed = true;
            }
        }
    }
}
