using System.Diagnostics;

using TonquerPicacgDatabaseReverse.Download;

var targetfolder = @"D:\哔咔下载完成";
DownloadContext downloadContext = new();
var downloads = downloadContext.Downloads;

var downloads_2001 = downloads.Where(x => x.Status == "2001").ToList();
Stopwatch sw = Stopwatch.StartNew();
System.Console.WriteLine(downloads_2001.Count(x => Directory.Exists(x.SavePath)));
Console.Read();
foreach (var download in downloads_2001)
{
    var oldfolder = download.SavePath.Replace(@"\original", string.Empty);
    if (Directory.Exists(oldfolder))
    {
        Console.WriteLine(oldfolder);
        var foldername = Path.GetFileName(oldfolder);
        var newfolder = Path.Combine(targetfolder, foldername);
        if (Directory.Exists(newfolder))
        {
            // 应该不可能重复
            continue;
        }
        else
        {
            try
            {
                sw.Restart();
                Directory.Move(oldfolder, newfolder);
                sw.Stop();
                Console.WriteLine(sw.Elapsed);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}