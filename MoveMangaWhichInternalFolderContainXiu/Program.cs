var sourcefolder = @"D:\哔咔下载完成";
var targetfolder = @"D:\未确认是否无修无水印";

var folders = Directory.GetDirectories(sourcefolder);

foreach (var folder in folders)
{
    var internalfolder = Directory.EnumerateDirectories(folder, "*修*", new EnumerationOptions() { RecurseSubdirectories = true }).Any();
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