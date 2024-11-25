using Microsoft.UI.Xaml.Input;
using OpenCvSharp;
using Photo.ViewModels;
using System.Diagnostics;
using Point = Windows.Foundation.Point;

namespace Photo
{
    public sealed partial class MainWindow : Microsoft.UI.Xaml.Window
    {
        public MainWindow(object dataContext)
        {
            this.InitializeComponent();
            RootPanel.DataContext = dataContext;
        }
        private void scrollImageTarget_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
            // Lấy giá trị cuộn chuột
            int delta = e.GetCurrentPoint(scrollImageTarget).Properties.MouseWheelDelta;

            // Tính toán tỷ lệ zoom (phóng to hoặc thu nhỏ)
            double zoomFactor = delta > 0 ? 1.1 : 0.9;

            // Lấy kích thước hiện tại của Image
            double currentWidth = mainImage.ActualWidth;
            double currentHeight = mainImage.ActualHeight;

            // Vị trí con trỏ chuột trên Image
            Point pointerPosition = e.GetCurrentPoint(mainImage).Position;
            double imageMouseX = pointerPosition.X;
            double imageMouseY = pointerPosition.Y;

            // Cập nhật kích thước của Image
            mainImage.Width = currentWidth * zoomFactor;
            mainImage.Height = currentHeight * zoomFactor;

            // Tính toán vị trí mới của con trỏ trên Image
            double newImageMouseX = imageMouseX * (mainImage.Width / currentWidth);
            double newImageMouseY = imageMouseY * (mainImage.Height / currentHeight);

            // Tính toán sự thay đổi vị trí của ScrollViewer để đồng bộ hóa
            double offsetX = newImageMouseX - imageMouseX;
            double offsetY = newImageMouseY - imageMouseY;

            // Cập nhật lại tỷ lệ phóng to/thu nhỏ
            scaleTransform.ScaleX *= zoomFactor;
            scaleTransform.ScaleY *= zoomFactor;

            // Điều chỉnh vị trí của ScrollViewer để zoom vào vị trí con trỏ chuột
            scrollImageTarget.ChangeView(scrollImageTarget.HorizontalOffset + offsetX, scrollImageTarget.VerticalOffset + offsetY, null);

            e.Handled = true;
        }


        private void mainImage_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var viewModel = (MainViewModel)RootPanel.DataContext;
            var pointerPosition = e.GetCurrentPoint(mainImage).Position;
            if (viewModel.Image != null)
            {
                double scaleX = mainImage.ActualWidth / viewModel.Image.Width;
                double scaleY = mainImage.ActualHeight / viewModel.Image.Height;
                int pixelX = (int)(pointerPosition.X / scaleX);
                int pixelY = (int)(pointerPosition.Y / scaleY);

                if (pixelX >= 0 && pixelX < viewModel.Image.Width && pixelY >= 0 && pixelY < viewModel.Image.Height)
                {
                    var pixel = viewModel.Image.At<Vec3b>(pixelY, pixelX);
                    XPo.Text = "X: " + pixelX;
                    YPo.Text = ", Y: " + pixelY;
                }
            }
        }

        private void Canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var vm = (MainViewModel)RootPanel.DataContext;

            // Lấy tọa độ chuột gốc trong `Canvas`
            var point = e.GetCurrentPoint(DrawingCanvas).Position;

            // Điều chỉnh theo tỷ lệ zoom (ScaleTransform)
            double scaledX = point.X / scaleTransform.ScaleX;
            double scaledY = point.Y / scaleTransform.ScaleY;

            vm.StartDrawing(scaledX, scaledY); // Sử dụng tọa độ đã hiệu chỉnh
        }





        private void Canvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var vm = (MainViewModel)RootPanel.DataContext;

            // Lấy tọa độ chuột gốc trong `Canvas`
            var point = e.GetCurrentPoint(DrawingCanvas).Position;

            // Điều chỉnh theo tỷ lệ zoom (ScaleTransform)
            double scaledX = point.X / scaleTransform.ScaleX;
            double scaledY = point.Y / scaleTransform.ScaleY;

            vm.ContinueDrawing(scaledX, scaledY); // Sử dụng tọa độ đã hiệu chỉnh
        }




        private void Canvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var vm = (MainViewModel)RootPanel.DataContext;
            vm.StopDrawing();
        }


    }
}
