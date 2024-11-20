using EditPhotoApp.ViewModels.MainWindowComponents.ContentComponents.ToolFeatureViewModel;
using EditPhotoApp.Views.FeaturePage;
using EditPhotoApp.Views.FeaturePage.ContentComponents;
using EditPhotoApp.Views.FeaturePage.ContentComponents.FeaturePage;
using EditPhotoApp.Views.MainWindowComponents;
using EditPhotoApp.Views.MainWindowComponents.ContentComponents;
using EditPhotoApp.Views.MainWindowComponents.ContentComponents.FeaturePage;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EditPhotoApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        //public ToolUseComponent ToolUsePage => ToolUseComponentFrame.Content as ToolUseComponent;
        private BrightnessAndContrastViewModel brightnessAndContrastViewModel;
        BrightnessAndContrastPage brightnessAndContrastPage;
        TopBarPage topBarComponent;
        ToolsListPage toolsListPage;
        ShapesPage shapesPage;
        DrawingToolViewModel drawingToolViewModel;
        DrawingToolPage drawingToolPage;
        public ImageEditPage ImageEditPage => ImageEditComponentFrame.Content as ImageEditPage;

        public MainWindow()
        {
            this.InitializeComponent();
            toolsListPage = new ToolsListPage();
            toolsListPage.ToolSelected += OnToolSelected; // Đăng ký sự kiện ở đây
            brightnessAndContrastViewModel = new BrightnessAndContrastViewModel();
            topBarComponent = new TopBarPage(brightnessAndContrastViewModel);
            brightnessAndContrastPage = new BrightnessAndContrastPage(brightnessAndContrastViewModel);
            shapesPage = new ShapesPage();

            
            this.TopBarComponentFrame.Content = topBarComponent;
            this.ToolsComponentFrame.Content = toolsListPage; // Thiết lập nội dung trực tiếp
            this.ImageEditComponentFrame.Navigate(typeof(ImageEditPage));
        }
        //private void LoadThemeSettings()
        //{
        //    var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        //    if (localSettings.Values.ContainsKey("AppTheme"))
        //    {
        //        string appTheme = localSettings.Values["AppTheme"] as string;
        //        if (appTheme == "Dark")
        //        {
        //            Application.Current.RequestedTheme = ApplicationTheme.Dark;
        //            SetMainGridBackground(new SolidColorBrush(Microsoft.UI.Colors.Black)); // Sửa lại ở đây
        //            topBarComponent.ThemeToggleButton.Content = "☀️"; // Cập nhật icon
        //        }
        //        else
        //        {
        //            Application.Current.RequestedTheme = ApplicationTheme.Light;
        //            SetMainGridBackground(new SolidColorBrush(Microsoft.UI.Colors.White)); // Sửa lại ở đây
        //            topBarComponent.ThemeToggleButton.Content = "🌙"; // Cập nhật icon
        //        }
        //    }
        //}

        public void SetMainGridBackground(SolidColorBrush color)
        {
            MainGrid.Background = color;
        }
        private void OnToolSelected(string tool)
        {
            // Navigate to the appropriate tool page based on the selected tool
            switch (tool)
            {
                case "BrightnessContrast":
                    // Chuyển tham số đã khởi tạo
                    ToolUseComponentFrame.Content = brightnessAndContrastPage;
                    break;
                // Add cases for other tools here
                case "Drawing":
                    drawingToolViewModel = new DrawingToolViewModel(this);
                    drawingToolPage = new DrawingToolPage(drawingToolViewModel);
                    ToolUseComponentFrame.Content = drawingToolPage;

                    break;
                default:

                    ToolUseComponentFrame.Content = null;
                    break;
            }
        }

    }
}
