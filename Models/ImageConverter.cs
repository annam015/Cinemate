namespace Cinemate.Models
{
    namespace Cinemate.Models
    {
        public class ImageConverter
        {
            public static string ImageToBase64(Stream imageStream)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imageStream.CopyTo(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
            public static ImageSource Base64ToImage(string base64String)
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                Stream stream = new MemoryStream(imageBytes);
                return ImageSource.FromStream(() => stream);
            }
        }
    }
}
