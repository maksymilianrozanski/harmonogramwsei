using System;
using System.IO;
using CalendarGenerator;

namespace ConsoleCalGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter pdf file path.");
            var path = Console.ReadLine();
            var generator = new CalGeneratorImpl();
            using var fileStream = File.OpenRead(path);
            var calendar = generator.GenerateICalCalendar(fileStream);
            Console.WriteLine("Enter path and filename where calendar should be saved.");
            var destination = Console.ReadLine();
            File.WriteAllText(destination, calendar);
        }
    }
}