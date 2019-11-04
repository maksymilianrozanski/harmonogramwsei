using System;
using System.IO;
using CalendarGenerator.PdfRead;

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
            Console.WriteLine(extractedText);
        }
    }
}