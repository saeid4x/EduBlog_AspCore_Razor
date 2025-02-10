namespace SokanAcademy.ViewModels
{
    public class ChapterViewModel
    {
        public int ChapterId { get; set; } = 0;
        public string Title { get; set; } = "";
        public string Duration { get; set; } = "";
        public List<ChapterItemViewModel> ChapterItems { get; set; } = new List<ChapterItemViewModel>();
    }
}
