using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace CalendarGenerator.PdfRead
{
    public class PdfTextReader
    {
        public string GetTextFromAllPages(string filePath)
        {
            var reader = new PdfReader(filePath);
            return ExtractTextFromReader(reader);
        }
        
        public string GetTextFromAllPages(FileStream fileStream)
        {
            var reader = new PdfReader(fileStream);
            return ExtractTextFromReader(reader);
        }

        private string ExtractTextFromReader(PdfReader reader)
        {
            var doc = new PdfDocument(reader);
            var strategy = new SimpleTextExtractionStrategy();

            for (int pageNum = 1; pageNum < doc.GetNumberOfPages(); pageNum++)
            {
                var page = doc.GetPage(pageNum);
                PdfTextExtractor.GetTextFromPage(page, strategy);
            }

            //PdfTextExtractor "remember" previous pages
            var allPagesText = PdfTextExtractor.GetTextFromPage(doc.GetLastPage(), strategy);
            return allPagesText;
        }
    }
}