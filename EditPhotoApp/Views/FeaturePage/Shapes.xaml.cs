using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EditPhotoApp.Views.FeaturePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shapes : Page
    {
        public Shapes()
        {
            this.InitializeComponent();
        }
        private Point _startPoint;
        private bool _isDragging = false;

        private void Shape_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            _startPoint = e.GetCurrentPoint(DrawingCanvas).Position;
            _isDragging = true;
            var shape = sender as UIElement;
            shape.CapturePointer(e.Pointer);
        }

        private void Shape_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (_isDragging)
            {
                var shape = sender as UIElement;
                var currentPoint = e.GetCurrentPoint(DrawingCanvas).Position;
                var transform = shape.RenderTransform as CompositeTransform ?? new CompositeTransform();
                double offsetX = currentPoint.X - _startPoint.X;
                double offsetY = currentPoint.Y - _startPoint.Y;

                transform.TranslateX += offsetX;
                transform.TranslateY += offsetY;

                shape.RenderTransform = transform;

                _startPoint = currentPoint;
            }
        }

        private void Shape_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            _isDragging = false;
            var shape = sender as UIElement;
            shape.ReleasePointerCaptures();
        }

        private void InsertRectangleButton_Click(object sender, RoutedEventArgs e)
        {
            var rectangle = new Rectangle
            {
                Name= "Rectangle",
                Width = 100,
                Height = 50,
                Fill = new SolidColorBrush(Microsoft.UI.Colors.Blue) // ??m b?o s? d?ng Microsoft.UI.Colors
            };
            rectangle.PointerPressed += Shape_PointerPressed;
            rectangle.PointerMoved += Shape_PointerMoved;
            rectangle.PointerReleased += Shape_PointerReleased;
            Canvas.SetLeft(rectangle, 50);
            Canvas.SetTop(rectangle, 50);
            DrawingCanvas.Children.Clear(); // Xóa tất cả các đối tượng trên Canvas

            DrawingCanvas.Children.Add(rectangle);
        }

        private void InsertEllipseButton_Click(object sender, RoutedEventArgs e)
        {
            var ellipse = new Ellipse
            {
                Width = 50,
                Height = 50,
                Fill = new SolidColorBrush(Microsoft.UI.Colors.Red) // ??m b?o s? d?ng Microsoft.UI.Colors
            };

            Canvas.SetLeft(ellipse, 200);
            Canvas.SetTop(ellipse, 50); DrawingCanvas.Children.Clear(); // Xóa tất cả các đối tượng trên Canvas

            DrawingCanvas.Children.Add(ellipse);
        }

        private void InsertLineButton_Click(object sender, RoutedEventArgs e)
        {
            var line = new Line
            {
                X1 = 0,
                Y1 = 0,
                X2 = 200,
                Y2 = 100,
                Stroke = new SolidColorBrush(Microsoft.UI.Colors.Green), // ??m b?o s? d?ng Microsoft.UI.Colors
                StrokeThickness = 2
            };

            Canvas.SetLeft(line, 50);
            Canvas.SetTop(line, 50); DrawingCanvas.Children.Clear(); // Xóa tất cả các đối tượng trên Canvas

            DrawingCanvas.Children.Add(line);
        }
    }
}
