global using System;
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.Diagnostics;
global using System.IO;
global using System.Linq;
global using System.Threading.Tasks;

global using CommunityToolkit.WinUI.Notifications;
global using CommunityToolkit.WinUI.UI.Controls;

global using EroMangaDatabase.Entities;
global using EroMangaDatabase.Tables;

global using EroMangaManager.Core.Helpers;
global using EroMangaManager.Core.MangaParser;
global using EroMangaManager.Core.Models;
global using EroMangaManager.Core.ViewModels;
global using EroMangaManager.WinUI3.Commands;
global using EroMangaManager.WinUI3.Helpers;
global using EroMangaManager.WinUI3.Models;
global using EroMangaManager.WinUI3.Views;
global using EroMangaManager.WinUI3.Views.ContentDialogPages;
global using EroMangaManager.WinUI3.Views.FunctionChildPages;
global using EroMangaManager.WinUI3.Views.MainPageChildPages;
global using EroMangaManager.WinUI3.Views.SettingPageChildPages;

global using GroupedItemsLibrary.Models;
global using GroupedItemsLibrary.ViewModels;

global using Microsoft.UI.Windowing;
global using Microsoft.UI.Xaml;
global using Microsoft.UI.Xaml.Controls;
global using Microsoft.UI.Xaml.Controls.Primitives;
global using Microsoft.UI.Xaml.Data;
global using Microsoft.UI.Xaml.Input;
global using Microsoft.UI.Xaml.Markup;
global using Microsoft.UI.Xaml.Media;
global using Microsoft.UI.Xaml.Media.Imaging;
global using Microsoft.UI.Xaml.Navigation;

global using MyLibrary.WinUI3;

global using SharpCompress.Archives;

global using Windows.ApplicationModel.Resources;
global using Windows.Storage;
global using Windows.Storage.FileProperties;
global using Windows.Storage.Pickers;
global using Windows.Storage.Streams;

global using WinRT.Interop;

global using static EroMangaDatabase.BasicController;
global using static EroMangaManager.Core.SettingEnums.FolderEnum;
global using static MyLibrary.Standard20.HashComputer;
global using static MyLibrary.WinUI3.StorageFolderHelper;
