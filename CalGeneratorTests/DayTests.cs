using System.Collections.Generic;
using CalendarGenerator.Lesson;
using CalendarGenerator.PdfParse;
using NUnit.Framework;

namespace CalGeneratorTests
{
    public class DayTests
    {
        private readonly Day _dayOne = new Day("Data Zajęć: 2019-10-05 sobota", new List<string>
        {
            "8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena ",
            "11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin"
        });

        private readonly Day _dayTwo = new Day("Data Zajęć: 2019-10-05 sobota", new List<string>
        {
            "8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena ",
            "11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin"
        });

        [Test]
        public void GetLessonTextsTest()
        {
            var expected = new List<CalendarGenerator.Lesson.Lesson>
            {
                new CalendarGenerator.Lesson.Lesson(_dayOne.Date, _dayOne.LessonStrings[0]),
                new CalendarGenerator.Lesson.Lesson(_dayOne.Date, _dayOne.LessonStrings[1])
            };
            var result = _dayOne.GetLessonTexts();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DayEqualsTrueTest()
        {
            Assert.AreEqual(_dayOne, _dayTwo);
        }

        [Test]
        public void DayEqualsFalseDifferentDateTest()
        {
            var dayOne = new Day("Data Zajęć: 2019-10-06 niedziela", new List<string>
            {
                "8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena ",
                "11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin"
            });
            var dayTwo = new Day("Data Zajęć: 2019-10-05 sobota", new List<string>
            {
                "8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena ",
                "11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin"
            });
            Assert.AreNotEqual(dayOne, dayTwo);
        }

        [Test]
        public void DayEqualsFalseDifferentLessonsTest()
        {
            var dayOne = new Day("Data Zajęć: 2019-10-05 sobota", new List<string>
            {
                "8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena ",
                "11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin"
            });
            var dayTwo = new Day("Data Zajęć: 2019-10-05 sobota", new List<string>
            {
                "8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena ",
                "11:20 14:30 4h00m doc. dr John Black Mathematics Wyk W/2/W F Toronto Egzamin"
            });
            Assert.AreNotEqual(dayOne, dayTwo);
        }
    }
}