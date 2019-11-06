using System;
using System.Collections.Generic;
using System.Linq;
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
            const string dayPattern = "Data";
            var afterTableHeader = false;

            foreach (var word in words)
            {
                var dayMatcher = Regex.Match(word, dayPattern);
                if (dayMatcher.Success)
                {
                    afterTableHeader = true;
                    if (stringBuilder.Length != 0) dayItems.Add(stringBuilder.ToString().Trim());
                    stringBuilder.Clear();
                    stringBuilder.Append(word + " ");
                }
                else if (afterTableHeader)
                {
                    stringBuilder.Append(word + " ");
                }
            }

            dayItems.Add(stringBuilder.ToString().Trim());
            return dayItems;
        }

        internal static string ExtractDateFromDayStringItem(string dayStringItem)
        {
            const string datePattern =
                "Data Zajęć: \\d\\d\\d\\d-\\d\\d-\\d\\d \\b(poniedziałek|wtorek|środa|czwartek|piątek|sobota|niedziela)\\b";
            Regex regex = new Regex(datePattern);
            Match match = regex.Match(dayStringItem);
            if (match.Success == false) throw new ParsingException(ParsingException.MatchingDateFailed);
            if (match.Index != 0) throw new ParsingException(ParsingException.IndexOfMatchedItemNotZero);
            return match.Value;
        }

        internal static List<string> ExtractLessonStringsFromDayStringItem(string dayStringItem)
        {
            var lessons = new List<string>();

            const string startPattern = "\\d?\\d:\\d\\d \\d?\\d:\\d\\d";
            Regex regex = new Regex(startPattern);
            MatchCollection matches = regex.Matches(dayStringItem);

            for (int i = 0; i < matches.Count - 1; i++)
            {
                var start = matches[i].Index;
                var length = matches[i].NextMatch().Index - start;
                lessons.Add(dayStringItem.Substring(start, length));
            }

            lessons.Add(dayStringItem.Substring(matches[^1].Index));
            return lessons;
        }
    }

    internal class ParsingException : Exception
    {
        internal const string MatchingDateFailed = "Matching date not successful";
        internal const string IndexOfMatchedItemNotZero = "Index of matched item not equals to 0";

        public ParsingException(string message) : base(message)
        {
        }
    }
}