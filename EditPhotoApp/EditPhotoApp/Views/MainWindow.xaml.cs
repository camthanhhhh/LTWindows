using EditPhotoApp.Views.MainWindowComponents;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using System;
using Microsoft.UI.Xaml.Input;

namespace EditPhotoApp
{
    public sealed partial class MainWindow : Window
    {
        private ImageEditComponent _imageEditComponent;

        public MainWindow()
        {
            this.InitializeComponent();
            this.ToolsComponentFrame.Navigate(typeof(ToolsListComponent));
            this.ToolUseComponentFrame.Navigate(typeof(ToolUseComponent));
            this._imageEditComponent = new ImageEditComponent();
            this.ImageEditComponentFrame.Content = _imageEditComponent;
        }

        private void FileTextBlock_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (FilePopup.IsOpen)
            {
                FilePopup.IsOpen = false;
            }
            else
            {
                FilePopup.IsOpen = true;
            }
        }

        private async void OpenPopupItem_Click(object sender, PointerRoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");

            var hwnd = WindowNative.GetWindowHandle(this);
            InitializeWithWindow.Initialize(picker, hwnd);

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var bitmapImage = new BitmapImage();
                using (var stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    bitmapImage.SetSource(stream);
                }

                // Cập nhật hình ảnh cho mainImage trong ImageEditComponent
                if (_imageEditComponent.FindName("mainImage") is Image mainImage)
                {
                    mainImage.Source = bitmapImage;
                }
            }

            FilePopup.IsOpen = false; 
        }

        private void SavePopupItem_Click(object sender, PointerRoutedEventArgs e)
        {
            FilePopup.IsOpen = false; 
        }

        private void ExitPopupItem_Click(object sender, PointerRoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
