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
        private ImageEditComponent _imageEditComponent;

        public TopBarComponent()
        {
            this.InitializeComponent();
            _imageEditComponent = new ImageEditComponent();
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
                rootName = menuFlyoutItem.Text;
            }
            exportOptionsViewModel.export(xamlRoot, rootName);

      
        }


        private async void ImportImage_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.MainWindow;

            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");

            var hwnd = WindowNative.GetWindowHandle(App.MainWindow);

            InitializeWithWindow.Initialize(picker, hwnd);

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var bitmapImage = new BitmapImage();
                using (var stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    bitmapImage.SetSource(stream);
                }
         
                //if (_imageEditComponent.FindName("mainImage") is Image mainImage)
                //{
                //    mainImage.Source = bitmapImage;
                //}

                  mainWindow.ImageEditPage.saveImage.Source = bitmapImage;
          
            }
        }


    }
}
