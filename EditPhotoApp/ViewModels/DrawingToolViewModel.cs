using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.Graphics.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;

namespace EditPhotoApp.ViewModels
{
    public class DrawingToolViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isDrawing;
        private bool isEraser;
        private double lastX;
        private double lastY;
        private Canvas drawingCanvas;
        private Image editableImage;

        public DrawingToolViewModel(MainWindow mainWindow)
        {
            if (mainWindow == null)
            {
                throw new ArgumentNullException(nameof(mainWindow), "MainWindow cannot be null.");
            }
            drawingCanvas = mainWindow.ImageEditPage.drawingCanvas;
            editableImage = mainWindow.ImageEditPage.saveImage; // Sử dụng đúng tên của Image
            SetupCanvasEvents();

        }
        private void SetupCanvasEvents()
        {
            // Thiết lập các sự kiện cho Canvas
            drawingCanvas.PointerPressed += HandlePointerPressed;
            drawingCanvas.PointerMoved += HandlePointerMoved;
            drawingCanvas.PointerReleased += HandlePointerReleased;
        }
        public void SelectPencilTool()
        {
            isEraser = false;
        }

        public void SelectBrushTool()
        {
            isEraser = false;
        }

        public void SelectEraserTool()
        {
            isEraser = true;
        }
        public void HandlePointerPressed(object sender, PointerRoutedEventArgs e)
        {
            isDrawing = true;
            var point = e.GetCurrentPoint(drawingCanvas).Position;
            lastX = point.X;
            lastY = point.Y;
        }

        public void HandlePointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (isDrawing)
            {
                var point = e.GetCurrentPoint(drawingCanvas).Position;
                DrawLine(lastX, lastY, point.X, point.Y);
                lastX = point.X;
                lastY = point.Y;
            }
        }

        public void HandlePointerReleased(object sender, PointerRoutedEventArgs e)
        {
            isDrawing = false;
        }
        public void AddText()
        {
            var textBlock = new TextBlock
            {
                Text = "Nhập văn bản ở đây",
                FontSize = 14,
                Foreground = new SolidColorBrush(Microsoft.UI.Colors.Black)
            };
            Canvas.SetLeft(textBlock, 50);
            Canvas.SetTop(textBlock, 50);
            drawingCanvas.Children.Add(textBlock);
        }

        private void DrawingCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            isDrawing = true;
            var point = e.GetCurrentPoint(drawingCanvas).Position;
            lastX = point.X;
            lastY = point.Y;
        }

        private void DrawingCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (isDrawing)
            {
                var point = e.GetCurrentPoint(drawingCanvas).Position;
                DrawLine(lastX, lastY, point.X, point.Y);
                lastX = point.X;
                lastY = point.Y;
            }
        }

        private void DrawingCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            isDrawing = false;
        }

        private void DrawLine(double x1, double y1, double x2, double y2)
        {
            var line = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = !isEraser ? new SolidColorBrush(Microsoft.UI.Colors.Black) : new SolidColorBrush(Microsoft.UI.Colors.White),
                StrokeThickness = 2
            };
            drawingCanvas.Children.Add(line);
        }
    }
}
