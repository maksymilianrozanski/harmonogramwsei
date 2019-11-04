using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("CalGeneratorTests"), InternalsVisibleTo("ConsoleCalGenerator")]

namespace CalendarGenerator.PdfParse
{
    internal class PdfParser
    {
        internal static string[] RawTextToWords(string input)
        {
            var removedBreakLines = input.Replace("\n", " ");
            return removedBreakLines.Split(" ");
        }

        internal static List<string> WordsToDayItems(string[] words)
        {
            var stringBuilder = new StringBuilder();
            var dayItems = new List<string>();
            var dayPattern = "Data";
            var hourPattern = "\\d?\\d:\\d\\d";

            int wordCounter = 0;

            foreach (var word in words)
            {
                var dayMatcher = Regex.Match(word, dayPattern);
                var hourMatcher = Regex.Match(word, hourPattern);

                if (dayMatcher.Success)
                {
                    if (stringBuilder.Length != 0) dayItems.Add(stringBuilder.ToString().Trim());
                    stringBuilder.Clear();
                    wordCounter = 0;
                    stringBuilder.Append(word);
                    stringBuilder.Append(" ");
                    wordCounter++;
                }
                else if (hourMatcher.Success && wordCounter > 0)
                {
                    stringBuilder.Append(word);
                    stringBuilder.Append(" ");
                    wordCounter++;
                }
                else if (hourMatcher.Success)
                {
                    if (stringBuilder.Length != 0) dayItems.Add(stringBuilder.ToString().Trim());
                    stringBuilder.Clear();
                    wordCounter = 0; //TODO: check use of wordCounter
                    stringBuilder.Append(word);
                    stringBuilder.Append(" ");
                    wordCounter++;
                }
                else if (wordCounter > 0)
                {
                    stringBuilder.Append(word);
                    stringBuilder.Append(" ");
                    wordCounter++;
                }
            }

            dayItems.Add(stringBuilder.ToString().Trim());
            return dayItems;
        }
    }
}