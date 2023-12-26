﻿namespace EroMangaManager.WinUI3.StaticResource
{
    internal class FileSizeConverter : IValueConverter
    {
        public object Convert (object value , Type targetType , object parameter , string language)
        {
            var size = (long) value;
            var kb = size >> 10;
            var mb = kb >> 10;
            if (mb > 1000)
            {
                var gb = mb >> 10;
                return gb + "GB";
            }
            else
            {
                return mb + "MB";
            }
        }

        public object ConvertBack (object value , Type targetType , object parameter , string language)
        {
            throw new NotImplementedException();
        }
    }
}