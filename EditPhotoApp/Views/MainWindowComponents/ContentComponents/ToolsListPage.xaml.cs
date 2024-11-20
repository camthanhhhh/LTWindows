using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using EditPhotoApp.ViewModels;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EditPhotoApp.Views.MainWindowComponents.ContentComponents
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ToolsListPage : Page
    {
        public event Action<string> ToolSelected;
        private Canvas imageCanvas;

        public ToolsListPage()
        {
           
            this.InitializeComponent();
            
        }

        private void BrightnessContrastButton_Click(object sender, RoutedEventArgs e)
        {
            //toolUseComponent._frame = "
            ToolSelected?.Invoke("BrightnessContrast");
            if (imageCanvas != null)
            {
                imageCanvas.IsHitTestVisible = false;

            }
        }
        private void DrawingButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.MainWindow;
            imageCanvas = mainWindow.ImageEditPage.drawingCanvas;
            //toolUseComponent._frame = "
            ToolSelected?.Invoke("Drawing");
            imageCanvas.IsHitTestVisible = true;

        }
        private void InsertShapesButton_Click(object sender, RoutedEventArgs e)
        {
            //toolUseComponent._frame = "
            ToolSelected?.Invoke("Shapes");
            if (imageCanvas != null)
            {
                imageCanvas.IsHitTestVisible = false;

            }

        }
        private async void InsertPictureButton_Click(object sender, RoutedEventArgs e)
        {
            // Khởi tạo FileOpenPicker
            var openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".bmp");

            // Lấy window handle
            var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
            InitializeWithWindow.Initialize(openPicker, hwnd);

            // Mở FileOpenPicker và nhận file người dùng chọn
            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // Tạo một BitmapImage để hiển thị hình ảnh
                var bitmapImage = new BitmapImage();
                using (var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    bitmapImage.SetSource(stream);
                }

                // Tạo một Image để thêm vào Canvas
                var image = new Image
                {
                    Source = bitmapImage,
                    Width = 200, // Đặt chiều rộng mặc định cho hình ảnh
                    Height = 200 // Đặt chiều cao mặc định cho hình ảnh
                };

                // Đặt vị trí cho hình ảnh trên Canvas
                Canvas.SetLeft(image, 0);
                Canvas.SetTop(image, 50);
                DrawingCanvas.Children.Add(image);
            }
        }
    }
}
