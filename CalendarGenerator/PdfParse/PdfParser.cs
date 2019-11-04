using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CalGeneratorTests")]
namespace CalendarGenerator.PdfParse
{
    internal class PdfParser
    {
        internal static string[] RawTextToWords(string input)
        {
            var removedBreakLines = input.Replace("\n", " ");
            return removedBreakLines.Split(" ");
        }
    }
}