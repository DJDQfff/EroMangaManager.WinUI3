using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Config.Net;

using EroMangaManager.Core.Setting;

namespace EroMangaManager.Core.ViewModels
{
    public class SettingViewModel : INotifyPropertyChanged
    {
        public IAppConfig AppConfig { get; }

        public IEnumerable<string> ExePaths
        {
            get => AppConfig.MangaOpenWaySetting.PossibleExePaths?.Split('|')
                .SkipWhile(x => string.IsNullOrWhiteSpace(x));
        }

        public void SetExePath (string path)
        {
            AppConfig.MangaOpenWaySetting.UserSelectedReadMangaExePath = path;
        }

        public void RemoveExePath (string path)
        {
            AppConfig.MangaOpenWaySetting.PossibleExePaths = AppConfig.MangaOpenWaySetting.PossibleExePaths.Replace(path , string.Empty);
            if (AppConfig.MangaOpenWaySetting.UserSelectedReadMangaExePath == path)
            {
                AppConfig.MangaOpenWaySetting.UserSelectedReadMangaExePath = string.Empty;
            }
            OnPropertyChanged();
        }

        public void AddExePath (string exePath)
        {
            if (!AppConfig.MangaOpenWaySetting.PossibleExePaths.Contains(exePath))
            {
                AppConfig.MangaOpenWaySetting.PossibleExePaths += $"|{exePath}";
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged ()
        {
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(""));
        }

        public SettingViewModel (string iniPath)
        {
            AppConfig = new ConfigurationBuilder<IAppConfig>().UseIniFile(iniPath).Build();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}