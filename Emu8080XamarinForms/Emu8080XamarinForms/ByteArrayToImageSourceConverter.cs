using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Xamarin.Forms;

namespace Emu8080XamarinForms
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource retSource = null;
            if (value != null)
            {
                byte[] imageAsBytes = (byte[])value;
                Array.Reverse(imageAsBytes);
                GCHandle handle = GCHandle.Alloc(imageAsBytes, GCHandleType.Pinned);
                IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(imageAsBytes, 0);

                Bitmap bmap = new Bitmap(256, 224, 32,  System.Drawing.Imaging.PixelFormat.Format1bppIndexed,ptr);
                handle.Free();
                bmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                bmap = new Bitmap(bmap, 1024, 896);


                //byte[] imageAsBytes = (byte[])value;
                var stream = new MemoryStream(ImageToolkit.ToByteArray(bmap, ImageFormat.Bmp));
                retSource = ImageSource.FromStream(() => stream);
            }
            return retSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}

