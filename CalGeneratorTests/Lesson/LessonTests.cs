using CalendarGenerator.Lesson;
using NUnit.Framework;

namespace CalGeneratorTests.Lesson
{
    public class LessonTests
    {
        [Test]
        public void ExtractDateTest()
        {
            var input = "Data Zajęć: 2019-10-04 piątek";
            var expected = "2019-10-04";
            var result = LessonText.ExtractDate(input);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLessonHoursTest()
        {
            var input =
                "9:40 11:10 2h00m mgr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expectedStart = "9:40";
            var expectedEnd = "11:10";
            LessonText.ExtractHours(input, out var startResult, out var endResult);
            Assert.AreEqual(expectedStart, startResult);
            Assert.AreEqual(expectedEnd, endResult);
        }

        [Test]
        public void ExtractLecturersTitleProfZwDrHabTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m prof. zw. dr hab. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "prof. zw. dr hab.";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturersTitleProfWseiDrHabTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m prof. WSEI dr hab. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "prof. WSEI dr hab.";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturersTitleProfNadzwDrTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m prof. nadzw. dr Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "prof. nadzw. dr";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturersTitleProfDrHabInzTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m prof. dr hab. inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "prof. dr hab. inż.";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturersTitleProfDrHabTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m prof. dr hab. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "prof. dr hab.";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturersTitleMecenasTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m mecenas Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "mecenas";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturersTitleMgrInzTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m mgr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "mgr inż.";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturersTitleMgrTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m mgr Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "mgr";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturersTitleDrInzTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr inż.";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturersTitleDrTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturersTitleInzTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "inż.";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturersTitleDrHabInzTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. inż. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr hab. inż.";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturersTitleDocDrTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m doc. dr Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "doc. dr";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturersTitleDrHabTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr hab.";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturersTitleMbaTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m MBA Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "MBA";
            var result = LessonText.ExtractLecturersTitle(lessonInput);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ExtractLecturerSingleSurnameTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr hab. Thomas Orange";
            var result = LessonText.ExtractLecturer(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturerDoubleSurnameTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange-Brown Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr hab. Thomas Orange-Brown";
            var result = LessonText.ExtractLecturer(lessonInput);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ExtractLecturerDoubleSurnameWithSpacesTest()
        {
            var lessonInput =
                "9:40 11:10 2h00m dr hab. Thomas Orange - Brown Biology and Geography Lab lab15/2/WebN F Los Angeles Zaliczenie ocena ";
            var expected = "dr hab. Thomas Orange - Brown";
            var result = LessonText.ExtractLecturer(lessonInput);
            Assert.AreEqual(expected, result);
        }
    }
}