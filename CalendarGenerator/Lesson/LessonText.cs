using System;
using System.Text.RegularExpressions;
using CalendarGenerator.Calendar;

namespace CalendarGenerator.Lesson
{
    internal class LessonText : IEquatable<LessonText>
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
            this.LecturersTitleAndName = ExtractLecturer(lessonString);
            this.LessonType = ExtractLessonType(lessonString, LecturersTitleAndName);
            this.LessonTitle = ExtractLessonName(lessonString, LecturersTitleAndName, LessonType);
            this.LessonCodeAndClassRoom = ExtractLessonCodeAndClassRoom(lessonString, LessonTitle, LessonType);
            this.StartDateTime = ParseToDateTime(date, startHour);
            this.EndDateTime = ParseToDateTime(date, endHour);
        }

        internal LessonText()
        {
        }

        internal CalendarEvent ToCalendarEvent()
        {
            var calEvent = new CalendarEvent
            {
                Start = StartDateTime,
                End = EndDateTime,
                Summary = LessonType + " " + LessonTitle,
                Description = LessonCodeAndClassRoom + " " + LecturersTitleAndName,
                Location = LessonText.Location
            };
            return calEvent;
        }

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

        public static DateTime ParseToDateTime(string date, string hour)
        {
            DateTime dateTime = DateTime.Parse(date + " " + hour);
            return dateTime;
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
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LessonText) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (LecturersTitleAndName != null ? LecturersTitleAndName.GetHashCode() : 0);
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