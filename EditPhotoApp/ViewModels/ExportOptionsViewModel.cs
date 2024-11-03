using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.Graphics.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;

namespace EditPhotoApp.ViewModels
{
    public class ExportOptionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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