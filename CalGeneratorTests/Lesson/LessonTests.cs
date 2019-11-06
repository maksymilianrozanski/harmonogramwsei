using CalendarGenerator.Lesson;
using NUnit.Framework;

namespace CalGeneratorTests.Lesson
{
    public class LessonTests
    {
        [Test]
        public void ExtractDateTest()
        {
            var input = "Data Zajęć: 2019-10-04 piątek 17:30 19:00";
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
    }
}