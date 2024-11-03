using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using EditPhotoApp.ViewModels;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml;
using Windows.Foundation;

namespace EditPhotoApp.Views.MainWindowComponents
{
    public sealed partial class ImageEditComponent : Page
    {
        public Image saveImage => mainImage; // 'mainImage' là tên của Image trong XAML.
        public Canvas drawingCanvas => DrawingCanvas; // 'DrawingCanvas' là tên của Canvas trong XAML.

        public ImageEditComponent()
        {
            this.InitializeComponent();
        }

        // Các phương thức sự kiện sẽ được chuyển tiếp tới ViewModel
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


    }

}
