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
    }
}