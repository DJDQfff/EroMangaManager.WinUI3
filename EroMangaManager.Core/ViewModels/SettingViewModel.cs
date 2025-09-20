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
    public class SettingViewModel : ObservableObject
    {
        /// <summary>
        /// 设置数据源
        /// </summary>
        public IAppConfig AppConfig { get; } 

        public SettingViewModel(string iniPath) {
            AppConfig=     new ConfigurationBuilder<IAppConfig>().UseIniFile(iniPath).Build();
            var exes = AppConfig.MangaOpenWay3.OpenWays
                    ?.Split('|', '?')

                    .SkipWhile(string.IsNullOrWhiteSpace)
                .Skip(2);
            ExePaths = new(exes);

 }


        /// <summary>
        /// 存储的exe路径
        /// </summary>
        public ObservableCollection<string> ExePaths { get; }

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
          _=  ExePaths.Remove(path);
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
                ExePaths.Add(exePath);
            }
        }
    }
}