using System;
using System.Text.RegularExpressions;
using CalendarGenerator.Calendar;

namespace CalendarGenerator.Lesson
{
    internal struct LessonText : IEquatable<LessonText>
    {
        public string LecturersTitleAndName { get; set; }
        public string LessonTitle { get; set; }
        public string LessonType { get; set; }
        public string LessonCodeAndClassRoom { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        internal const string Location = "św. Filipa 17";

        public LessonText(string dateString, string lessonString)
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
            var datePattern = "\\d\\d\\d\\d-\\d\\d-\\d\\d";
            var regex = new Regex(datePattern);
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
            var lecturerPattern =
                "(\\p{L}+ \\p{L}+\\-\\p{L}+)|(\\p{L}+ \\p{L}+ \\- \\p{L}+)|(\\p{L}+ \\p{L}+)";
            var nameRegex = new Regex(lecturerPattern);
            var nameMatch = nameRegex.Match(titleCutOff);
            return title + " " + nameMatch.Value;
        }

        internal static string ExtractLecturersTitle(string lessonString)
        {
            const string possibleTitles =
                "(prof. zw. dr hab.|prof. WSEI dr hab.|prof. nadzw. dr|prof. dr hab. inż.|prof. dr hab.|mecenas|mgr inż.|mgr|dr inż.|inż.|dr hab. inż.|doc. dr|dr hab.|dr|MBA)";
            var regex = new Regex(possibleTitles);
            var match = regex.Match(lessonString);
            return match.Value;
        }

        internal static string ExtractLessonType(string lessonString, string lecturersTitleAndName)
        {
            const string possibleLessonTypes = " Cw | Lab | Konw | Wyk ";
            var lecturersNameCutOff = lessonString.Split(lecturersTitleAndName)[1];
            var regex = new Regex(possibleLessonTypes);
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

        public bool Equals(LessonText other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return LecturersTitleAndName == other.LecturersTitleAndName && LessonTitle == other.LessonTitle &&
                   LessonType == other.LessonType && LessonCodeAndClassRoom == other.LessonCodeAndClassRoom &&
                   StartDateTime.Equals(other.StartDateTime) && EndDateTime.Equals(other.EndDateTime);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LessonText) obj);
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

        public static bool operator ==(LessonText left, LessonText right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LessonText left, LessonText right)
        {
            return !Equals(left, right);
        }
    }
}