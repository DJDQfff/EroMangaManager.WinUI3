// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.Threading;

using Microsoft.Windows.ApplicationModel.WindowsAppRuntime;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

namespace EroMangaManager.WinUI3;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    public Window MainWindow;
    internal static new App Current;
    internal ObservableCollectionVM GlobalViewModel { get; private set; }

    internal BackgroundCoverSetter BackgroundCoverSetter { get; private set; } = new();
    internal MangaInitialStack initialStack { get; private set; } = new();
    internal SettingViewModel AppConfig { get; private set; }
    internal string AppConfigPath { get; private set; }
    internal string LocalFolder = ApplicationData.Current.LocalFolder.Path;

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App ()
    {
        InitializeComponent();

        Current = this;
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override async void OnLaunched (LaunchActivatedEventArgs args)
    {
        #region 快速执行

#if DEBUG
        await Windows.System.Launcher.LaunchFolderPathAsync(LocalFolder);
#endif

        MangaCommands.Initial();
        UICommands.Initial();

        GlobalViewModel = new ObservableCollectionVM();

        DatabaseConfig.ConnectingString = $"Data Source={LocalFolder}\\localdatabase.db";
        DatabaseController.Migrate();

        AppConfigPath = Path.Combine(LocalFolder , "AppConfig.ini");
        AppConfig = new SettingViewModel(AppConfigPath);

        var language = App.Current.AppConfig.AppConfig.General.LanguageIndex switch
        {
            1 => "en",
            _ => "zhCN"
        };
        Windows.ApplicationModel.Resources.Core.ResourceContext.SetGlobalQualifierValue(
            "Language" ,
            language
        );

        CoverHelper.InitialDefaultCover();

        EnsureChildTemporaryFolders(Covers.ToString() , Filters.ToString());

        #region 事件赋值

        GlobalViewModel.ErrorZipEvent += str =>
        {
            var appNotification = new AppNotificationBuilder()
                .AddText(
                    $"{str}\r{ResourceLoader.GetForViewIndependentUse().GetString("ErrorString1")}"
                )
                .BuildNotification();
            AppNotificationManager.Default.Show(appNotification);
        };
        GlobalViewModel.WorkDoneEvent += Toast;
        GlobalViewModel.WorkFailedEvent += Toast;

        #endregion 事件赋值

        InitializeGlobalViewModel();

        DeploymentResult result = DeploymentManager.GetStatus();
        if (result.Status is not DeploymentStatus.Ok)
        {
            await Task.Run(() => DeploymentManager.Initialize());
        }

        #endregion 快速执行

        // If this is the first instance launched, then register it as the "main" instance.
        // If this isn't the first instance launched, then "main" will already be registered,
        // so retrieve it.
        var mainInstance = Microsoft.Windows.AppLifecycle.AppInstance.FindOrRegisterForKey("main");

        // If the instance that's executing the OnLaunched handler right now
        // isn't the "main" instance.
        if (!mainInstance.IsCurrent)
        {
            // Redirect the activation (and args) to the "main" instance, and exit.
            var activatedEventArgs = Microsoft.Windows.AppLifecycle.AppInstance
                .GetCurrent()
                .GetActivatedEventArgs();
            await mainInstance.RedirectActivationToAsync(activatedEventArgs);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            return;
        }
        MainWindow = new MainWindow();
        MainWindow.Activate();

        #region 需要后台执行

        await GlobalViewModel.StartInitial();
        //await Current.initialStack.StartAsync();
        await Current.BackgroundCoverSetter.LoopWork3();

        //GlobalViewModel.InitialEachFoldersInOrder();

        #endregion 需要后台执行
    }

    /// <summary>
    /// 初始化文件夹目录
    /// </summary>
    private void InitializeGlobalViewModel ()
    {
        var folders = DatabaseController.MangaFolder_GetAllPaths().ToList();
        var defaultpath = AppConfig.AppConfig.General.DefaultBookcaseFolder;

        var f = folders.SingleOrDefault(x => x == defaultpath);
        if (f != null)
        {
            folders.Remove(f);
            folders.Insert(0 , f);
        }
#if DEBUG_TESTFOLDER
        folders = new() { @"D:\test" };
#endif
        GlobalViewModel.GetAllFolders(folders);
        GlobalViewModel.InitialGroup += MangaFactory.InitialGroup2;
    }

    private void Toast (string message)
    {
        var appNotification = new AppNotificationBuilder().AddText(message).BuildNotification();
        AppNotificationManager.Default.Show(appNotification);
    }
}
