using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dictionary
{
    public class Word
    {
        public string Syntax { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImageBase64 { get; set; }

        // Constructor that accepts ImageSource. This will immediately convert the ImageSource to a base64 string.
        public Word(string syntax, string category, string description, ImageSource imageSource)
        {
            Syntax = syntax;
            Category = category;
            Description = description;
            ImageBase64 = ConvertImageSourceToBase64(imageSource);
        }

        // Converts an ImageSource to a base64 string
        private string ConvertImageSourceToBase64(ImageSource imageSource)
        {
            if (imageSource is BitmapSource bitmapSource)
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                using (MemoryStream stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    byte[] bytes = stream.ToArray();
                    return Convert.ToBase64String(bytes);
                }
            }
            return null;
        }

        // Optionally, add a method to convert a base64 string back to an ImageSource for use in your application
        public static ImageSource ConvertBase64ToImageSource(string base64String)
        {
            if (string.IsNullOrEmpty(base64String)) return null;

            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }
        }
    }

}
