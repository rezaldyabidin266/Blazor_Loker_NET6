namespace BlazorLoker2022.Resource.Quiz
{
    public class FormQuizz
    {
        public int detailJawabanId { get; set; }
        public int no { get; set; }
        public string pertanyaan { get; set; }
        public object jawaban { get; set; }
        public string bentukIsian { get; set; }
        public bool isRequired { get; set; }
        public List<FormIsianJawabanDetailResponse> formIsianJawabanDetailResponses { get; set; }
    }
}

