using EditPhotoApp.ViewModels;
using EditPhotoApp.Views.FeaturePage;
using EditPhotoApp.Views.MainWindowComponents;
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
        TopBarComponent topBarComponent;
        ToolsListComponent toolsListComponent;
        Shapes shapesPage;
        DrawingToolViewModel drawingToolViewModel;
        DrawingToolPage drawingToolPage;
        public ImageEditComponent ImageEditPage => ImageEditComponentFrame.Content as ImageEditComponent;

        public MainWindow()
        {
            this.InitializeComponent();

            toolsListComponent = new ToolsListComponent();
            toolsListComponent.ToolSelected += OnToolSelected; // Đăng ký sự kiện ở đây
            brightnessAndContrastViewModel = new BrightnessAndContrastViewModel();
            topBarComponent = new TopBarComponent(brightnessAndContrastViewModel);
            brightnessAndContrastPage = new BrightnessAndContrastPage(brightnessAndContrastViewModel);
            shapesPage = new Shapes();

            

            // Đảm bảo rằng các Frame đã được khởi tạo trước khi gọi Navigate
            this.TopBarComponentFrame.Content = topBarComponent;
            this.ToolsComponentFrame.Content = toolsListComponent; // Thiết lập nội dung trực tiếp
            this.ImageEditComponentFrame.Navigate(typeof(ImageEditComponent));
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

                    ToolUseComponentFrame.Content = shapesPage;
                    break;
            }
        }

    }
}
