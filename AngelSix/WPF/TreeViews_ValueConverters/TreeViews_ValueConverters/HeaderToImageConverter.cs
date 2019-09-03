using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TreeViews_ValueConverters
{

    /// <summary>
    /// converts a full path to a specific image type of a drive folder or path
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //get full path
            var path = (string)value;

            // If the path is null , ignore
            if (path == null)
                return null;

            var name = MainWindow.GetFileFolderName(path);

            var image = "Images/file.png";

            //if the name is blank we presume its a drive as we cannot v a blank file or folder name
            if (string.IsNullOrEmpty(name))
                image = "Images/icons8-c-drive-100.png";
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
                image = "Images/icons8-folder-100.png";
            

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
