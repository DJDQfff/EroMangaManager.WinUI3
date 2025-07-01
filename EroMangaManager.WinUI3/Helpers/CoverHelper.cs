namespace EroMangaManager.WinUI3.Helpers;

/// <summary>
/// 封面帮助类
/// </summary>
public static class CoverHelper
{
    private static SvgImageSource _imageSource;

    /// <summary>
    /// 初始化默认漫画封面
    /// </summary>
    public static void InitialDefaultCover()
    {
        _imageSource = new SvgImageSource(new Uri(DefaultCoverPath));
    }

    /// <summary>
    /// 默认书籍封面路径
    /// </summary>
    public static string DefaultCoverPath => "ms-appx:///Assets/SVGs/book.svg";

    public static string ErrorSVGPath => "ms-appx:///Assets/SVGs/error.svg";

    /// <summary>
    /// 获取默认封面
    /// </summary>
    public static SvgImageSource DefaultCover => _imageSource;

    /// <summary> 调用系统API，返回缩率图 </summary>
    /// <param name="cover"> </param>
    /// <returns> </returns>
    public static async Task<BitmapImage> GetCoverThumbnail_SystemAsync(this StorageFile cover)
    {
        BitmapImage bitmapImage = new();
        //thumbnailMode.picturemode有坑,缩略图不完整
        IRandomAccessStream randomAccessStream;

        using (
            StorageItemThumbnail thumbnail = await cover.GetThumbnailAsync(
                ThumbnailMode.SingleItem,
                80
            )
        )
        {
            randomAccessStream = thumbnail.CloneStream();
        }
        await bitmapImage.SetSourceAsync(randomAccessStream);

        return bitmapImage;
    }

    /// <summary>
    /// 清除所有封面文件
    /// </summary>
    /// <returns></returns>
    public static async Task ClearCovers()
    {
        StorageFolder storageFolder = await GetChildTemporaryFolder(nameof(Covers));
        var coverfolder = storageFolder.Path;
        Directory.Delete(coverfolder, true);
        EnsureChildTemporaryFolders(Covers.ToString());
    }

    public static string LoadCoverFromInternalFolder(string folderPath)
    {
        var directoryinfo = new DirectoryInfo(folderPath);
        if (!directoryinfo.Exists)
            throw new ArgumentException();
        var file = directoryinfo
            .GetFiles()
            .FirstOrDefault(x => SupportedType.ImageType.Contains(x.Extension));
        return file?.FullName;
    }

    /// <summary> 尝试创建封面文件。 </summary>
    /// <returns> </returns>
    public static async Task<string> TryCreatCoverFileAsync(
        string storageFile,
        FilteredImage[] filteredImages
    )
    {
        string path;
        StorageFolder folder = await GetChildTemporaryFolder(nameof(Covers));
        var coverfile = Path.Combine(Path.GetFileNameWithoutExtension(storageFile), ".jpg");

        IStorageItem storageItem = await folder.TryGetItemAsync(coverfile);
        if (storageItem is null)
        {
            path = await CreatCoverFile_Origin_SharpCompress(storageFile, filteredImages);
        }
        else
        {
            path = storageItem.Path;
        }

        return path;
    }

    /// <summary>
    /// 使用SharpCompress类库创建源图片设为封面
    /// </summary>
    /// <param name="storageFile"></param>
    /// <param name="filteredImages">要比较的数据</param>
    /// <returns></returns>
    public static async Task<string> CreatCoverFile_Origin_SharpCompress(
        this string storageFile,
        FilteredImage[] filteredImages
    )
    {
        string path = null;
        StorageFolder coverfolder = await GetChildTemporaryFolder(nameof(Covers));
        Stream stream = new FileStream(storageFile, FileMode.Open);
        try
        {
            using (var zipArchive = ArchiveFactory.Open(stream))
            {
                foreach (var entry in zipArchive.Entries)
                {
                    bool canuse = entry.EntryFilter(filteredImages);
                    if (canuse)
                    {
                        path = Path.Combine(
                            coverfolder.Path,
                            Path.GetFileNameWithoutExtension(storageFile) + ".jpg"
                        );
                        entry.WriteToFile(path);
                        break;
                    }
                }
            }
            return path;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
