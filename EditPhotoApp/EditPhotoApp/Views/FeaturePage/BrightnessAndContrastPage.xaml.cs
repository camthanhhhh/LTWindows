using EditPhotoApp.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EditPhotoApp.Views.FeaturePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BrightnessAndContrastPage : Page
    {
        public BrightnessAndContrastPage()
        {
            this.InitializeComponent();
        }
        


        private void OnSliderValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            BrightnessAndContrastViewModel viewModel = new BrightnessAndContrastViewModel();
            float brightness = (float)BrightnessSlider.Value;
            float contrast = (float)ContrastSlider.Value;
            viewModel.UpdateImage(brightness, contrast);
        }
    }
}