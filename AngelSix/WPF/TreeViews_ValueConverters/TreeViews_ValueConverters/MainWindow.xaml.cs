using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace TreeViews_ValueConverters
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region On Loaded
        /// <summary>
        /// Runs on loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //get every drive in your directory
            foreach (var drive in Directory.GetLogicalDrives())
            {
                //create a new item for it
                var item = new TreeViewItem()
                {
                    Header = drive,
                    Tag = drive
                };

                //Adds dummy value
                item.Items.Add(null);
                
                //Listen out for item being expanded
                item.Expanded += Folder_Expanded;

                //Add it to the main tree
                FolderView.Items.Add(item);
            }
        
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            //if the items only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            item.Items.Clear();


            //get the full path
            var fullpath = (string)item.Tag;

            var directories = new List<string>();

            //Try get directories from the folder
            //ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullpath);

                if(dirs.Length >0)
                { directories.AddRange(dirs);}
            }
            catch
            {

            }

            //For Each directory
            directories.ForEach(directoryPath =>
            {
                var subItem = new TreeViewItem()
                {
                    //header is just name of the folder
                    Header = GetFileFolderName(directoryPath),

                    //tag is going to be entire path
                    Tag = directoryPath
                };

                //Add dummy item so we can expand folder
                subItem.Items.Add(null);

                // Handle expanding out
                subItem.Expanded += Folder_Expanded;

                //Add this subitem to the view
                item.Items.Add(subItem);


            });

            var files = new List<string>();

            //Try get files from the folder
            //ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullpath);

                if (fs.Length > 0)
                { files.AddRange(fs); }
            }
            catch
            {

            }

            //For Each directory
            files.ForEach(filePath =>
            {
                var subItem = new TreeViewItem()
                {
                    //header is just name of the folder
                    Header = GetFileFolderName(filePath),

                    //tag is going to be entire path
                    Tag = filePath
                };

  


                //Add this subitem to the view
                item.Items.Add(subItem);


            });

        }
        #endregion

        /// <summary>
        /// finds the file or folder name from a pull path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            // c:\somthing\ a folder
            // c:\something a file.png < we will want to get the end file

            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // make all slashed back slashes
            var normalizedPath = path.Replace('/', '\\');

            var lastIndex = normalizedPath.LastIndexOf('\\');

            //If we dont find a backslash, return the path iteself
            if (lastIndex <= 0)
                return path;

            //return the name after the last backslash
            return path.Substring(lastIndex +1);
        }
    }
}
