using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public static class ImageHelper
{
    public static Image CreateThumbnail(Image original, int maxSize)
    {
        int newWidth, newHeight;
        if (original.Width > original.Height)
        {
            newWidth = maxSize;
            newHeight = (int)(original.Height * ((float)maxSize / original.Width));
        }
        else
        {
            newHeight = maxSize;
            newWidth = (int)(original.Width * ((float)maxSize / original.Height));
        }

        Bitmap thumbnail = new Bitmap(newWidth, newHeight);
        using (Graphics g = Graphics.FromImage(thumbnail))
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(original, 0, 0, newWidth, newHeight);
        }

        return thumbnail;
    }

    public static byte[] ImageToByteArray(Image image, long quality = 80L)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            image.Save(ms, jpegCodec, encoderParams);

            return ms.ToArray();
        }
    }

    private static ImageCodecInfo GetEncoderInfo(string mimeType)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
        foreach (var codec in codecs)
        {
            if (codec.MimeType == mimeType)
                return codec;
        }
        return null;
    }
}
