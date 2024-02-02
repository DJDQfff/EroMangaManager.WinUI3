using Config.Net;

using EroMangaManager.Core.Setting;

namespace EroMangaManager.Core.ViewModels
{
    public class SettingViewModel
    {
        public IAppConfig AppConfig { get; }

        public SettingViewModel (string iniPath)
        {
            AppConfig = new ConfigurationBuilder<IAppConfig>().UseIniFile(iniPath).Build();
        }
    }
}