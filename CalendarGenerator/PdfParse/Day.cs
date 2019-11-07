using System;
using System.Collections.Generic;
using System.Linq;
using CalendarGenerator.Lesson;

namespace CalendarGenerator.PdfParse
{
    public struct Day : IEquatable<Day>
    {
        public string Date { get; }
        public List<string> LessonStrings { get; }

        public Day(string date, List<string> lessonStrings)
        {
            Date = date;
            LessonStrings = lessonStrings;
        }

        internal readonly List<Lesson.Lesson> GetLessonTexts()
        {
            var lessons = new List<Lesson.Lesson>();
            foreach (var lessonString in LessonStrings)
            {
                var lessonText = new Lesson.Lesson(Date, lessonString);
                lessons.Add(lessonText);
            }

            return lessons;
        }

        public bool Equals(Day other)
        {
            return Date == other.Date && LessonStrings.SequenceEqual(other.LessonStrings);
        }

        public override bool Equals(object obj)
        {
            return obj is Day other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Date != null ? Date.GetHashCode() : 0) * 397) ^
                       (LessonStrings != null ? LessonStrings.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Day left, Day right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Day left, Day right)
        {
            return !left.Equals(right);
        }
    }
}