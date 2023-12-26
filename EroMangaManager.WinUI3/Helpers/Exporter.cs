using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;

namespace EroMangaManager.WinUI3.Helpers
{
    internal class Exporter
    {
        internal static void ExportAsPDF (MangaBook mangaBook , string target)
        {
            using var reader = new ReaderVM(mangaBook);
            reader.SelectEntries(null);

            var writestream = new FileStream(target , FileMode.Open , FileAccess.Write);

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
                memoryStream.Read(b , 0 , b.Length);

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