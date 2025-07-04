using System.Drawing;

using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace EroMangaManager.WinUI3.Helpers
{
    internal class Exporter
    {
        internal static void ExportAsPDF(MangaBook mangaBook, string target)
        {
            switch (mangaBook.MangaType)
            {
                case "":
                    FolderToPDF(mangaBook, target);
                    break;
                default:
                    CompressionFileToPDF(mangaBook, target);

                    break;
            }
        }

        private static void FolderToPDF(MangaBook mangaBook, string target)
        {
            var files = Directory.GetFiles(mangaBook.FilePath);
            var writestream = new FileStream(target, FileMode.Open, FileAccess.Write);

            using var writer = new PdfWriter(writestream);
            using var pdfDocument = new PdfDocument(writer);
            using var document = new Document(pdfDocument);
            foreach (var file in files)
            {
                var format = SixLabors.ImageSharp.Image.DetectFormat(file);
                string source = file;
                if (format.Name == "Webp")
                {
                    var b = SixLabors.ImageSharp.Image.Load(file);

                    source = Path.GetTempFileName();

                    b.SaveAsPng(source);
                }
                var imageData = ImageDataFactory.Create(source);

                var image = new iText.Layout.Element.Image(imageData);
                image.SetWidth(pdfDocument.GetDefaultPageSize().GetWidth());
                image.SetAutoScaleHeight(true);

                document.Add(image);
            }
            pdfDocument.Close();
        }

        private static void CompressionFileToPDF(MangaBook mangaBook, string target)
        {
            using var reader = new ReaderVM(mangaBook);
            reader.SelectEntries(null);

            var writestream = new FileStream(target, FileMode.Open, FileAccess.Write);

            using var writer = new PdfWriter(writestream);
            using var pdfDocument = new PdfDocument(writer);
            using var document = new Document(pdfDocument);
            foreach (var entry in reader.FilteredArchiveImageEntries)
            {
                using var stream = entry.OpenEntryStream();
                using var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                byte[] b = new byte[memoryStream.Length];
                memoryStream.Position = 0;
                memoryStream.Read(b, 0, b.Length);

                var imageData = ImageDataFactory.Create(b);

                var image = new iText.Layout.Element.Image(imageData);
                image.SetWidth(pdfDocument.GetDefaultPageSize().GetWidth());
                image.SetAutoScaleHeight(true);

                document.Add(image);
            }
            pdfDocument.Close();
        }
    }
}
