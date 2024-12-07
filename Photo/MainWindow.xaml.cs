using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using OpenCvSharp;
using Photo.ViewModels;
using Photo.Views;
using System.Data;
using System.Diagnostics;
using Point = Windows.Foundation.Point;

namespace Photo
{
    public sealed partial class MainWindow : Microsoft.UI.Xaml.Window
    {
        bool isClick = false;
        public MainWindow(object dataContext)
        {
            this.InitializeComponent();
            RootPanel.DataContext = dataContext;
            DraggableGrid.PointerPressed += OnGridPointerPressed;
            DraggableGrid.PointerMoved += OnGridPointerMoved;
            DraggableGrid.PointerReleased += OnGridPointerReleased;
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

            scaleTransform.ScaleX *= zoomFactor;
            scaleTransform.ScaleY *= zoomFactor;

            scrollImageTarget.ChangeView(scrollImageTarget.HorizontalOffset + offsetX, scrollImageTarget.VerticalOffset + offsetY, null);

            e.Handled = true;
        }
        //private void scrollImageTarget_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        //{
        //    int delta = e.GetCurrentPoint(scrollImageTarget).Properties.MouseWheelDelta;

        //    // Xác định hệ số zoom
        //    double zoomFactor = delta > 0 ? 1.1 : 0.9;

        //    // Tính toán vị trí con trỏ chuột trên hình ảnh
        //    Point pointerPosition = e.GetCurrentPoint(mainImage).Position;

        //    // Cập nhật ScaleTransform
        //    double newScaleX = scaleTransform.ScaleX * zoomFactor;
        //    double newScaleY = scaleTransform.ScaleY * zoomFactor;

        //    // Giới hạn mức zoom
        //    if (newScaleX < 1 || newScaleY < 1 || newScaleX > 5 || newScaleY > 5)
        //    {
        //        return; // Không cho phép zoom vượt quá giới hạn
        //    }

        //    scaleTransform.ScaleX = newScaleX;
        //    scaleTransform.ScaleY = newScaleY;

        //    // Tính toán offset để giữ vị trí con trỏ chuột ổn định
        //    double offsetX = pointerPosition.X * (newScaleX - scaleTransform.ScaleX);
        //    double offsetY = pointerPosition.Y * (newScaleY - scaleTransform.ScaleY);

        //    scrollImageTarget.ChangeView(scrollImageTarget.HorizontalOffset + offsetX, scrollImageTarget.VerticalOffset + offsetY, null);

        //    e.Handled = true;
        //}


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
        private void scrollImageOrigin_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
            int delta = e.GetCurrentPoint(scrollImageOrigin).Properties.MouseWheelDelta;

            double zoomFactor = delta > 0 ? 1.1 : 0.9;

            double currentWidth = mainImageOrigin.ActualWidth;
            double currentHeight = mainImageOrigin.ActualHeight;

            Point pointerPosition = e.GetCurrentPoint(mainImageOrigin).Position;

            double imageMouseX = pointerPosition.X;
            double imageMouseY = pointerPosition.Y;

            mainImageOrigin.Width = currentWidth * zoomFactor;
            mainImageOrigin.Height = currentHeight * zoomFactor;

            double newImageMouseX = imageMouseX * (mainImageOrigin.Width / currentWidth);
            double newImageMouseY = imageMouseY * (mainImageOrigin.Height / currentHeight);

            double offsetX = newImageMouseX - imageMouseX;
            double offsetY = newImageMouseY - imageMouseY;

            scaleTransformOrigin.ScaleX *= zoomFactor;
            scaleTransformOrigin.ScaleY *= zoomFactor;

            scrollImageOrigin.ChangeView(scrollImageOrigin.HorizontalOffset + offsetX, scrollImageOrigin.VerticalOffset + offsetY, null);

            e.Handled = true;
        }

        private void mainImageOrigin_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var viewModel = (MainViewModel)RootPanel.DataContext;
            var pointerPosition = e.GetCurrentPoint(mainImageOrigin).Position;
            if (viewModel.Image != null)
            {
                double scaleX = mainImageOrigin.ActualWidth / viewModel.Image.Width;
                double scaleY = mainImageOrigin.ActualHeight / viewModel.Image.Height;
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
            if (vm.DrawingStatus.Status ==false) return; 

            var point = e.GetCurrentPoint(DrawingCanvas).Position;

            // Điều chỉnh tọa độ dựa trên tỷ lệ zoom
            double scaledX = point.X / scaleTransform.ScaleX;
            double scaledY = point.Y / scaleTransform.ScaleY;
            isClick = true;
            vm.StartDrawing(scaledX, scaledY);
        }

        private void Canvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            
            var vm = (MainViewModel)RootPanel.DataContext;
            if (vm.DrawingStatus.Status == false) return;

            var point = e.GetCurrentPoint(DrawingCanvas).Position;

            // Điều chỉnh tọa độ dựa trên tỷ lệ zoom
            double scaledX = point.X / scaleTransform.ScaleX;
            double scaledY = point.Y / scaleTransform.ScaleY;
            if (isClick ==true)
            {
                vm.ContinueDrawing(scaledX, scaledY);
            }    
           
           
        }

        private void Canvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var vm = (MainViewModel)RootPanel.DataContext;

            if (vm.DrawingStatus.Status == false) return;

            isClick = false;

            vm.StopDrawing();
        }
        private void OnShowColorPickerClicked(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ bảng màu
            var colorPickerWindow = new ColorChoose(RootPanel.DataContext);
            colorPickerWindow.Activate();
        }
        private Point _lastPointerPosition;
        private bool _isDragging = false;
        private Point _startPointerPosition;


        private void TextBlock_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            _isDragging = true;
            //_draggedTextBlock = (TextElementModel)((FrameworkElement)sender).DataContext;

            // Lưu vị trí chuột khi nhấn
            _startPointerPosition = e.GetCurrentPoint(DrawingCanvas).Position;
        }

        private void TextBlock_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            //if (_isDragging && _draggedTextBlock != null)
            //{
            //    // Lấy vị trí hiện tại của chuột
            //    var currentPointerPosition = e.GetCurrentPoint(DrawingCanvas).Position;

            //    // Tính toán tọa độ mới
            //    double offsetX = currentPointerPosition.X - _startPointerPosition.X;
            //    double offsetY = currentPointerPosition.Y - _startPointerPosition.Y;

            //    // Cập nhật vị trí trong ViewModel
            //    //_draggedTextBlock.UpdatePosition(offsetX, offsetY);

            //    // Cập nhật vị trí bắt đầu
            //    _startPointerPosition = currentPointerPosition;
            //}
        }

        private void TextBlock_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            _isDragging = false;
        }



        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainViewModel)RootPanel.DataContext;

            // Lấy tọa độ của DraggableGrid trong Canvas
            double gridX = Canvas.GetLeft(DraggableGrid);
            double gridY = Canvas.GetTop(DraggableGrid);

            if (double.IsNaN(gridX)) gridX = 0;
            if (double.IsNaN(gridY)) gridY = 0;

            // Lấy kích thước thực tế của DraggableGrid và TextBlock
            double gridWidth = DraggableGrid.ActualWidth;
            double gridHeight = DraggableGrid.ActualHeight;

            double textBlockWidth = DraggableGridTextBlock.ActualWidth;
            double textBlockHeight = DraggableGridTextBlock.ActualHeight;

            // Lấy giá trị Margin hoặc Padding nếu có
            Thickness textMargin = DraggableGridTextBlock.Margin;

            // Tính vị trí thực tế của TextBlock so với Canvas
            double textX = gridX + (gridWidth - textBlockWidth) / 2 + textMargin.Left - textMargin.Right;
            double textY = gridY + (gridHeight - textBlockHeight) / 2 + textMargin.Top - textMargin.Bottom;

            // Điều chỉnh tọa độ dựa trên tỷ lệ zoom nếu cần thiết
            double scaledX = textX / scaleTransform.ScaleX;
            double scaledY = textY / scaleTransform.ScaleY;

            OpenCvSharp.Point point = new OpenCvSharp.Point(scaledX, scaledY);
            Debug.WriteLine($"TextBlock Position: {point}");

            // Lấy dữ liệu Text và các thuộc tính
            string text = TextInput.Text;

            ComboBoxItem typeItem = (ComboBoxItem)FontSelector.SelectedItem;
            string font = typeItem.Content.ToString();

            int size = int.Parse(SizeInput.Text);

            // Thêm văn bản vào Mat
            vm.AddTextToMat(point, text, font, size);
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
           
            var vm = (MainViewModel)RootPanel.DataContext;
            vm.AddTextCancel();


        }

        ///Draft code
        private bool isDragging = false;
        private double initialPointerX, initialPointerY;
        private double initialCanvasLeft, initialCanvasTop;

        

        private void OnGridPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            isDragging = true;

            var pointerPosition = e.GetCurrentPoint(DrawingCanvas);
            initialPointerX = pointerPosition.Position.X;
            initialPointerY = pointerPosition.Position.Y;

            initialCanvasLeft = Canvas.GetLeft(DraggableGrid);
            initialCanvasTop = Canvas.GetTop(DraggableGrid);

            DraggableGrid.CapturePointer(e.Pointer);
        }

        private void OnGridPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (isDragging)
            {
                var pointerPosition = e.GetCurrentPoint(DrawingCanvas);
                double deltaX = pointerPosition.Position.X - initialPointerX;
                double deltaY = pointerPosition.Position.Y - initialPointerY;

                Canvas.SetLeft(DraggableGrid, initialCanvasLeft + deltaX);
                Canvas.SetTop(DraggableGrid, initialCanvasTop + deltaY);
            }
        }

        private void OnGridPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            // Kết thúc kéo
            isDragging = false;
            DraggableGrid.ReleasePointerCaptures();
        }
       
        private void AddNewTextBlock(object sender, RoutedEventArgs e)
        {
            // Kết thúc kéo
            var vm = (MainViewModel)RootPanel.DataContext;
            string text = TextInput.Text;
            //string font = FontSelector.Text;
            ComboBoxItem typeItem = (ComboBoxItem)FontSelector.SelectedItem;
            string font = typeItem.Content.ToString();
            Debug.WriteLine(font);
            int size = int.Parse(SizeInput.Text);
            vm.OpenGridTextBlock(text, font, size);

        }
       
    }
}
