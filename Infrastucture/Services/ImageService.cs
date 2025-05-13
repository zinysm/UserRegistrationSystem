using System.Drawing;
using System.Drawing.Imaging;

namespace UserRegistrations.Infrastucture.Services;

public class ImageService
{
    private const int MaxSizeBytes = 2 * 1024 * 1024;
    private const int MaxWidth = 200;
    private const int MaxHeight = 200;

    public async Task<byte[]> ProcessImageAsync(Stream imageStream, string fileName)
    {
        if (!fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) &&
            !fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("Only .jpg or .jpeg files are allowed.");
        }

        if (imageStream.Length > MaxSizeBytes)
        {
            throw new Exception("Image size must not exceed 2 MB.");
        }

        using var image = System.Drawing.Image.FromStream(imageStream);
        var resized = ResizeImage(image, MaxWidth, MaxHeight);

        using var ms = new MemoryStream();
        resized.Save(ms, ImageFormat.Jpeg);
        return ms.ToArray();
    }

    private System.Drawing.Image ResizeImage(System.Drawing.Image image, int maxWidth, int maxHeight)
    {
        var ratioX = (double)maxWidth / image.Width;
        var ratioY = (double)maxHeight / image.Height;
        var ratio = Math.Min(ratioX, ratioY);

        var newWidth = (int)(image.Width * ratio);
        var newHeight = (int)(image.Height * ratio);

        var destRect = new Rectangle(0, 0, newWidth, newHeight);
        var destImage = new Bitmap(newWidth, newHeight);

        using (var graphics = Graphics.FromImage(destImage))
        {
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
        }

        return destImage;
    }
}

