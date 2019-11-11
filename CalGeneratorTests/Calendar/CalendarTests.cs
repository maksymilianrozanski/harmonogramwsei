using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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
        public void GenerateCalendarTest()
        {
            var result = CalendarGenerator.Calendar.Calendar.GenerateCalendar(ExampleRawInput);
            var exampleResult =
                "BEGIN:VCALENDAR\r\nVERSION:2.0\r\nPRODID:Schedule_generated_with_itext7\r\nCALSCALE:GREGORIAN\r\nBEGIN:VEVENT\r\nDTSTAMP:20191111T134700\r\nDTSTART:20191004T173000\r\nDTEND:20191004T190000\r\nSUMMARY:Wyk Advanced Math\r\nDESCRIPTION:W/1/Web F Toronto dr Name Surname\r\nLOCATION:św. Filipa 17\r\nUID:13@1573476438461\r\nEND:VEVENT\r\nBEGIN:VEVENT\r\nDTSTAMP:20191111T134700\r\nDTSTART:20191004T191500\r\nDTEND:20191004T204500\r\nSUMMARY:Wyk Modern History of Poland\r\nDESCRIPTION:W/1/Web F Praga doc. dr John Smiths\r\nLOCATION:św. Filipa 17\r\nUID:13@1573476438462\r\nEND:VEVENT\r\nBEGIN:VEVENT\r\nDTSTAMP:20191111T134700\r\nDTSTART:20191005T080000\r\nDTEND:20191005T093000\r\nSUMMARY:Cw Chemistry\r\nDESCRIPTION:12 Blue 2/IEN F Sztokholm mgr Marry Smiths-Blue\r\nLOCATION:św. Filipa 17\r\nUID:13@1573476438463\r\nEND:VEVENT\r\nBEGIN:VEVENT\r\nDTSTAMP:20191111T134700\r\nDTSTART:20191005T112000\r\nDTEND:20191005T143000\r\nSUMMARY:Wyk Physics\r\nDESCRIPTION:W/2/W F Toronto doc. dr John Black\r\nLOCATION:św. Filipa 17\r\nUID:13@1573476438464\r\nEND:VEVENT\r\nBEGIN:VEVENT\r\nDTSTAMP:20191111T134700\r\nDTSTART:20191006T080000\r\nDTEND:20191006T093000\r\nSUMMARY:Lab Wzorce projektowe\r\nDESCRIPTION:lab/WebN F Montreal mgr Jack Green\r\nLOCATION:św. Filipa 17\r\nUID:13@1573476438465\r\nEND:VEVENT\r\nBEGIN:VEVENT\r\nDTSTAMP:20191111T134700\r\nDTSTART:20191006T094000\r\nDTEND:20191006T111000\r\nSUMMARY:Lab Biology and Geography\r\nDESCRIPTION:lab15/2/WebN F Los Angeles mgr inż. Thomas Orange\r\nLOCATION:św. Filipa 17\r\nUID:13@1573476438466\r\nEND:VEVENT\r\nBEGIN:VEVENT\r\nDTSTAMP:20191111T134700\r\nDTSTART:20191006T144000\r\nDTEND:20191006T161000\r\nSUMMARY:Lab Geology\r\nDESCRIPTION:lab51/22/WebN F San Francisco dr George White\r\nLOCATION:św. Filipa 17\r\nUID:13@1573476438467\r\nEND:VEVENT\r\nBEGIN:VEVENT\r\nDTSTAMP:20191111T134700\r\nDTSTART:20191006T162000\r\nDTEND:20191006T175000\r\nSUMMARY:Lab Basic Mathematics\r\nDESCRIPTION:lab22/22/WebN F Chicago mgr Richard White\r\nLOCATION:św. Filipa 17\r\nUID:13@1573476438468\r\nEND:VEVENT\r\nBEGIN:VEVENT\r\nDTSTAMP:20191111T134700\r\nDTSTART:20191006T180000\r\nDTEND:20191006T193000\r\nSUMMARY:Lab Civil Engineering\r\nDESCRIPTION:lab3/3/WebN F Nowy Jork mgr David Smith\r\nLOCATION:św. Filipa 17\r\nUID:13@1573476438469\r\nEND:VEVENT\r\nBEGIN:VEVENT\r\nDTSTAMP:20191111T134700\r\nDTSTART:20191006T194000\r\nDTEND:20191006T211000\r\nSUMMARY:Konw Advanced Mathematics\r\nDESCRIPTION:konw/2/WebN F Toronto mgr Jacob Brown\r\nLOCATION:św. Filipa 17\r\nUID:13@1573476438470\r\nEND:VEVENT\r\nBEGIN:VEVENT\r\nDTSTAMP:20191111T134700\r\nDTSTART:20191018T173000\r\nDTEND:20191018T190000\r\nSUMMARY:Wyk Advanced Physics\r\nDESCRIPTION:W/2/WebN F Toronto dr Jacob Brown\r\nLOCATION:św. Filipa 17\r\nUID:13@1573476438471\r\nEND:VEVENT\r\nBEGIN:VEVENT\r\nDTSTAMP:20191111T134700\r\nDTSTART:20191018T191500\r\nDTEND:20191018T204500\r\nSUMMARY:Konw Geology\r\nDESCRIPTION:konw/2/WebN F Praga dr inż. Thomas Blue\r\nLOCATION:św. Filipa 17\r\nUID:13@1573476438472\r\nEND:VEVENT\r\nEND:VCALENDAR\r\n";
            var exampleResultFiltered = exampleResult.Split("\n").Select(
                line => line).Where(it => !it.StartsWith("DTSTAMP:") && !it.StartsWith("UID:"));
            var resultFiltered = result.Split("\n").Select(
                line => line).Where(it => !it.StartsWith("DTSTAMP:") && !it.StartsWith("UID:"));
            Assert.AreEqual(exampleResultFiltered, resultFiltered);
        }

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
            Assert.AreEqual(ParsingException.HeadersNotMatched + input.Split("\n")[0], exception.Message);
        }

        [Test]
        public void ValidateInputInvalidDateFormatTest()
        {
            var input = ExampleRawInput.Replace("2019-10-04", "2019:10:04");
            ExpectMatchingLineFailed(input, 1);
        }

        [Test]
        public void ValidateInputInvalidHourFormatTest()
        {
            var input = ExampleRawInput.Replace("17:30", "17:30:00");
            ExpectMatchingLineFailed(input, 2);
        }

        [Test]
        public void ValidateInputInvalidLecturersTitleTest()
        {
            var input = ExampleRawInput.Replace("dr", "unknown title");
            ExpectMatchingLineFailed(input, 2);
        }

        [Test]
        public void ValidateInputInvalidLessonTypeTest()
        {
            var input = ExampleRawInput.Replace("Wyk", "unknown");
            ExpectMatchingLineFailed(input, 2);
        }

        private void ExpectMatchingLineFailed(string input, int lineThrowingException)
        {
            var exception = Assert.Throws<ParsingException>(() =>
            {
                CalendarGenerator.Calendar.Calendar.ValidateInput(input);
            });
            Assert.AreEqual(ParsingException.MatchingLineToPatternFailed
                            + input.Split("\n")[lineThrowingException], exception.Message);
        }
    }
}