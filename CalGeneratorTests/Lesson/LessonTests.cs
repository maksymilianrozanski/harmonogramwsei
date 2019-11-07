using System;
using CalendarGenerator.Calendar;
using NUnit.Framework;

namespace CalGeneratorTests.Lesson
{
    public class LessonTests
    {
        [Test]
        public void LessonTextConstructorTest()
        {
            var inputDateString = "Data Zajęć: 2019-10-05 sobota";
            var lessonInputString = "11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin";
            var result = new CalendarGenerator.Lesson.Lesson(inputDateString, lessonInputString);
            Assert.AreEqual("doc. dr John Black", result.LecturersTitleAndName);
            Assert.AreEqual("Physics", result.LessonTitle);
            Assert.AreEqual("Wyk", result.LessonType);
            Assert.AreEqual("W/2/W F Toronto", result.LessonCodeAndClassRoom);
            Assert.AreEqual(new DateTime(2019, 10, 5, 11, 20, 0), result.StartDateTime);
            Assert.AreEqual(new DateTime(2019, 10, 5, 14, 30, 0), result.EndDateTime);
        }

        [Test]
        public void ToCalendarEventTest()
        {
            var inputDateString = "Data Zajęć: 2019-10-05 sobota";
            var lessonInputString = "11:20 14:30 4h00m doc. dr John Black Physics Wyk W/2/W F Toronto Egzamin";
            var lessonText = new CalendarGenerator.Lesson.Lesson(inputDateString, lessonInputString);

            var expected = new CalendarEvent
            {
                Start = lessonText.StartDateTime,
                End = lessonText.EndDateTime,
                Summary = lessonText.LessonType + " " + lessonText.LessonTitle,
                Description = lessonText.LessonCodeAndClassRoom + " " + lessonText.LecturersTitleAndName,
                Location = CalendarGenerator.Lesson.Lesson.Location
            };
            var result = lessonText.ToCalendarEvent();
            Assert.AreEqual(expected.Start, result.Start);
            Assert.AreEqual(expected.End, result.End);
            Assert.AreEqual(expected.Summary, result.Summary);
            Assert.AreEqual(expected.Description, result.Description);
            Assert.AreEqual(expected.Location, result.Location);
        }

        [Test]
        public void ExtractDateTest()
        {
            var input = "Data Zajęć: 2019-10-04 piątek";
            var expected = "2019-10-04";
            var result = CalendarGenerator.Lesson.Lesson.ExtractDate(input);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLessonHoursTest()
        {
            var input =
                "9:40 11:10 2h00m mgr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expectedStart = "9:40";
            var expectedEnd = "11:10";
            CalendarGenerator.Lesson.Lesson.ExtractHours(input, out var startResult, out var endResult);
            Assert.AreEqual(expectedStart, startResult);
            Assert.AreEqual(expectedEnd, endResult);
        }

        [Test]
        public void ParseToDateTimeAMTest()
        {
            var inputDate = "2019-10-04";
            var inputHour = "9:40";
            var expected = new DateTime(2019, 10, 4, 9, 40, 0);
            var result = CalendarGenerator.Lesson.Lesson.ParseToDateTime(inputDate, inputHour);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ParseToDateTimePMTest()
        {
            var inputDate = "2019-10-04";
            var inputHour = "19:40";
            var expected = new DateTime(2019, 10, 4, 19, 40, 0);
            var result = CalendarGenerator.Lesson.Lesson.ParseToDateTime(inputDate, inputHour);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleProfZwDrHabTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m prof. zw. dr hab. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "prof. zw. dr hab.";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleProfWseiDrHabTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m prof. WSEI dr hab. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "prof. WSEI dr hab.";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleProfNadzwDrTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m prof. nadzw. dr Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "prof. nadzw. dr";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleProfDrHabInzTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m prof. dr hab. inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "prof. dr hab. inż.";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleProfDrHabTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m prof. dr hab. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "prof. dr hab.";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleMecenasTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m mecenas Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "mecenas";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleMgrInzTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m mgr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "mgr inż.";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleMgrTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m mgr Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "mgr";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleDrInzTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr inż.";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleDrTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleInzTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "inż.";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleDrHabInzTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr hab. inż.";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleDocDrTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m doc. dr Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "doc. dr";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleDrHabTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr hab.";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleMbaTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m MBA Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "MBA";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturerSingleSurnameTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomasżąęźł Oórangeąę Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr hab. Thomasżąęźł Oórangeąę";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturer(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturerDoubleSurnameTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange-Brown Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr hab. Thomas Orange-Brown";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturer(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturerDoubleSurnameWithSpacesTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange - Brown Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr hab. Thomas Orange - Brown";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLecturer(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLessonTypeLabTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange - Brown Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var lecturersTitleAndName = "dr hab. Thomas Orange - Brown";
            var expected = "Lab";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLessonType(lessonInput, lecturersTitleAndName);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLessonTypeCwTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange - Brown Biology and Geography Cw lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var lecturersTitleAndName = "dr hab. Thomas Orange - Brown";
            var expected = "Cw";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLessonType(lessonInput, lecturersTitleAndName);
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void ExtractLessonTypeKonwTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange - Brown Biology and Geography Konw lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var lecturersTitleAndName = "dr hab. Thomas Orange - Brown";
            var expected = "Konw";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLessonType(lessonInput, lecturersTitleAndName);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLessonTypeWykTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange - Brown Biology and Geography Wyk lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var lecturersTitleAndName = "dr hab. Thomas Orange - Brown";
            var expected = "Wyk";
            var result = CalendarGenerator.Lesson.Lesson.ExtractLessonType(lessonInput, lecturersTitleAndName);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLessonName()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange - Brown Biology and Geography Wyk lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var lecturersTitleAndName = "dr hab. Thomas Orange - Brown";
            var lessonType = "Wyk";
            var expected = "Biology and Geography";
            var result =
                CalendarGenerator.Lesson.Lesson.ExtractLessonName(lessonInput, lecturersTitleAndName, lessonType);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLessonCodeAndClassRoomSimpleTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange - Brown Biology and Geography Wyk lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var lessonName = "Biology and Geography";
            var lessonType = "Wyk";
            var expected = "lab15/2/WebN F Los Angeles";
            var result =
                CalendarGenerator.Lesson.Lesson.ExtractLessonCodeAndClassRoom(lessonInput, lessonName, lessonType);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLessonCodeAndClassRoomCodeWithSpacesTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange - Brown Biology and Geography Wyk 90 w lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var lessonName = "Biology and Geography";
            var lessonType = "Wyk";
            var expected = "90 w lab15/2/WebN F Los Angeles";
            var result =
                CalendarGenerator.Lesson.Lesson.ExtractLessonCodeAndClassRoom(lessonInput, lessonName, lessonType);
            Assert.AreEqual(expected, result);
        }
    }
}