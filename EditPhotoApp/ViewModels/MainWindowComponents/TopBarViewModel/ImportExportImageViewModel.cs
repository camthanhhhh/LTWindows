using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using System.Runtime.InteropServices.WindowsRuntime;
using EditPhotoApp.ViewModels.MainWindowComponents.ContentComponents.ToolFeatureViewModel;
using System.Drawing;

namespace EditPhotoApp.ViewModels.MainWindowComponents.TopBarViewModel
{
    public class ImportExportImageViewModel
    {
        public ImportExportImageViewModel() { 

        }
        private Bitmap _originalImage;
        public async Task<string> import(XamlRoot xamlRoot, BrightnessAndContrastViewModel brightnessAndContrastViewModel)
        {
            if (brightnessAndContrastViewModel is null)
            {
                throw new ArgumentNullException(nameof(brightnessAndContrastViewModel));
            }

            try
            {
                var mainWindow = App.MainWindow;

                // Cấu hình FileOpenPicker
                var picker = new FileOpenPicker
                {
                    ViewMode = PickerViewMode.Thumbnail,
                    SuggestedStartLocation = PickerLocationId.PicturesLibrary
                };
                picker.FileTypeFilter.Add(".png");
                picker.FileTypeFilter.Add(".jpg");
                picker.FileTypeFilter.Add(".jpeg");

                // Kết nối FileOpenPicker với cửa sổ hiện tại
                var hwnd = WindowNative.GetWindowHandle(mainWindow);
                InitializeWithWindow.Initialize(picker, hwnd);

                // Chọn file
                var file = await picker.PickSingleFileAsync();
                if (file == null)
                {
                    return null; // Người dùng không chọn file
                }

                // Đọc và xử lý hình ảnh
                using (var stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    var decoder = await BitmapDecoder.CreateAsync(stream);
                    var softwareBitmap = await decoder.GetSoftwareBitmapAsync();

                    // Chuyển đổi SoftwareBitmap sang Bitmap
                    _originalImage = brightnessAndContrastViewModel.SoftwareBitmapToBitmap(softwareBitmap);
                    brightnessAndContrastViewModel.SetOriginalImage(_originalImage);

                    // Hiển thị ảnh trên giao diện
                    var bitmapImage = new BitmapImage();
                    stream.Seek(0); // Đặt lại con trỏ stream
                    bitmapImage.SetSource(stream);
                    mainWindow.ImageEditPage.saveImage.Source = bitmapImage;

                    // Trả về đường dẫn file
                    return file.Path;
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu xảy ra
                var dialog = new ContentDialog
                {
                    Title = "Lỗi Import",
                    Content = $"Đã xảy ra lỗi khi import: {ex.Message}",
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot
                };
                await dialog.ShowAsync();
                return null;
            }
        }

        public async void export(XamlRoot xamlRoot, string fileName)
        {
            var mainWindow = App.MainWindow;
            if (mainWindow?.ImageEditPage?.saveImage != null)
            {
                var image = mainWindow.ImageEditPage.saveImage;

                // Thực hiện xuất ảnh
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
                await renderTargetBitmap.RenderAsync(image); // Render Image từ trang khác
                var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

                // Mở FileSavePicker và lưu ảnh
                var savePicker = new FileSavePicker
                {
                    SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                    SuggestedFileName = "ExportedImage"
                };
                switch (fileName)
                {
                    case "JPG":
                        savePicker.FileTypeChoices.Add("JPG Image", new List<string> { ".jpg" });

                        break;
                    case "PNG":
                        savePicker.FileTypeChoices.Add("PNG Image", new List<string> { ".png" });
                        break;
                    case "BMP":
                        savePicker.FileTypeChoices.Add("Bitmap Image", new List<string> { ".bmp" });

                        break;
                    case "GIF":
                        savePicker.FileTypeChoices.Add("Gif Image", new List<string> { ".gif" });

                        break;
                    case "TIFF":
                        savePicker.FileTypeChoices.Add("Tiff Image", new List<string> { ".tiff" });

                        break;



                }
                //savePicker.FileTypeChoices.Add("JPEG Image", new List<string> { ".jpg", ".jpeg" });
                //savePicker.FileTypeChoices.Add("Bitmap Image", new List<string> { ".bmp" });

                nint windowHandle = WindowNative.GetWindowHandle(App.MainWindow);
                InitializeWithWindow.Initialize(savePicker, windowHandle);

                StorageFile file = await savePicker.PickSaveFileAsync();

                if (file != null)
                {
                    using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                        encoder.SetPixelData(
                            BitmapPixelFormat.Bgra8,
                            BitmapAlphaMode.Premultiplied,
                            (uint)renderTargetBitmap.PixelWidth,
                            (uint)renderTargetBitmap.PixelHeight,
                            96.0,  // dpiX
                            96.0,  // dpiY
                            pixelBuffer.ToArray()
                        );
                        await encoder.FlushAsync();
                    }

                    // Hiển thị thông báo thành công
                    var dialog = new ContentDialog
                    {
                        Title = "Lưu thành công",
                        Content = $"File đã được lưu: {file.Name}",
                        CloseButtonText = "OK"
                    };
                    dialog.XamlRoot = xamlRoot;  // Use the provided XamlRoot
                    await dialog.ShowAsync();
                }
            }
        }
    }
}
