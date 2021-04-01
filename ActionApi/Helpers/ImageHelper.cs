using CSharpFunctionalExtensions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Tesseract;
using ActionApi.Models.Dto;

namespace ActionApi.Helpers
{
    public class ImageHelper
    {
        public static Result<string> ExtractWordFromImage(OcrRequest request)
        {
            Image myImage = WindowsHelper.CaptureWindowByName(request.ProcessName, new Rectangle(request.ImageXCoordinate, request.ImageYCoordinate, request.ImageWidth, request.ImageHeight));
            var imageAttr = new ImageAttributes();
            imageAttr.SetThreshold(0.75f);
            var bm = new Bitmap(myImage.Width, myImage.Height);

            using (var gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(myImage, new[] {
                        new Point(0, 0),
                        new Point(myImage.Width, 0),
                        new Point(0, myImage.Height),
                    }, new Rectangle(0, 0, myImage.Width, myImage.Height),
                    GraphicsUnit.Pixel, imageAttr);
            }

            if (request.InvertColour)
            {
                for (var h = 0; h < bm.Height; h++)
                {
                    for (var w = 0; w < bm.Width; w++)
                    {
                        var color = bm.GetPixel(w, h);
                        bm.SetPixel(w, h, Color.FromArgb(255, (255 - color.R), (255 - color.G), (255 - color.B)));
                    }
                }
            }

            var fp = $"{Path.GetTempPath()}{Guid.NewGuid()}.jpg";

            bm.Save(fp);
            var word = new TesseractEngine($"{new System.IO.DirectoryInfo(Environment.CurrentDirectory).FullName}\\tessdata\\",
                                                "eng",
                                                EngineMode.Default).Process(Pix.LoadFromFile(@"D:\\tmp.jpg"))
                                                                   .GetText()
                                                                   .Replace("\n", " ");
            return Result.SuccessIf<string>(!string.IsNullOrWhiteSpace(word), word, $"No words readable, image saved in {fp}");
        }
    }
}