using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace Photo.Converters
{
    public class BitmapToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Bitmap bitmap)
            {
                return ConvertBitmapToBitmapImage(bitmap);
            }

            throw new ArgumentException("Value must be a Bitmap.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private BitmapImage ConvertBitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the bitmap to a memory stream
                bitmap.Save(memoryStream, ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Create a decoder from the stream
                var randomAccessStream = memoryStream.AsRandomAccessStream();
                var decoder = BitmapDecoder.CreateAsync(randomAccessStream).AsTask().Result;

                // Convert to a SoftwareBitmap
                SoftwareBitmap softwareBitmap = decoder.GetSoftwareBitmapAsync().AsTask().Result;

                // Convert SoftwareBitmap to BitmapImage
                BitmapImage bitmapImage = new BitmapImage();
                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {
                    // Encode SoftwareBitmap to a stream (e.g., PNG)
                    var encoder = BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream).AsTask().Result;
                    encoder.SetSoftwareBitmap(softwareBitmap);
                    encoder.FlushAsync().AsTask().Wait();

                    // Set the stream source to BitmapImage
                    stream.Seek(0);
                    bitmapImage.SetSource(stream);
                }

                return bitmapImage;
            }
        }
    }
}
