using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FT.Client.Extensions
{
    public static class IconExtension
    {
        public static ImageSource ToImageSource(this Icon icon)
        {
            if (icon == null)
                return null;

            ImageSource imageSource;

            using (Bitmap bmp = icon.ToBitmap())
            {
                var stream = new MemoryStream();
                bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                imageSource = BitmapFrame.Create(stream);
            }
            return imageSource;
        }
    }
}
