using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CalendarGenerator.PdfParse;

namespace CalendarGenerator.Calendar
{
    public class Calendar
    {
        public static string GenerateCalendar(string rawInput)
        {
            var daysList = PdfParser.GetDaysList(rawInput);
            var events =
                daysList.SelectMany(day => day.GetLessonTexts()).Select(text => text.ToCalendarEvent());

            var calendar = new StringBuilder();
            calendar.AppendLine("BEGIN:VCALENDAR");
            calendar.AppendLine("VERSION:2.0");
            calendar.AppendLine("PRODID:Schedule_generated_with_itext7");
            calendar.AppendLine("CALSCALE:GREGORIAN");

            events.ToList().ForEach(ev =>
            {
                calendar.AppendLine("BEGIN:VEVENT");
                calendar.AppendLine("DTSTAMP:" + FormatDateTime(ev.TimeStamp));
                calendar.AppendLine("DTSTART:" + FormatDateTime(ev.Start));
                calendar.AppendLine("DTEND:" + FormatDateTime(ev.End));
                calendar.AppendLine("SUMMARY:" + ev.Summary);
                calendar.AppendLine("DESCRIPTION:" + ev.Description);
                calendar.AppendLine("LOCATION:" + ev.Location);
                calendar.AppendLine("UID:" + ev.Uid);
                calendar.AppendLine("END:VEVENT");
            });

            calendar.AppendLine("END:VCALENDAR");
            return calendar.ToString();
        }

        private static string FormatDateTime(DateTime dateTime)
        {
            return dateTime.Year + AppendZeroIfSingleDigit(dateTime.Month) +
                   AppendZeroIfSingleDigit(dateTime.Day) + "T" +
                   AppendZeroIfSingleDigit(dateTime.Hour) +
                   AppendZeroIfSingleDigit(dateTime.Minute) +
                   "00";
        }

        private static string AppendZeroIfSingleDigit(int number)
        {
            if (number >= 10) return number.ToString();
            return "0" + number;
        }

        internal static bool ValidateInput(string rawInput)
        {
            var lines = rawInput.Split("\n");
            var headersExpected =
                "Czas od Czas do Liczba godzin Prowadzący Przedmiot Forma zaj. Grupy Sala Forma zaliczenia Uwagi";
            if (lines[0] != headersExpected) throw new ParsingException(ParsingException.HeadersNotMatched + lines[0]);
            var lessonsPattern = "^(" + PdfParser.HoursPattern + " \\dh\\d\\dm " + PdfParser.PossibleTitlesPattern +
                                 ".+(" + PdfParser.PossibleLessonTypesPattern + ").+)+$";
            var regexPattern = PdfParser.DateAndDayPatternLine + "|" + lessonsPattern;
            var regex = new Regex(regexPattern);

            for (var i = 1; i < lines.Length; i++)
            {
                var match = regex.Match(lines[i]);
                if (!match.Success) throw new ParsingException(ParsingException.MatchingLineToPatternFailed + lines[i]);
            }

            return true;
        }
    }
}