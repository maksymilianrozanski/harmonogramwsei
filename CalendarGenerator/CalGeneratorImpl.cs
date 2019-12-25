using System.IO;
using CalendarGenerator.PdfRead;

namespace CalendarGenerator
{
    public class CalGeneratorImpl : ICalGenerator
    {
        /// <summary>
        /// Generate calendar text as string in ical format. 
        /// </summary>
        /// <param name="pdfFilePath">Path of the file containing schedule</param>
        /// <returns></returns>
        public string GenerateICalCalendar(string pdfFilePath)
        {
            var reader = new PdfTextReader();
            using var stream = File.Open(pdfFilePath, FileMode.Open, FileAccess.Read);
            var extractedText = reader.GetTextFromAllPages(stream);
            var iCalendarText = Calendar.Calendar.GenerateCalendar(extractedText);
            return iCalendarText;
        }

        /// <summary>
        /// Generate calendar text as string in ical format.
        /// </summary>
        /// <param name="pdfFileStream">FileStream of file containing schedule</param>
        /// <returns></returns>
        public string GenerateICalCalendar(FileStream pdfFileStream)
        {
            var reader = new PdfTextReader();
            var extractedText = reader.GetTextFromAllPages(pdfFileStream);
            var iCalendarText = Calendar.Calendar.GenerateCalendar(extractedText);
            return iCalendarText;
        }
    }
}