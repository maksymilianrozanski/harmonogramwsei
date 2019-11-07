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
        public string LessonCodeAndClassRoom { get; set; }

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
            var lecturerPattern =
                "([a-zA-Z]+ [a-zA-Z]+\\-[a-zA-Z]+)|([a-zA-Z]+ [a-zA-Z]+ \\- [a-zA-Z]+)|([a-zA-Z]+ [a-zA-Z]+)";
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

        internal static string ExtractLessonType(string lessonString, string lecturersTitleAndName)
        {
            const string possibleLessonTypes = " Cw | Lab | Konw | Wyk ";
            var lecturersNameCutOff = lessonString.Split(lecturersTitleAndName)[1];
            Regex regex = new Regex(possibleLessonTypes);
            Match match = regex.Match(lecturersNameCutOff);
            return match.Value.Trim();
        }

        internal static string ExtractLessonName(string lessonString, string lecturersTitleAndName, string lessonType)
        {
            var lecturersNameCutOff = lessonString.Split(lecturersTitleAndName)[1];
            return lecturersNameCutOff.Split(lessonType)[0].Trim();
        }

        internal static string ExtractLessonCodeAndClassRoom(string lessonString, string lessonName, string lessonType)
        {
            var lessonTitleCutOff = lessonString.Split(lessonName + " " + lessonType)[1];
            const string examTypePattern = " Egzamin| Zaliczenie ocena";
            Regex regex = new Regex(examTypePattern);
            Match match = regex.Match(lessonTitleCutOff);
            var examType = match.Value;
            return lessonTitleCutOff.Split(examType)[0].Trim();
        }
    }
}