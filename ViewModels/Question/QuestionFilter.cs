using NTKServer.Internal;

namespace NTKServer.ViewModels.Question
{
    public class QuestionFilter
    {
        /// <summary> question_category_fk: 问题类别 </summary>
        [Where("=", "question_category_fk")]
        public int? question_category_fk { get; set; }

        /// <summary> lang: 语系 </summary>
        [Where("=", "lang")]
        public string? lang { get; set; }

        /// <summary> question: 题目 </summary>
        [Where("LIKE", "question")]
        public string? question { get; set; }

    }
}
