﻿global using System;
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.Diagnostics;
global using System.IO;
global using System.Linq;
global using System.Threading.Tasks;

global using CommonLibrary;

global using CommunityToolkit.WinUI.Controls;

global using EroMangaDatabase.Entities;
global using EroMangaDatabase.Tables;

global using EroMangaManager.Core.IOOperation;
global using EroMangaManager.Core.Models;
global using EroMangaManager.Core.Setting;
global using EroMangaManager.Core.ViewModels;
global using EroMangaManager.WinUI3.Commands;
global using EroMangaManager.WinUI3.Helpers;
global using EroMangaManager.WinUI3.Models;
global using EroMangaManager.WinUI3.OldLibrary;
global using EroMangaManager.WinUI3.Views;
global using EroMangaManager.WinUI3.Views.ContentDialogPages;
global using EroMangaManager.WinUI3.Views.FunctionChildPages;
global using EroMangaManager.WinUI3.Views.MainPageChildPages;
global using EroMangaManager.WinUI3.Views.SettingPageChildPages;

global using GroupedItemsLibrary;

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
global using Microsoft.Windows.AppNotifications;

global using SharpCompress.Archives;

global using Windows.ApplicationModel.Resources;
global using Windows.Storage;
global using Windows.Storage.FileProperties;
global using Windows.Storage.Pickers;
global using Windows.Storage.Streams;

global using WinRT.Interop;

global using static CommonLibrary.HashComputer;
global using static EroMangaDatabase.BasicController;
global using static EroMangaManager.Core.Setting.FolderEnum;
global using static EroMangaManager.WinUI3.OldLibrary.StorageFolderHelper;
