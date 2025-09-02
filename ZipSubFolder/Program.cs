// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

Console.WriteLine("Hello, World!");
var exepath = @"C:\Program Files\7-Zip\7z.exe";
var sourcefolder = @"D:\移动到128G卡：AI CG 卖片";
var targetfolder = @"D:\test";/*F:\图集*/
var folders = Directory.GetDirectories(sourcefolder);
foreach (var folder in folders)
{
    var foldername = Path.GetFileName(folder);
    var newfolder = Path.Combine(targetfolder, foldername);
    Directory.CreateDirectory(newfolder);
    var originfolder = Directory.GetDirectories(folder).SingleOrDefault();
    if (originfolder != null && Path.GetFileName(originfolder) == "original")
    {
        var subfolders = Directory.GetDirectories(originfolder);
        foreach (var subfolder in subfolders)
        {
            var subname = Path.GetFileName(subfolder);
            var zipfolder = Path.Combine(newfolder, subname);

            var newzippath = zipfolder + ".zip";
            if (!Directory.Exists(newzippath))
            {
                var arguments = $" a -tzip {newzippath} \"{subfolder}\"\\*\" ";
                Process.Start(exepath, arguments).WaitForExit();
                return;
            }
        }
    }
}