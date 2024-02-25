using System.Collections.Generic;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;

using Config.Net;

using EroMangaManager.Core.Setting;

namespace EroMangaManager.Core.ViewModels
{
    /// <summary>
    /// 设置项ViewModel
    /// </summary>
    public class SettingViewModel : ObservableObject
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
            get => AppConfig.MangaOpenWay3.OpenWays?.Split('|' , '?')
                .SkipWhile(x => string.IsNullOrWhiteSpace(x));
        }

        /// <summary>
        /// 移除某exe路径
        /// </summary>
        /// <param name="path"></param>
        public void RemoveExePath (string path)
        {
            var str = $"|{path}";
            AppConfig.MangaOpenWay3.OpenWays = AppConfig.MangaOpenWay3.OpenWays.Replace(str , string.Empty);

            OnPropertyChanged(nameof(ExePaths));
        }

        /// <summary>
        /// 添加exe路径
        /// </summary>
        /// <param name="exePath"></param>
        public void AddExePath (string exePath)
        {
            if (!AppConfig.MangaOpenWay3.OpenWays.Contains(exePath))
            {
                AppConfig.MangaOpenWay3.OpenWays += $"|{exePath}";
                OnPropertyChanged(nameof(ExePaths));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="iniPath"></param>
        public SettingViewModel (string iniPath)
        {
            AppConfig = new ConfigurationBuilder<IAppConfig>().UseIniFile(iniPath).Build();
        }
    }
}