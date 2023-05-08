using System;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

namespace Emu8080XamarinForms
{
    public static class ImageToolkit
    {
        public static byte[] ToByteArray(this Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }
    }
}

