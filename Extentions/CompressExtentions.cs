using SkiaSharp;

namespace DvD_Api.Extentions
{
    public static class CompressExtentions
    {
        public static string ConvertToBase64(this Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            return Convert.ToBase64String(bytes);
        }

        public static SKBitmap GetBitmap(this string base64String)
        {
            SKBitmap sBitmap;
            byte[] byteBuffer = Convert.FromBase64String(base64String);
            using (var memoryStream = new MemoryStream(byteBuffer))
            {
                sBitmap = SKBitmap.Decode(memoryStream);
            }

            return sBitmap;
        }
    }
}
