using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Models
{
    public class PaginatedList
    {
    }

    public class Blankword
    {
        public string correct { get; set; }
    }

    public class WroAnswer
    {
        public string wrong { get; set; }
    }

    public class Question
    {
        public int id { get; set; }
        public string type { get; set; }
        public bool example { get; set; }
        public string header { get; set; }
        public string body { get; set; }
        public List<Blankword> blankwords { get; set; }
        public string partialfeedback { get; set; }
        public string rightfeedback { get; set; }
        public string wrongfeedback { get; set; }
        public object co_answer { get; set; }
        public List<WroAnswer> wro_answer { get; set; }
        public string feedback_title { get; set; }
    }

    public class Report
    {
        public bool enabled { get; set; }
        public string header { get; set; }
        public string essay { get; set; }
        public int trueFalse { get; set; }
        public int singleChoiceTxt { get; set; }
        public int multiChoiceTxt { get; set; }
        public int singleChoiceImg { get; set; }
        public int multiChoiceImg { get; set; }
        public int fillBlanK { get; set; }
        public int fillDropDown { get; set; }
        public int fillByDragDrop { get; set; }
        public int math { get; set; }
    }

    public class QuizJson
    {
        public List<Question> questions { get; set; }
        public Report report { get; set; }
        public bool feedback { get; set; }
        public bool randomaize { get; set; }
    }

    public class co_answer
    {
        public string correct { get; set; }

        

    }
}
