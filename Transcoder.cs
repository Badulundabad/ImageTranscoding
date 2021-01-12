using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ImageTranscoding
{
    public class Transcoder
    {
        public static BitmapSource BitmapSourceFromBitmap(Bitmap bitmap)
        {
            BitmapSource bitmapSource = null;
            var hBitmap = bitmap.GetHbitmap();

            try
            {
                bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Win32Exception)
            {
                bitmapSource = null;
            }

            return bitmapSource;
        }
        public static Bitmap BitmapFromBitmapImage(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                Bitmap bitmap = null;

                try
                {
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                    enc.Save(outStream);
                    bitmap = new Bitmap(outStream);
                }
                catch
                {
                    bitmap.Dispose();
                    bitmap = null;
                }

                return bitmap;
            }
        }
    }
}
