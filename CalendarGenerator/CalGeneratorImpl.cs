using System;
using System.IO;
using CalendarGenerator.PdfRead;

namespace CalendarGenerator
{
    public class CalGeneratorImpl : ICalGenerator
    {
        public string GenerateICalCalendar(string pdfFilePath)
        {
            var reader = new PdfTextReader();
            using var stream = File.Open(pdfFilePath, FileMode.Open, FileAccess.Read);
            var extractedText = reader.GetTextFromAllPages(stream);
            var iCalendarText = Calendar.Calendar.GenerateCalendar(extractedText);
            return iCalendarText;
        }

        public string GenerateICalCalendar(FileStream pdfFileStream)
        {
            // TODO: not implemented yet
            return "not implemented yet";
        }
    }
}