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
    }
}