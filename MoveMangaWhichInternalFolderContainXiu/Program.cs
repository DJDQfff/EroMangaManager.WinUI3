var sourcefolder = @"D:\哔咔下载完成";
var targetfolder = @"D:\未确认是否无修无水印";
string[] checkstrings = ["无修", "无码", "無修", "無碼", "買動漫"];
var folders = Directory.GetDirectories(sourcefolder);

foreach (var folder in folders)
{
    var subfolders = Directory.EnumerateDirectories(folder, "*", new EnumerationOptions() { RecurseSubdirectories = true });
    var internalfolder = subfolders.Any(f => checkstrings.Any(str => f.Contains(str)));
    if (internalfolder)
    {
        var foldername = Path.GetFileName(folder);
        var newfolder = Path.Combine(targetfolder, foldername);
        if (!Directory.Exists(newfolder))
        {
            try
            {
                Directory.Move(folder, newfolder);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}