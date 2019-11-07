using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CalendarGenerator.PdfParse;
using NUnit.Framework;

namespace CalGeneratorTests
{
    public class PdfParserTests
    {
        [Test]
        public void GetDaysListTest()
        {
            var input =
                "Czas od Czas do Liczba godzin Prowadzący Przedmiot Forma zaj. Grupy Sala Forma zaliczenia Uwagi Data Zajęć: 2019-10-04 piątek 17:30 19:00 2h00m dr Name Surname Advanced Math Wyk W/1/Web F Toronto Egzamin 19:15 20:45 2h00m doc. dr John Smiths Modern History of Poland Wyk W/1/Web F Praga Egzamin Data Zajęć: 2019-10-05 sobota 8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena 11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin Data Zajęć: 2019-10-06 niedziela 8:00 9:30 2h00m mgr Jack Green Wzorce projektowe Lab lab/WebN F Montreal Zaliczenie ocena 9:40 11:10 2h00m mgr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena 14:40 16:10 2h00m dr George White Geology Lab lab51/22/WebN F San Francisco Zaliczenie ocena 16:20 17:50 2h00m mgr Richard White Basic Mathematics Lab lab22/22/WebN F Chicago Zaliczenie ocena 18:00 19:30 2h00m mgr David Smith Civil Engineering Lab lab3/3/WebN F Nowy Jork Zaliczenie ocena 19:40 21:10 2h00m mgr Jacob Brown Advanced Mathematics Konw konw/2/WebN F Toronto Zaliczenie ocena Data Zajęć: 2019-10-18 piątek 17:30 19:00 2h00m dr Jacob Brown Advanced Physics Wyk W/2/WebN F Toronto Egzamin 19:15 20:45 2h00m dr inż. Thomas Blue Geology Konw konw/2/WebN F Praga Zaliczenie ocena ";
            var expected = new List<Day>
            {
                new Day
                (
                    "Data Zajęć: 2019-10-04 piątek",
                    new List<string>
                    {
                        "17:30 19:00 2h00m dr Name Surname Advanced Math Wyk W/1/Web F Toronto Egzamin ",
                        "19:15 20:45 2h00m doc. dr John Smiths Modern History of Poland Wyk W/1/Web F Praga Egzamin"
                    }
                ),
                new Day
                (
                    "Data Zajęć: 2019-10-05 sobota",
                    new List<string>
                    {
                        "8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena ",
                        "11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin"
                    }
                ),
                new Day
                (
                    "Data Zajęć: 2019-10-06 niedziela",
                    new List<string>
                    {
                        "8:00 9:30 2h00m mgr Jack Green Wzorce projektowe Lab lab/WebN F Montreal Zaliczenie ocena ",
                        "9:40 11:10 2h00m mgr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ",
                        "14:40 16:10 2h00m dr George White Geology Lab lab51/22/WebN F San Francisco Zaliczenie ocena ",
                        "16:20 17:50 2h00m mgr Richard White Basic Mathematics Lab lab22/22/WebN F Chicago Zaliczenie ocena ",
                        "18:00 19:30 2h00m mgr David Smith Civil Engineering Lab lab3/3/WebN F Nowy Jork Zaliczenie ocena ",
                        "19:40 21:10 2h00m mgr Jacob Brown Advanced Mathematics Konw konw/2/WebN F Toronto Zaliczenie ocena"
                    }
                ),
                new Day
                (
                    "Data Zajęć: 2019-10-18 piątek",
                    new List<string>
                    {
                        "17:30 19:00 2h00m dr Jacob Brown Advanced Physics Wyk W/2/WebN F Toronto Egzamin ",
                        "19:15 20:45 2h00m dr inż. Thomas Blue Geology Konw konw/2/WebN F Praga Zaliczenie ocena"
                    })
            };
            var result = PdfParser.GetDaysList(input);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void RawTextToWordsTest()
        {
            var expectedOutput = new[] {"Some", "example", "input", "text"};
            var input = expectedOutput[0] + " " + expectedOutput[1] + "\n" + expectedOutput[2] + " " +
                        expectedOutput[3];
            var result = PdfParser.RawTextToWords(input);
            Assert.AreEqual(expectedOutput, result);
        }

        [Test]
        public void WordsToStringDayItemsTest()
        {
            var expectedOutput = new List<string>
            {
                "Data Zajęć: 2019-10-04 piątek 17:30 19:00 2h00m dr Name Surname Advanced Math Wyk W/1/Web F Toronto Egzamin 19:15 20:45 2h00m doc. dr John Smiths Modern History of Poland Wyk W/1/Web F Praga Egzamin",
                "Data Zajęć: 2019-10-05 sobota 8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena 11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin",
                "Data Zajęć: 2019-10-06 niedziela 8:00 9:30 2h00m mgr Jack Green Wzorce projektowe Lab lab/WebN F Montreal Zaliczenie ocena 9:40 11:10 2h00m mgr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena 14:40 16:10 2h00m dr George White Geology Lab lab51/22/WebN F San Francisco Zaliczenie ocena 16:20 17:50 2h00m mgr Richard White Basic Mathematics Lab lab22/22/WebN F Chicago Zaliczenie ocena 18:00 19:30 2h00m mgr David Smith Civil Engineering Lab lab3/3/WebN F Nowy Jork Zaliczenie ocena 19:40 21:10 2h00m mgr Jacob Brown Advanced Mathematics Konw konw/2/WebN F Toronto Zaliczenie ocena",
                "Data Zajęć: 2019-10-18 piątek 17:30 19:00 2h00m dr Jacob Brown Advanced Physics Wyk W/2/WebN F Toronto Egzamin 19:15 20:45 2h00m dr inż. Thomas Blue Geology Konw konw/2/WebN F Praga Zaliczenie ocena"
            };
            var wordsInput =
                "Czas od Czas do Liczba godzin Prowadzący Przedmiot Forma zaj. Grupy Sala Forma zaliczenia Uwagi Data Zajęć: 2019-10-04 piątek 17:30 19:00 2h00m dr Name Surname Advanced Math Wyk W/1/Web F Toronto Egzamin 19:15 20:45 2h00m doc. dr John Smiths Modern History of Poland Wyk W/1/Web F Praga Egzamin Data Zajęć: 2019-10-05 sobota 8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena 11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin Data Zajęć: 2019-10-06 niedziela 8:00 9:30 2h00m mgr Jack Green Wzorce projektowe Lab lab/WebN F Montreal Zaliczenie ocena 9:40 11:10 2h00m mgr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena 14:40 16:10 2h00m dr George White Geology Lab lab51/22/WebN F San Francisco Zaliczenie ocena 16:20 17:50 2h00m mgr Richard White Basic Mathematics Lab lab22/22/WebN F Chicago Zaliczenie ocena 18:00 19:30 2h00m mgr David Smith Civil Engineering Lab lab3/3/WebN F Nowy Jork Zaliczenie ocena 19:40 21:10 2h00m mgr Jacob Brown Advanced Mathematics Konw konw/2/WebN F Toronto Zaliczenie ocena Data Zajęć: 2019-10-18 piątek 17:30 19:00 2h00m dr Jacob Brown Advanced Physics Wyk W/2/WebN F Toronto Egzamin 19:15 20:45 2h00m dr inż. Thomas Blue Geology Konw konw/2/WebN F Praga Zaliczenie ocena "
                    .Split(" ");
            var result = PdfParser.WordsToStringDayItems(wordsInput);
            Assert.AreEqual(expectedOutput, result);
        }

        [Test]
        public void ExtractDateFromDayStringItemSuccessTest()
        {
            var dayStringItem =
                "Data Zajęć: 2019-10-04 piątek 17:30 19:00 2h00m dr Name Surname Advanced Math Wyk W/1/Web F Toronto Egzamin 19:15 20:45 2h00m doc. dr John Smiths Modern History of Poland Wyk W/1/Web F Praga Egzamin";
            var result = PdfParser.ExtractDateFromDayStringItem(dayStringItem);
            Assert.AreEqual("Data Zajęć: 2019-10-04 piątek", result);
        }

        [Test]
        public void ExtractDateFromStringItemNotSuccessfulTest1()
        {
            var dayStringItem =
                "Da_ta Zajęć: 2019-10-04 piątek 17:30 19:00 2h00m dr Name Surname Advanced Math Wyk W/1/Web F Toronto Egzamin 19:15 20:45 2h00m doc. dr John Smiths Modern History of Poland Wyk W/1/Web F Praga Egzamin";
            var exception = Assert.Throws<ParsingException>(() =>
            {
                PdfParser.ExtractDateFromDayStringItem(dayStringItem);
            });
            Assert.That(exception.Message, Is.EqualTo(ParsingException.MatchingDateFailed));
        }

        [Test]
        public void ExtractDateFromStringItemNotSuccessfulTest2()
        {
            var dayStringItem =
                "    Data Zajęć: 2019-10-04 piątek 17:30 19:00 2h00m dr Name Surname Advanced Math Wyk W/1/Web F Toronto Egzamin 19:15 20:45 2h00m doc. dr John Smiths Modern History of Poland Wyk W/1/Web F Praga Egzamin";
            var exception = Assert.Throws<ParsingException>(() =>
            {
                PdfParser.ExtractDateFromDayStringItem(dayStringItem);
            });
            Assert.That(exception.Message, Is.EqualTo(ParsingException.IndexOfMatchedItemNotZero));
        }

        [Test]
        public void ExtractLessonStringsFromDayStringItemsTest()
        {
            var dayStringItem =
                "Data Zajęć: 2019-10-06 niedziela 8:00 9:30 2h00m mgr Jack Green Wzorce projektowe Lab lab/WebN F Montreal Zaliczenie ocena " +
                "9:40 11:10 2h00m mgr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena " +
                "14:40 16:10 2h00m dr George White Geology Lab lab51/22/WebN F San Francisco Zaliczenie ocena " +
                "16:20 17:50 2h00m mgr Richard White Basic Mathematics Lab lab22/22/WebN F Chicago Zaliczenie ocena " +
                "18:00 19:30 2h00m mgr David Smith Civil Engineering Lab lab3/3/WebN F Nowy Jork Zaliczenie ocena " +
                "19:40 21:10 2h00m mgr Jacob Brown Advanced Mathematics Konw konw/2/WebN F Toronto Zaliczenie ocena";

            var expected = new List<string>
            {
                "8:00 9:30 2h00m mgr Jack Green Wzorce projektowe Lab lab/WebN F Montreal Zaliczenie ocena ",
                "9:40 11:10 2h00m mgr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ",
                "14:40 16:10 2h00m dr George White Geology Lab lab51/22/WebN F San Francisco Zaliczenie ocena ",
                "16:20 17:50 2h00m mgr Richard White Basic Mathematics Lab lab22/22/WebN F Chicago Zaliczenie ocena ",
                "18:00 19:30 2h00m mgr David Smith Civil Engineering Lab lab3/3/WebN F Nowy Jork Zaliczenie ocena ",
                "19:40 21:10 2h00m mgr Jacob Brown Advanced Mathematics Konw konw/2/WebN F Toronto Zaliczenie ocena"
            };

            var result = PdfParser.ExtractLessonStringsFromDayStringItem(dayStringItem);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DayStringsToDayItemsTest()
        {
            var dayStringItems = new List<string>
            {
                "Data Zajęć: 2019-10-04 piątek 17:30 19:00 2h00m dr Name Surname Advanced Math Wyk W/1/Web F Toronto Egzamin 19:15 20:45 2h00m doc. dr John Smiths Modern History of Poland Wyk W/1/Web F Praga Egzamin",
                "Data Zajęć: 2019-10-05 sobota 8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena 11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin",
                "Data Zajęć: 2019-10-06 niedziela 8:00 9:30 2h00m mgr Jack Green Wzorce projektowe Lab lab/WebN F Montreal Zaliczenie ocena 9:40 11:10 2h00m mgr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena 14:40 16:10 2h00m dr George White Geology Lab lab51/22/WebN F San Francisco Zaliczenie ocena 16:20 17:50 2h00m mgr Richard White Basic Mathematics Lab lab22/22/WebN F Chicago Zaliczenie ocena 18:00 19:30 2h00m mgr David Smith Civil Engineering Lab lab3/3/WebN F Nowy Jork Zaliczenie ocena 19:40 21:10 2h00m mgr Jacob Brown Advanced Mathematics Konw konw/2/WebN F Toronto Zaliczenie ocena",
                "Data Zajęć: 2019-10-18 piątek 17:30 19:00 2h00m dr Jacob Brown Advanced Physics Wyk W/2/WebN F Toronto Egzamin 19:15 20:45 2h00m dr inż. Thomas Blue Geology Konw konw/2/WebN F Praga Zaliczenie ocena"
            };

            var expected = new List<Day>
            {
                new Day
                (
                    "Data Zajęć: 2019-10-04 piątek",
                    new List<string>
                    {
                        "17:30 19:00 2h00m dr Name Surname Advanced Math Wyk W/1/Web F Toronto Egzamin ",
                        "19:15 20:45 2h00m doc. dr John Smiths Modern History of Poland Wyk W/1/Web F Praga Egzamin"
                    }
                ),
                new Day
                (
                    "Data Zajęć: 2019-10-05 sobota",
                    new List<string>
                    {
                        "8:00 9:30 2h00m mgr Marry Smiths-Blue Chemistry Cw 12 Blue 2/IEN F Sztokholm Zaliczenie ocena ",
                        "11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin"
                    }
                ),
                new Day
                (
                    "Data Zajęć: 2019-10-06 niedziela",
                    new List<string>
                    {
                        "8:00 9:30 2h00m mgr Jack Green Wzorce projektowe Lab lab/WebN F Montreal Zaliczenie ocena ",
                        "9:40 11:10 2h00m mgr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ",
                        "14:40 16:10 2h00m dr George White Geology Lab lab51/22/WebN F San Francisco Zaliczenie ocena ",
                        "16:20 17:50 2h00m mgr Richard White Basic Mathematics Lab lab22/22/WebN F Chicago Zaliczenie ocena ",
                        "18:00 19:30 2h00m mgr David Smith Civil Engineering Lab lab3/3/WebN F Nowy Jork Zaliczenie ocena ",
                        "19:40 21:10 2h00m mgr Jacob Brown Advanced Mathematics Konw konw/2/WebN F Toronto Zaliczenie ocena"
                    }
                ),
                new Day
                (
                    "Data Zajęć: 2019-10-18 piątek",
                    new List<string>
                    {
                        "17:30 19:00 2h00m dr Jacob Brown Advanced Physics Wyk W/2/WebN F Toronto Egzamin ",
                        "19:15 20:45 2h00m dr inż. Thomas Blue Geology Konw konw/2/WebN F Praga Zaliczenie ocena"
                    })
            };

            var result = PdfParser.DayStringsToDayItems(dayStringItems);
            Assert.True(expected.SequenceEqual(result));
        }
    }
}