using System;
using System.IO;
using CalendarGenerator;
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
            var generator = new CalGeneratorImpl();
            var calendar = generator.GenerateICalCalendar(path);
            Console.WriteLine(calendar);
        }
    }
}