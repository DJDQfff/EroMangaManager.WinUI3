var sourcefolder = @"D:\jmcomic_tocheck\检查";
var targetfolder = @"D:\jmcomic_tocheck\新建文件夹";

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