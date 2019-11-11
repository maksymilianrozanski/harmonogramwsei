using System.Text.RegularExpressions;
using CalendarGenerator.PdfParse;
using NUnit.Framework;

namespace CalGeneratorTests.Calendar
{
    public class CalendarTests
    {
        private const string ExampleRawInput =
            "Czas od Czas do Liczba godzin Prowadzący Przedmiot Forma zaj. Grupy Sala Forma zaliczenia Uwagi\nData Zajęć: 2019-10-04 piątek\n17:30 19:00 2h00m dr Name Surname Advanced Math Wyk W/1/Web F Toronto Egzamin 19:15 20:45 2h00m doc. dr John Smiths Modern History of Poland Wyk W/1/Web F Praga Egzamin\nData Zajęć: 2019-10-05 sobota\n8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena 11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin\nData Zajęć: 2019-10-06 niedziela\n8:00 9:30 2h00m mgr Jack Green Wzorce projektowe Lab lab/WebN F Montreal Zaliczenie ocena 9:40 11:10 2h00m mgr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena 14:40 16:10 2h00m dr George White Geology Lab lab51/22/WebN F San Francisco Zaliczenie ocena 16:20 17:50 2h00m mgr Richard White Basic Mathematics Lab lab22/22/WebN F Chicago Zaliczenie ocena 18:00 19:30 2h00m mgr David Smith Civil Engineering Lab lab3/3/WebN F Nowy Jork Zaliczenie ocena 19:40 21:10 2h00m mgr Jacob Brown Advanced Mathematics Konw konw/2/WebN F Toronto Zaliczenie ocena\nData Zajęć: 2019-10-18 piątek\n17:30 19:00 2h00m dr Jacob Brown Advanced Physics Wyk W/2/WebN F Toronto Egzamin 19:15 20:45 2h00m dr inż. Thomas Blue Geology Konw konw/2/WebN F Praga Zaliczenie ocena ";

        [Test]
        public void ValidateInputTest()
        {
            var input = ExampleRawInput;
            var result = CalendarGenerator.Calendar.Calendar.ValidateInput(input);
            Assert.True(result);
        }

        [Test]
        public void ValidateInputInvalidHeadersTest()
        {
            var input = ExampleRawInput.Replace("Czas od", "Invalid");
            var exception = Assert.Throws<ParsingException>(() =>
            {
                CalendarGenerator.Calendar.Calendar.ValidateInput(input);
            });
            Assert.AreEqual("Headers not matched:" + input.Split("\n")[0], exception.Message);
        }

        [Test]
        public void ValidateInputInvalidDateFormatTest()
        {
            var input = ExampleRawInput.Replace("2019-10-04", "2019:10:04");
            var exception = Assert.Throws<ParsingException>(() =>
            {
                CalendarGenerator.Calendar.Calendar.ValidateInput(input);
            });
            Assert.AreEqual("Matching line to pattern failed:" + input.Split("\n")[1], exception.Message);
        }
    }
}