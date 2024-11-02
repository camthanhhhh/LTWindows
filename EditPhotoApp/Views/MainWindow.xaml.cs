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
        public ToolUseComponent ToolUsePage => ToolUseComponentFrame.Content as ToolUseComponent;

        public ImageEditComponent ImageEditPage => ImageEditComponentFrame.Content as ImageEditComponent;

        public MainWindow()
        {
            this.InitializeComponent();
            var toolsList = new ToolsListComponent();
            toolsList.ToolSelected += OnToolSelected; // Subscribe to the event here

            this.TopBarComponentFrame.Navigate(typeof(TopBarComponent));
            this.ToolsComponentFrame.Content = toolsList; // Set content directly
            //this.ToolUseComponentFrame.Navigate(typeof(ToolUseComponent));
            this.ImageEditComponentFrame.Navigate(typeof(ImageEditComponent));
        }

        private void OnToolSelected(string tool)
        {
            // Navigate to the appropriate tool page based on the selected tool
            switch (tool)
            {
                case "BrightnessContrast":
                    ToolUseComponentFrame.Navigate(typeof(BrightnessAndContrastPage));
                    break;
                // Add cases for other tools here
                default:
                    break;
            }
        }
    }
}
