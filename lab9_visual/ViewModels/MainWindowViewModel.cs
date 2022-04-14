using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using lab9_visual.Models;
using Avalonia.Media.Imaging;

namespace lab9_visual.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public class Image
        {
            public Bitmap Img { get; set; }
            public string Path { get; set; }
            public Image(string path)
            {
                try
                {
                    Img = new Bitmap(path);
                    Path = path;
                }
                catch
                {

                }
            }
        }
        public ObservableCollection<Image> DirectoryImages { get; set; }
        public ObservableCollection<lab9_visual.Models.File> Items { get; }

        List<string> allDrivesNames;

        public MainWindowViewModel()
        {
            allDrivesNames = new List<string>();
            Items = new ObservableCollection<lab9_visual.Models.File>();
            DirectoryImages = new ObservableCollection<Image>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                allDrivesNames.Add(drive.Name);
                lab9_visual.Models.File rootNode = new lab9_visual.Models.File(drive.Name, true);
                Items.Add(rootNode);
            }
        }

        public void RefreshImageList(List<string> imagesPaths, string selectedImage)
        {
            DirectoryImages.Clear();
            DirectoryImages.Add(new Image(selectedImage));
            foreach (string image in imagesPaths)
            {
                DirectoryImages.Add(new Image(image));
            }
        }
    }
}
