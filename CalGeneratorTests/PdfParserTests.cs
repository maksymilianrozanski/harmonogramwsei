using CalendarGenerator.PdfParse;
using NUnit.Framework;

namespace CalGeneratorTests
{
    public class PdfParserTests
    {
        [Test]
        public void RawTextToWordsTest()
        {
            var expectedOutput = new[] {"Some", "example", "input", "text"};
            var input = expectedOutput[0] + " " + expectedOutput[1] + "\n" + expectedOutput[2] + " " +
                        expectedOutput[3];
            var result = PdfParser.RawTextToWords(input);
            Assert.AreEqual(expectedOutput, result);
        }
    }
}