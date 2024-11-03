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

        private void InsertRectangleButton_Click(object sender, RoutedEventArgs e)
        {
            var rectangle = new Rectangle
            {
                Width = 100,
                Height = 50,
                Fill = new SolidColorBrush(Microsoft.UI.Colors.Blue) // ??m b?o s? d?ng Microsoft.UI.Colors
            };

            Canvas.SetLeft(rectangle, 50);
            Canvas.SetTop(rectangle, 50);
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
            Canvas.SetTop(ellipse, 50);
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
            Canvas.SetTop(line, 50);
            DrawingCanvas.Children.Add(line);
        }
    }
}
