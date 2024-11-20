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
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using System.Drawing;
using EditPhotoApp.ViewModels.MainWindowComponents.ContentComponents.ToolFeatureViewModel;
using EditPhotoApp.Views.MainWindowComponents.ContentComponents;
using EditPhotoApp.ViewModels.MainWindowComponents.TopBarViewModel;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EditPhotoApp.Views.MainWindowComponents
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TopBarPage : Page
    {
        private Bitmap _originalImage;
        private ImportExportImageViewModel importExportViewModel;
        private BrightnessAndContrastViewModel brightnessAndContrastViewModel;
        public static string ImageFilePath { get; private set; }

        public TopBarPage(BrightnessAndContrastViewModel brightnessAndContrastViewModel)
        {
            importExportViewModel = new ImportExportImageViewModel();
            this.brightnessAndContrastViewModel = brightnessAndContrastViewModel;
            this.InitializeComponent();
           

        }
        private void FileButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            XamlRoot xamlRoot = this.Content.XamlRoot;
            String rootName = "";
            if (sender is MenuFlyoutItem menuFlyoutItem)
            {
                rootName = menuFlyoutItem.Text;
            }
            importExportViewModel.export(xamlRoot, rootName);

      
        }

        // Chuyển đổi giữa Dark và Light
        private void ThemeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if we need to run on the UI thread
            if (DispatcherQueue.TryEnqueue(() =>
            {
                var currentTheme = Application.Current.RequestedTheme;

                if (currentTheme == ApplicationTheme.Light)
                {
                    Application.Current.RequestedTheme = ApplicationTheme.Dark;
                    ThemeToggleButton.Content = "☀️"; // Update icon to sun for dark mode
                    App.MainWindow.SetMainGridBackground(new SolidColorBrush(Microsoft.UI.Colors.Black));
                }
                else
                {
                    Application.Current.RequestedTheme = ApplicationTheme.Light;
                    ThemeToggleButton.Content = "🌙"; // Update icon to moon for light mode
                    App.MainWindow.SetMainGridBackground(new SolidColorBrush(Microsoft.UI.Colors.White));
                }

                // Save theme state in LocalSettings
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["AppTheme"] = Application.Current.RequestedTheme == ApplicationTheme.Dark ? "Dark" : "Light";
            }))
            {
                // The action was successfully enqueued
            }
            else
            {
                // Handle the error if needed (e.g., log the error)
            }
        }

        //private async void ImportImage_Click(object sender, RoutedEventArgs e)
        //{
        //    var mainWindow = App.MainWindow;

        //    var picker = new FileOpenPicker();
        //    picker.ViewMode = PickerViewMode.Thumbnail;
        //    picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        //    picker.FileTypeFilter.Add(".png");
        //    picker.FileTypeFilter.Add(".jpg");
        //    picker.FileTypeFilter.Add(".jpeg");

        //    var hwnd = WindowNative.GetWindowHandle(App.MainWindow);

        //    InitializeWithWindow.Initialize(picker, hwnd);

        //    var file = await picker.PickSingleFileAsync();
        //    if (file != null)
        //    {
        //        var bitmapImage = new BitmapImage();
        //        using (var stream = await file.OpenAsync(FileAccessMode.Read))
        //        {
        //            bitmapImage.SetSource(stream);
        //            var decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);
        //            var softwareBitmap = await decoder.GetSoftwareBitmapAsync();

        //            _originalImage = brightnessAndContrastViewModel.SoftwareBitmapToBitmap(softwareBitmap);
        //            brightnessAndContrastViewModel.SetOriginalImage(_originalImage);

        //        }
        //        mainWindow.ImageEditPage.saveImage.Source = bitmapImage;
        //        ImageFilePath = file.Path;

        //    }
        //}

        private async void ImportImage_Click(object sender, RoutedEventArgs e)
        {
            // Lấy XamlRoot từ Content
            XamlRoot xamlRoot = (sender as FrameworkElement)?.XamlRoot;
            if (xamlRoot == null)
            {
                return; // Nếu không lấy được XamlRoot, không thực hiện tiếp
            }

            string response = await importExportViewModel.import(xamlRoot,brightnessAndContrastViewModel);
            if (!string.IsNullOrEmpty(response))
            {
                ImageFilePath = response;
            }
            else
            {
                var dialog = new ContentDialog
                {
                    Title = "Import thất bại",
                    Content = "Không thể import hình ảnh. Vui lòng thử lại.",
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot
                };
                await dialog.ShowAsync();
            }
        }

    }
}
