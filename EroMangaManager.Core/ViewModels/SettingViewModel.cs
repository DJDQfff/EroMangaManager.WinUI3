using Config.Net;

using EroMangaManager.Core.Setting;

namespace EroMangaManager.Core.ViewModels
{
    /// <summary>
    /// 设置项ViewModel
    /// </summary>
    /// <remarks>
    ///
    /// </remarks>
    /// <param name="iniPath"></param>
    public class SettingViewModel(string iniPath) : ObservableObject
    {
        /// <summary>
        /// 设置数据源
        /// </summary>
        public IAppConfig AppConfig { get; } =
            new ConfigurationBuilder<IAppConfig>().UseIniFile(iniPath).Build();

        /// <summary>
        /// 存储的exe路径
        /// </summary>
        public IEnumerable<string> ExePaths
        {
            get =>
                AppConfig.MangaOpenWay3.OpenWays
                    ?.Split('|', '?')
                    .SkipWhile(string.IsNullOrWhiteSpace);
        }

        /// <summary>
        /// 移除某exe路径
        /// </summary>
        /// <param name="path"></param>
        public void RemoveExePath(string path)
        {
            var str = $"|{path}";
            AppConfig.MangaOpenWay3.OpenWays = AppConfig.MangaOpenWay3.OpenWays.Replace(
                str,
                string.Empty
            );

            OnPropertyChanged(nameof(ExePaths));
        }

        /// <summary>
        /// 添加exe路径
        /// </summary>
        /// <param name="exePath"></param>
        public void AddExePath(string exePath)
        {
            if (!AppConfig.MangaOpenWay3.OpenWays.Contains(exePath))
            {
                AppConfig.MangaOpenWay3.OpenWays += $"|{exePath}";
                OnPropertyChanged(nameof(ExePaths));
            }
        }
    }
}