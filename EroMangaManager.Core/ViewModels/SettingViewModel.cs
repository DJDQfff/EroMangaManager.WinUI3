using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Config.Net;

using EroMangaManager.Core.Setting;

namespace EroMangaManager.Core.ViewModels
{
    /// <summary>
    /// 设置项ViewModel
    /// </summary>
    public class SettingViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 设置数据源
        /// </summary>
        public IAppConfig AppConfig { get; }
        /// <summary>
        /// 存储的exe路径
        /// </summary>
        public IEnumerable<string> ExePaths
        {
            get => AppConfig.MangaOpenWaySetting.PossibleExePaths?.Split('|')
                .SkipWhile(x => string.IsNullOrWhiteSpace(x));
        }
        /// <summary>
        /// 设置打开exe路径
        /// </summary>
        /// <param name="path"></param>
        public void SetExePath (string path)
        {
            AppConfig.MangaOpenWaySetting.UserSelectedReadMangaExePath = path;
        }
        /// <summary>
        /// 移除某exe路径
        /// </summary>
        /// <param name="path"></param>
        public void RemoveExePath (string path)
        {
            AppConfig.MangaOpenWaySetting.PossibleExePaths = AppConfig.MangaOpenWaySetting.PossibleExePaths.Replace(path , string.Empty);
            if (AppConfig.MangaOpenWaySetting.UserSelectedReadMangaExePath == path)
            {
                AppConfig.MangaOpenWaySetting.UserSelectedReadMangaExePath = string.Empty;
            }
            OnPropertyChanged();
        }
        /// <summary>
        /// 添加exe路径
        /// </summary>
        /// <param name="exePath"></param>
        public void AddExePath (string exePath)
        {
            if (!AppConfig.MangaOpenWaySetting.PossibleExePaths.Contains(exePath))
            {
                AppConfig.MangaOpenWaySetting.PossibleExePaths += $"|{exePath}";
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 属性更改
        /// </summary>
        public void OnPropertyChanged ()
        {
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(""));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iniPath"></param>
        public SettingViewModel (string iniPath)
        {
            AppConfig = new ConfigurationBuilder<IAppConfig>().UseIniFile(iniPath).Build();
        }
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}