using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using EditPhotoApp.ViewModels;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml;
using Windows.Foundation;
using Microsoft.UI.Xaml.Media.Imaging;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using System;

namespace EditPhotoApp.Views.MainWindowComponents
{
    public sealed partial class ImageEditComponent : Page
    {
        public Image saveImage => mainImage; // 'mainImage' là tên của Image trong XAML.
        public Canvas drawingCanvas => DrawingCanvas; // 'DrawingCanvas' là tên của Canvas trong XAML.

        private WriteableBitmap originalImage;
        private WriteableBitmap currentImage;
        string filePath = @"/Assets/cat.jpg";
        public ImageEditComponent()
        {
            this.InitializeComponent();
            LoadOriginalImage();
        }
        private async void LoadOriginalImage()
        {
            var bitmapSource = mainImage.Source as BitmapImage;
            if (bitmapSource != null)
            {
                var streamReference = RandomAccessStreamReference.CreateFromUri(bitmapSource.UriSource);
                using (var stream = await streamReference.OpenReadAsync().AsTask())
                {
                    originalImage = new WriteableBitmap(1, 1);
                    await originalImage.SetSourceAsync(stream);
                }
            }
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (originalImage != null)
            {
                mainImage.Source = originalImage;
                currentImage = originalImage;
            }
        }
        private void scrollImageTarget_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
            int delta = e.GetCurrentPoint(scrollImageTarget).Properties.MouseWheelDelta;

            double zoomFactor = delta > 0 ? 1.1 : 0.9;

            double currentWidth = mainImage.ActualWidth;
            double currentHeight = mainImage.ActualHeight;

            Point pointerPosition = e.GetCurrentPoint(mainImage).Position;

            double imageMouseX = pointerPosition.X;
            double imageMouseY = pointerPosition.Y;

            mainImage.Width = currentWidth * zoomFactor;
            mainImage.Height = currentHeight * zoomFactor;

            double newImageMouseX = imageMouseX * (mainImage.Width / currentWidth);
            double newImageMouseY = imageMouseY * (mainImage.Height / currentHeight);

            double offsetX = newImageMouseX - imageMouseX;
            double offsetY = newImageMouseY - imageMouseY;

            scrollImageTarget.ChangeView(scrollImageTarget.HorizontalOffset + offsetX, scrollImageTarget.VerticalOffset + offsetY, null);

            e.Handled = true;
        }
        private double rotationAngle = 0;

        private void RotateButton_Click(object sender, RoutedEventArgs e)
        {
            rotationAngle += 90;

            if (rotationAngle >= 360)
            {
                rotationAngle = 0;
            }

            var centerX = mainImage.ActualWidth / 2;
            var centerY = mainImage.ActualHeight / 2;

            var rotateTransform = new RotateTransform
            {
                Angle = rotationAngle,
                CenterX = centerX,
                CenterY = centerY
            };

            mainImage.RenderTransform = rotateTransform;


        }

        private bool isFlippedHorizontal = false;
        private bool isFlippedVertical = false;

        public event PropertyChangedEventHandler PropertyChanged;

        private void FlipHorizontalButton_Click(object sender, RoutedEventArgs e)
        {
            isFlippedHorizontal = !isFlippedHorizontal;

            mainImage.RenderTransform = new ScaleTransform
            {
                ScaleX = isFlippedHorizontal ? -1 : 1,
                CenterX = mainImage.ActualWidth / 2,
                CenterY = mainImage.ActualHeight / 2
            };
        }

        private void FlipVerticalButton_Click(object sender, RoutedEventArgs e)
        {
            isFlippedVertical = !isFlippedVertical;

            mainImage.RenderTransform = new ScaleTransform
            {
                ScaleY = isFlippedVertical ? -1 : 1,
                CenterX = mainImage.ActualWidth / 2,
                CenterY = mainImage.ActualHeight / 2
            };
        }

        private async void Crop16x9Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TopBarComponent.ImageFilePath))
            {
                var bitmapImage = new BitmapImage();

                // Gán UriSource cho BitmapImage
                bitmapImage.UriSource = new Uri(TopBarComponent.ImageFilePath);

                // Gán BitmapImage cho saveImage.Source
                saveImage.Source = bitmapImage;
            }
            var bitmapSource = saveImage.Source as BitmapImage;
            if (bitmapSource == null) return;
            //if (TopBarComponent.ImageFilePath != null)
            //{
            //    Uri filePath = new Uri(TopBarComponent.ImageFilePath);

            //    BitmapImage bitmapImage = new BitmapImage();
            //    bitmapImage.UriSource = filePath;

            //    mainImage.Source = bitmapImage;
            //    bitmapSource = mainImage.Source as BitmapImage;
            //}
            var streamReference = RandomAccessStreamReference.CreateFromUri(bitmapSource.UriSource);
            using (var stream = await streamReference.OpenReadAsync().AsTask())
            {
                var decoder = await BitmapDecoder.CreateAsync(stream);
                uint width = decoder.PixelWidth;
                uint height = decoder.PixelHeight;

                uint targetWidth, targetHeight;
                if (width / 16.0 > height / 9.0)
                {
                    targetHeight = height;
                    targetWidth = (uint)(height * 16 / 9);
                }
                else
                {
                    targetWidth = width;
                    targetHeight = (uint)(width * 9 / 16);
                }

                var cropRect = new BitmapBounds
                {
                    X = (width - targetWidth) / 2,
                    Y = (height - targetHeight) / 2,
                    Width = targetWidth,
                    Height = targetHeight
                };

                var transform = new BitmapTransform { Bounds = cropRect };
                var pixelProvider = await decoder.GetPixelDataAsync(
                    decoder.BitmapPixelFormat,
                    decoder.BitmapAlphaMode,
                    transform,
                    ExifOrientationMode.IgnoreExifOrientation,
                    ColorManagementMode.DoNotColorManage);

                var croppedStream = new InMemoryRandomAccessStream();
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, croppedStream);
                encoder.SetPixelData(
                    decoder.BitmapPixelFormat,
                    decoder.BitmapAlphaMode,
                    targetWidth, targetHeight,
                    decoder.DpiX, decoder.DpiY,
                    pixelProvider.DetachPixelData()
                );
                await encoder.FlushAsync();

                var croppedBitmap = new WriteableBitmap((int)targetWidth, (int)targetHeight);
                croppedStream.Seek(0);
                await croppedBitmap.SetSourceAsync(croppedStream);

                mainImage.Source = croppedBitmap;
            }
        }

        private async void Crop4x3Button_Click(object sender, RoutedEventArgs e)
        {
            await CropImageWithAspectRatio(4, 3);
        }

        private async void Crop3x4Button_Click(object sender, RoutedEventArgs e)
        {
            await CropImageWithAspectRatio(3, 4);
        }

        private async void Crop1x1Button_Click(object sender, RoutedEventArgs e)
        {
            await CropImageWithAspectRatio(1, 1);
        }

        private async Task CropImageWithAspectRatio(double widthRatio, double heightRatio)
        {
            if (mainImage.Source == null)
            {
                return;
            }

            var bitmapSource = mainImage.Source as BitmapImage;
            if (bitmapSource == null) return;

            var streamReference = RandomAccessStreamReference.CreateFromUri(bitmapSource.UriSource);
            using (var stream = await streamReference.OpenReadAsync().AsTask())
            {
                var decoder = await BitmapDecoder.CreateAsync(stream);
                uint width = decoder.PixelWidth;
                uint height = decoder.PixelHeight;

                uint targetWidth, targetHeight;
                if (width / widthRatio > height / heightRatio)
                {
                    targetHeight = height;
                    targetWidth = (uint)(height * widthRatio / heightRatio);
                }
                else
                {
                    targetWidth = width;
                    targetHeight = (uint)(width * heightRatio / widthRatio);
                }

                var cropRect = new BitmapBounds
                {
                    X = (width - targetWidth) / 2,
                    Y = (height - targetHeight) / 2,
                    Width = targetWidth,
                    Height = targetHeight
                };

                var transform = new BitmapTransform { Bounds = cropRect };
                var pixelProvider = await decoder.GetPixelDataAsync(
                    decoder.BitmapPixelFormat,
                    decoder.BitmapAlphaMode,
                    transform,
                    ExifOrientationMode.IgnoreExifOrientation,
                    ColorManagementMode.DoNotColorManage);

                var croppedStream = new InMemoryRandomAccessStream();
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, croppedStream);
                encoder.SetPixelData(
                    decoder.BitmapPixelFormat,
                    decoder.BitmapAlphaMode,
                    targetWidth, targetHeight,
                    decoder.DpiX, decoder.DpiY,
                    pixelProvider.DetachPixelData()
                );
                await encoder.FlushAsync();

                var croppedBitmap = new WriteableBitmap((int)targetWidth, (int)targetHeight);
                croppedStream.Seek(0);
                await croppedBitmap.SetSourceAsync(croppedStream);

                mainImage.Source = croppedBitmap;
            }
        }


    }

}
