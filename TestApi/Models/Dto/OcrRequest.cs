namespace TestApi.Models.Dto
{
    public class OcrRequest
    {
        public int ImageXCoordinate { get; set; }
        public int ImageYCoordinate { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public string ProcessName { get; set; }
        public bool InvertColour { get; set; } = true;
    }
}