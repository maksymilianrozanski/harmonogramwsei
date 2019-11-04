using System.IO;

namespace CalendarGenerator
{
    public interface ICalGenerator
    {
        string GenerateICalCalendar(string pdfFilePath);

        string GenerateICalCalendar(FileStream pdfFileStream);
    }
}