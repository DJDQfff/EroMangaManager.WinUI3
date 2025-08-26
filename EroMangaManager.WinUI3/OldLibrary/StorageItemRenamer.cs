namespace EroMangaManager.WinUI3.OldLibrary;

/// <summary> StorageItem重命名 </summary>
public static class StorageItemRenamer
{
    /// <summary>
    /// 重命名文件，如果已存在，则在DisplayName名后补_copy
    /// </summary>
    /// <param name="storageFile"> </param>
    /// <param name="newdisplayname">
    /// 文件新DisplayName名
    /// </param>
    /// <param name="collisiontag"> 若重名，则此补后缀 </param>
    /// <returns> </returns>
    public static async Task ReSetDisplayName_perhaps_UniqueName(
        this StorageFile storageFile,
        string newdisplayname,
        string collisiontag = "_副本"
    )
    {
        string extensionname = storageFile.FileType;
        try
        {
            await storageFile.RenameAsync(
                newdisplayname + extensionname,
                NameCollisionOption.FailIfExists
            );
        }
        catch (Exception)
        {
            await storageFile.ReSetDisplayName_perhaps_UniqueName(newdisplayname + collisiontag);
        }
    }
}