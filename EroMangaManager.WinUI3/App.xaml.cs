// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Microsoft.Windows.ApplicationModel.WindowsAppRuntime;

namespace EroMangaManager.WinUI3;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    public Window MainWindow;
    internal new static App Current;
    internal ObservableCollectionVM GlobalViewModel { get; private set; }

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

        MangaBookCommands.Initial();

        GlobalViewModel = new ObservableCollectionVM();

        DatabaseConfig.ConnectingString = $"Data Source={LocalFolder}\\localdatabase.db";
        DatabaseController.Migrate();

        AppConfigPath = Path.Combine(LocalFolder , "AppConfig.ini");
        AppConfig = new SettingViewModel(AppConfigPath);

        var language = App.Current.AppConfig.AppConfig.General.DefaultAppUILanguage;
        Windows.ApplicationModel.Resources.Core.ResourceContext.SetGlobalQualifierValue("Language" , language);

        CoverHelper.InitialDefaultCover();

        EnsureChildTemporaryFolders(Covers.ToString() , Filters.ToString());

        #region 事件赋值

        GlobalViewModel.ErrorZipEvent += (string str) =>
        {
            new ToastContentBuilder()
                .AddText($"{str}\r{ResourceLoader.GetForViewIndependentUse("Strings").GetString("ErrorString1")}")
                .Show();
        };
        GlobalViewModel.WorkDoneEvent += Toast;
        GlobalViewModel.WorkFailedEvent += Toast;

        #endregion 事件赋值

        InitializeGlobalViewModel();

        DeploymentResult result = DeploymentManager.GetStatus();
        if (result.Status is not DeploymentStatus.Ok)
        {
            var initializeTask = Task.Run(() => DeploymentManager.Initialize());
            initializeTask.Wait();
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
            var activatedEventArgs =
                Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().GetActivatedEventArgs();
            await mainInstance.RedirectActivationToAsync(activatedEventArgs);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            return;
        }
        MainWindow = new MainWindow();
        MainWindow.Activate();

        #region 需要后台执行

        await GlobalViewModel.InitialEachFolders();

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

        GlobalViewModel.GetAllFolders(folders);
    }

    private void Toast (string message)
    {
        new ToastContentBuilder().AddText(message).Show();
    }
}