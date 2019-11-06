using System.Collections.Generic;
using CalendarGenerator.PdfParse;
using NUnit.Framework;

namespace CalGeneratorTests
{
    public class DayTests
    {
        [Test]
        public void DayEqualsTrueTest()
        {
            var dayOne = new Day("Data Zajęć: 2019-10-05 sobota", new List<string>
            {
                "8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena ",
                "11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin"
            });
            var dayTwo = new Day("Data Zajęć: 2019-10-05 sobota", new List<string>
            {
                "8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena ",
                "11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin"
            });
            Assert.AreEqual(dayOne, dayTwo);
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