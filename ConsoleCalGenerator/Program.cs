using System;
using System.IO;
using CalendarGenerator.PdfRead;
using Calendar = CalendarGenerator.Calendar.Calendar;

namespace ConsoleCalGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter pdf file path.");
            var path = Console.ReadLine();
            var reader = new PdfTextReader();
            
            using var stream = File.Open(path, FileMode.Open, FileAccess.Read);
            var extractedText = reader.GetTextFromAllPages(stream);
            var iCalCalendarText = Calendar.GenerateCalendar(extractedText);
            Console.WriteLine(iCalCalendarText);
        }
    }
}