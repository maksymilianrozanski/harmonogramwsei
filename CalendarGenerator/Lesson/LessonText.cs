using System;
using System.Text.RegularExpressions;

namespace CalendarGenerator.Lesson
{
    internal class LessonText
    {
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Lecturer { get; set; }
        public string LessonTitle { get; set; }
        public string LessonType { get; set; }
        public string LessonCode { get; set; }
        public string ClassRoom { get; set; }


        internal static string ExtractDate(string input)
        {
            var datePattern = "\\d\\d\\d\\d-\\d\\d-\\d\\d";
            Regex regex = new Regex(datePattern);
            Match match = regex.Match(input);
            return match.Value;
        }

        public static void ExtractHours(string lessonString, out string start, out string end)
        {
            var wordArray = lessonString.Split(" ");
            start = wordArray[0];
            end = wordArray[1];
        }

        public static string ExtractLecturer(string lessonString)
        {
            var title = ExtractLecturersTitle(lessonString);
            var matchIndex = lessonString.IndexOf(title, StringComparison.Ordinal);
            int endOfTitle = matchIndex + title.Length;
            var titleCutOff = lessonString.Substring(endOfTitle);
            //TODO: implement double surname recognizing
            var lecturerPattern = "[a-zA-Z]+ [a-zA-Z]+";
            Regex nameRegex = new Regex(lecturerPattern);
            Match nameMatch = nameRegex.Match(titleCutOff);
            return title + " " + nameMatch.Value;
        }

        internal static string ExtractLecturersTitle(string lessonString)
        {
            const string possibleTitles =
                "(prof. zw. dr hab.|prof. WSEI dr hab.|prof. nadzw. dr|prof. dr hab. inż.|prof. dr hab.|mecenas|mgr inż.|mgr|dr inż.|inż.|dr hab. inż.|doc. dr|dr hab.|dr|MBA)";
            Regex regex = new Regex(possibleTitles);
            Match match = regex.Match(lessonString);
            return match.Value;
        }
    }
}