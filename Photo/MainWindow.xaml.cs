using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using OpenCvSharp;
using Photo.ViewModels;
using Photo.Views;
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
        //private void scrollImageTarget_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        //{
        //    int delta = e.GetCurrentPoint(scrollImageTarget).Properties.MouseWheelDelta;

        //    double zoomFactor = delta > 0 ? 1.1 : 0.9;

        //    double currentWidth = mainImage.ActualWidth;
        //    double currentHeight = mainImage.ActualHeight;

        //    Point pointerPosition = e.GetCurrentPoint(mainImage).Position;
        //    double imageMouseX = pointerPosition.X;
        //    double imageMouseY = pointerPosition.Y;

        //    mainImage.Width = currentWidth * zoomFactor;
        //    mainImage.Height = currentHeight * zoomFactor;

        //    double newImageMouseX = imageMouseX * (mainImage.Width / currentWidth);
        //    double newImageMouseY = imageMouseY * (mainImage.Height / currentHeight);

        //    double offsetX = newImageMouseX - imageMouseX;
        //    double offsetY = newImageMouseY - imageMouseY;

        //    scaleTransform.ScaleX *= zoomFactor;
        //    scaleTransform.ScaleY *= zoomFactor;

        //    scrollImageTarget.ChangeView(scrollImageTarget.HorizontalOffset + offsetX, scrollImageTarget.VerticalOffset + offsetY, null);

        //    e.Handled = true;
        //}
        private void scrollImageTarget_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
            int delta = e.GetCurrentPoint(scrollImageTarget).Properties.MouseWheelDelta;

            // Xác định hệ số zoom
            double zoomFactor = delta > 0 ? 1.1 : 0.9;

            // Tính toán vị trí con trỏ chuột trên hình ảnh
            Point pointerPosition = e.GetCurrentPoint(mainImage).Position;

            // Cập nhật ScaleTransform
            double newScaleX = scaleTransform.ScaleX * zoomFactor;
            double newScaleY = scaleTransform.ScaleY * zoomFactor;

            // Giới hạn mức zoom
            if (newScaleX < 1 || newScaleY < 1 || newScaleX > 5 || newScaleY > 5)
            {
                return; // Không cho phép zoom vượt quá giới hạn
            }

            scaleTransform.ScaleX = newScaleX;
            scaleTransform.ScaleY = newScaleY;

            // Tính toán offset để giữ vị trí con trỏ chuột ổn định
            double offsetX = pointerPosition.X * (newScaleX - scaleTransform.ScaleX);
            double offsetY = pointerPosition.Y * (newScaleY - scaleTransform.ScaleY);

            scrollImageTarget.ChangeView(scrollImageTarget.HorizontalOffset + offsetX, scrollImageTarget.VerticalOffset + offsetY, null);

            e.Handled = true;
        }


        private void mainImage_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var viewModel = (MainViewModel)RootPanel.DataContext;
            var pointerPosition = e.GetCurrentPoint(DrawingCanvas).Position;
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

        //private void Canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        //{
        //    var vm = (MainViewModel)RootPanel.DataContext;
        //    var point = e.GetCurrentPoint(DrawingCanvas).Position;

        //    // Điều chỉnh tọa độ dựa trên tỷ lệ zoom của ScaleTransform và offset của ScrollViewer
        //    double scaledX = (point.X - scrollImageTarget.HorizontalOffset) / scaleTransform.ScaleX;
        //    double scaledY = (point.Y - scrollImageTarget.VerticalOffset) / scaleTransform.ScaleY;
        //    //double scaledX = point.X;
        //    //double scaledY = point.Y;
        //    // Đưa tọa độ đã hiệu chỉnh vào phương thức StartDrawing
        //    vm.StartDrawing(scaledX, scaledY);
        //}

        //private void Canvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        //{
        //    var vm = (MainViewModel)RootPanel.DataContext;
        //    var point = e.GetCurrentPoint(mainImage).Position;

        //    // Điều chỉnh tọa độ dựa trên tỷ lệ zoom của ScaleTransform và offset của ScrollViewer
        //    double scaledX = (point.X - scrollImageTarget.HorizontalOffset) / scaleTransform.ScaleX;
        //    double scaledY = (point.Y - scrollImageTarget.VerticalOffset) / scaleTransform.ScaleY;
        //    //double scaledX = point.X;
        //    //double scaledY = point.Y;
        //    // Đưa tọa độ đã hiệu chỉnh vào phương thức ContinueDrawing
        //    vm.ContinueDrawing(scaledX, scaledY);
        //}

        private void Canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var vm = (MainViewModel)RootPanel.DataContext;
            var point = e.GetCurrentPoint(DrawingCanvas).Position;

            // Điều chỉnh tọa độ dựa trên tỷ lệ zoom
            double scaledX = point.X / scaleTransform.ScaleX;
            double scaledY = point.Y / scaleTransform.ScaleY;

            vm.StartDrawing(scaledX, scaledY);
        }

        private void Canvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var vm = (MainViewModel)RootPanel.DataContext;
            var point = e.GetCurrentPoint(DrawingCanvas).Position;

            // Điều chỉnh tọa độ dựa trên tỷ lệ zoom
            double scaledX = point.X / scaleTransform.ScaleX;
            double scaledY = point.Y / scaleTransform.ScaleY;

            vm.ContinueDrawing(scaledX, scaledY);
        }

        private void Canvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var vm = (MainViewModel)RootPanel.DataContext;
            vm.StopDrawing();
        }
        private void OnShowColorPickerClicked(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ bảng màu
            var colorPickerWindow = new ColorChoose(RootPanel.DataContext);
            colorPickerWindow.Activate();
        }

    }
}
