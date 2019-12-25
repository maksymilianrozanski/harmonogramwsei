using System.IO;

namespace CalendarGenerator
{
    public interface ICalGenerator
    {
        /// <summary>
        /// Generate calendar text as string in ical format. 
        /// </summary>
        /// <param name="pdfFilePath">Path of the file containing schedule</param>
        /// <returns></returns>
        string GenerateICalCalendar(string pdfFilePath);

        /// <summary>
        /// Generate calendar text as string in ical format.
        /// </summary>
        /// <param name="pdfFileStream">FileStream of file containing schedule</param>
        /// <returns></returns>
        string GenerateICalCalendar(FileStream pdfFileStream);
    }
}