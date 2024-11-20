using EditPhotoApp.ViewModels.MainWindowComponents.ContentComponents.ToolFeatureViewModel;
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

namespace EditPhotoApp.Views.FeaturePage.ContentComponents.FeaturePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DrawingToolPage : Page
    {
        DrawingToolViewModel viewModel;
        public DrawingToolPage(DrawingToolViewModel drawingToolViewModel)
        {
            viewModel = drawingToolViewModel;
            this.InitializeComponent();
            

        }
        private void SelectPencilTool_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SelectPencilTool();
        }
        private void SelectBrushTool_Click(object sender, RoutedEventArgs e)
        {
            viewModel?.SelectBrushTool();
        }
        private void SelectEraserTool_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SelectEraserTool();
        }
        private void AddTextButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddText();
        }
    }
}
