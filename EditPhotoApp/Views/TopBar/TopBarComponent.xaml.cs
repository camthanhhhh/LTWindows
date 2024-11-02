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
using EditPhotoApp.Views.FeatureWindow;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using EditPhotoApp.ViewModels;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EditPhotoApp.Views.MainWindowComponents
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TopBarComponent : Page
    {
        public TopBarComponent()
        {
            this.InitializeComponent();
        }
        private void FileButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            ExportOptionsViewModel exportOptionsViewModel = new ExportOptionsViewModel();
            XamlRoot xamlRoot = this.Content.XamlRoot;
            String rootName = "";
            if (sender is MenuFlyoutItem menuFlyoutItem)
            {
                rootName = menuFlyoutItem.Name;
            }
            exportOptionsViewModel.export(xamlRoot, "Filename");

            // Trong TopBarComponent.xaml.cs
            //var mainWindow = App.MainWindow;
            //if (mainWindow?.ImageEditPage?.saveImage != null)
            //{
            //    var image = mainWindow.ImageEditPage.saveImage;

            //    // Thực hiện xuất ảnh
            //    RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
            //    await renderTargetBitmap.RenderAsync(image); // Render Image từ trang khác
            //    var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

            //    // Mở FileSavePicker và lưu ảnh
            //    var savePicker = new FileSavePicker
            //    {
            //        SuggestedStartLocation = PickerLocationId.PicturesLibrary,
            //        SuggestedFileName = "ExportedImage"
            //    };

            //    savePicker.FileTypeChoices.Add("PNG Image", new List<string> { ".png" });
            //    savePicker.FileTypeChoices.Add("JPEG Image", new List<string> { ".jpg", ".jpeg" });
            //    savePicker.FileTypeChoices.Add("Bitmap Image", new List<string> { ".bmp" });

            //    nint windowHandle = WindowNative.GetWindowHandle(App.MainWindow);
            //    InitializeWithWindow.Initialize(savePicker, windowHandle);

            //    StorageFile file = await savePicker.PickSaveFileAsync();

            //    if (file != null)
            //    {
            //        using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            //        {
            //            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
            //            encoder.SetPixelData(
            //                BitmapPixelFormat.Bgra8,
            //                BitmapAlphaMode.Premultiplied,
            //                (uint)renderTargetBitmap.PixelWidth,
            //                (uint)renderTargetBitmap.PixelHeight,
            //                96.0,  // dpiX
            //                96.0,  // dpiY
            //                pixelBuffer.ToArray()
            //            );
            //            await encoder.FlushAsync();
            //        }

            //        // Hiển thị thông báo thành công
            //        var dialog = new ContentDialog
            //        {
            //            Title = "Lưu thành công",
            //            Content = $"File đã được lưu: {file.Name}",
            //            CloseButtonText = "OK"
            //        };
            //        dialog.XamlRoot = this.Content.XamlRoot;
            //        await dialog.ShowAsync();
            //    }

            //}
        }
    }
}
