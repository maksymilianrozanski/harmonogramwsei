using System.Collections.Generic;

namespace CalendarGenerator.PdfParse
{
    public struct Day
    {
        public string Date { get; set; }
        public List<string> LessonStrings { get; set; }
    }
}