using System;
using System.IO;
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
            if (lines[0] != headersExpected) return false;
            var datePattern =
                "Data Zajęć: \\d\\d\\d\\d-\\d\\d-\\d\\d \\b(poniedziałek|wtorek|środa|czwartek|piątek|sobota|niedziela)\\b";
            const string possibleTitles =
                "(prof. zw. dr hab.|prof. WSEI dr hab.|prof. nadzw. dr|prof. dr hab. inż.|prof. dr hab.|mecenas|mgr inż.|mgr|dr inż.|inż.|dr hab. inż.|doc. dr|dr hab.|dr|MBA)";
            const string possibleLessonTypes = "( Cw | Lab | Konw | Wyk )";
            //TODO: remove duplicate possible titles string
            var lessonsPattern = "^(\\d?\\d:\\d\\d \\d?\\d:\\d\\d \\dh\\d\\dm " + possibleTitles + ".+" + 
                                 possibleLessonTypes + ".+)+$";
            var regexPattern = datePattern + "|" + lessonsPattern;
            var regex = new Regex(regexPattern);

            for (int i = 1; i < lines.Length; i++)
            {
                var match = regex.Match(lines[i]);
                if (!match.Success) throw new ParsingException(ParsingException.MatchingLineToPatternFailed + lines[i]);
            }

            return true;
        }
    }
}