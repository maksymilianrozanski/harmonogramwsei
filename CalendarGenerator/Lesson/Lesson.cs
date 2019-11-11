using System;
using System.Text.RegularExpressions;
using CalendarGenerator.Calendar;
using CalendarGenerator.PdfParse;

namespace CalendarGenerator.Lesson
{
    internal struct Lesson : IEquatable<Lesson>
    {
        public string LecturersTitleAndName { get; }
        public string LessonTitle { get; }
        public string LessonType { get; }
        public string LessonCodeAndClassRoom { get; }
        public DateTime StartDateTime { get; }
        public DateTime EndDateTime { get; }

        internal const string Location = "św. Filipa 17";

        public Lesson(string dateString, string lessonString)
        {
            var date = ExtractDate(dateString);
            ExtractHours(lessonString, out var startHour, out var endHour);
            LecturersTitleAndName = ExtractLecturer(lessonString);
            LessonType = ExtractLessonType(lessonString, LecturersTitleAndName);
            LessonTitle = ExtractLessonName(lessonString, LecturersTitleAndName, LessonType);
            LessonCodeAndClassRoom = ExtractLessonCodeAndClassRoom(lessonString, LessonTitle, LessonType);
            StartDateTime = ParseToDateTime(date, startHour);
            EndDateTime = ParseToDateTime(date, endHour);
        }

        internal readonly CalendarEvent ToCalendarEvent()
        {
            var calEvent = new CalendarEvent
            {
                Start = StartDateTime,
                End = EndDateTime,
                Summary = LessonType + " " + LessonTitle,
                Description = LessonCodeAndClassRoom + " " + LecturersTitleAndName,
                Location = Location
            };
            return calEvent;
        }

        internal static string ExtractDate(string input)
        {
            var regex = new Regex(PdfParser.DatePattern);
            var match = regex.Match(input);
            return match.Value;
        }

        public static void ExtractHours(string lessonString, out string start, out string end)
        {
            var wordArray = lessonString.Split(" ");
            start = wordArray[0];
            end = wordArray[1];
        }

        public static DateTime ParseToDateTime(string date, string hour)
        {
            var dateTime = DateTime.Parse(date + " " + hour);
            return dateTime;
        }

        public static string ExtractLecturer(string lessonString)
        {
            var title = ExtractLecturersTitle(lessonString);
            var matchIndex = lessonString.IndexOf(title, StringComparison.Ordinal);
            var endOfTitle = matchIndex + title.Length;
            var titleCutOff = lessonString.Substring(endOfTitle);
            const string lecturerPattern =
                "(\\p{L}+ \\p{L}+\\-\\p{L}+)|(\\p{L}+ \\p{L}+ \\- \\p{L}+)|(\\p{L}+ \\p{L}+)";
            var nameRegex = new Regex(lecturerPattern);
            var nameMatch = nameRegex.Match(titleCutOff);
            return title + " " + nameMatch.Value;
        }

        internal static string ExtractLecturersTitle(string lessonString)
        {
            var regex = new Regex(PdfParser.PossibleTitlesPattern);
            var match = regex.Match(lessonString);
            return match.Value;
        }

        internal static string ExtractLessonType(string lessonString, string lecturersTitleAndName)
        {
            var lecturersNameCutOff = lessonString.Split(lecturersTitleAndName)[1];
            var regex = new Regex(PdfParser.PossibleLessonTypesPattern);
            var match = regex.Match(lecturersNameCutOff);
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
            var regex = new Regex(examTypePattern);
            var match = regex.Match(lessonTitleCutOff);
            var examType = match.Value;
            return lessonTitleCutOff.Split(examType)[0].Trim();
        }

        public bool Equals(Lesson other)
        {
            return LecturersTitleAndName == other.LecturersTitleAndName && LessonTitle == other.LessonTitle &&
                   LessonType == other.LessonType && LessonCodeAndClassRoom == other.LessonCodeAndClassRoom &&
                   StartDateTime.Equals(other.StartDateTime) && EndDateTime.Equals(other.EndDateTime);
        }

        public override bool Equals(object obj)
        {
            return obj is Lesson other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = LecturersTitleAndName != null ? LecturersTitleAndName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (LessonTitle != null ? LessonTitle.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LessonType != null ? LessonType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^
                           (LessonCodeAndClassRoom != null ? LessonCodeAndClassRoom.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ StartDateTime.GetHashCode();
                hashCode = (hashCode * 397) ^ EndDateTime.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Lesson left, Lesson right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Lesson left, Lesson right)
        {
            return !Equals(left, right);
        }
    }
}