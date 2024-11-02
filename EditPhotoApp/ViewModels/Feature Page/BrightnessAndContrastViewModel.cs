// ViewModels/PersonViewModel.cs
using EditPhotoApp.Models;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace EditPhotoApp.ViewModels
{
    public class BrightnessAndContrastViewModel : INotifyPropertyChanged
    {
        private BrightnessAndContrast _brightnessAndContrast;

        public BrightnessAndContrastViewModel()
        {
            _brightnessAndContrast = new BrightnessAndContrast { brightness = 0, contrast = 100 };
        }

        public float Brightness
        {
            get => _brightnessAndContrast.brightness;
            set
            {
                if (_brightnessAndContrast.brightness != value)
                {
                    _brightnessAndContrast.brightness = value;
                    OnPropertyChanged(nameof(Brightness));
                }
            }
        }

        public float Contrast
        {
            get => _brightnessAndContrast.contrast;
            set
            {
                if (_brightnessAndContrast.contrast != value)
                {
                    _brightnessAndContrast.contrast = value;
                    OnPropertyChanged(nameof(Contrast));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private SoftwareBitmap originalBitmap;



        public static Bitmap AdjustBrightnessContrast(Bitmap image, float brightnessValue, float contrastValue)
        {
            float brightness = (brightnessValue / 100.0f);
            float contrast = contrastValue / 100.0f;
            var bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);

            using (var g = Graphics.FromImage(bitmap))
            using (var attributes = new ImageAttributes())
            {
                float[][] matrix = {
            new float[] { contrast, 0, 0, 0, 0},
            new float[] {0, contrast, 0, 0, 0},
            new float[] {0, 0, contrast, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {brightness, brightness, brightness, 1, 1}
        };

                ColorMatrix colorMatrix = new ColorMatrix(matrix);
                attributes.SetColorMatrix(colorMatrix);
                g.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, attributes);
                return bitmap;
            }
        }

        private Bitmap SoftwareBitmapToBitmap(Windows.Graphics.Imaging.SoftwareBitmap softwareBitmap)
        {
            // Convert SoftwareBitmap to Bitmap
            using (var memoryStream = new InMemoryRandomAccessStream())
            {
                // Encode the SoftwareBitmap into a stream (for example, as a PNG image)
                var bitmapEncoder = Windows.Graphics.Imaging.BitmapEncoder.CreateAsync(
                    Windows.Graphics.Imaging.BitmapEncoder.PngEncoderId, memoryStream).AsTask().Result;

                bitmapEncoder.SetSoftwareBitmap(softwareBitmap);
                bitmapEncoder.FlushAsync().AsTask().Wait();

                // Convert the stream to a byte array
                byte[] bytes;
                using (var dataReader = new DataReader(memoryStream.GetInputStreamAt(0)))
                {
                    bytes = new byte[memoryStream.Size];
                    dataReader.LoadAsync((uint)memoryStream.Size).AsTask().Wait();
                    dataReader.ReadBytes(bytes);
                }

                // Convert byte array to Bitmap
                using (var bitmapStream = new MemoryStream(bytes))
                {
                    return new Bitmap(bitmapStream);
                }
            }
        }

        private Bitmap originalImage;
        public async void UpdateImage(float brightness,  float contrast)
        {
            if (originalImage == null)
            {
                // Log or handle the error if needed
                return;
            }
            // Get current slider values
           

            // Adjust brightness and contrast
            Bitmap adjustedImage = AdjustBrightnessContrast(originalImage, brightness, contrast);

            // Convert Bitmap to SoftwareBitmap
            SoftwareBitmap softwareBitmap = ConvertToSoftwareBitmap(adjustedImage);

            // Convert SoftwareBitmap to BitmapImage
            BitmapImage bitmapImage = await ConvertSoftwareBitmapToBitmapImageAsync(softwareBitmap);

            // Display in Image control
            SelectedImage.Source = bitmapImage;
        }

        private SoftwareBitmap ConvertToSoftwareBitmap(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the bitmap to a memory stream
                bitmap.Save(memoryStream, ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Create a decoder from the stream
                BitmapDecoder decoder = BitmapDecoder.CreateAsync(memoryStream.AsRandomAccessStream()).AsTask().Result;

                // Convert to a SoftwareBitmap
                return decoder.GetSoftwareBitmapAsync().AsTask().Result;
            }
        }

        private async Task<BitmapImage> ConvertSoftwareBitmapToBitmapImageAsync(SoftwareBitmap softwareBitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();

            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                // Encode SoftwareBitmap to a stream (e.g., PNG)
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                encoder.SetSoftwareBitmap(softwareBitmap);
                await encoder.FlushAsync();

                // Set the stream source to BitmapImage
                stream.Seek(0);
                await bitmapImage.SetSourceAsync(stream);
            }

            return bitmapImage;
        }
    }
}
