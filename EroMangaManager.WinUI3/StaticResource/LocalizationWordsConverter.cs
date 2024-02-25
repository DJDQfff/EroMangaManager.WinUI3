namespace EroMangaManager.WinUI3.StaticResource
{
    internal class LocalizationWordsConverter : IValueConverter

    {
        public object Convert (object value , Type targetType , object parameter , string language)
        {
            return value switch
            {
                "InternalReadPage" or "OSRelated" => ResourceLoader.GetForViewIndependentUse("UI").GetString(value as string),
                string => value as string,
                _ => null,
            };
        }

        public object ConvertBack (object value , Type targetType , object parameter , string language)
        {
            throw new NotImplementedException();
        }
    }
}