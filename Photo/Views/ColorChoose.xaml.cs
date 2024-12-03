using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Photo.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Photo.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorChoose : Window
    {
        public ColorChoose(object dataContext)
        {
            this.InitializeComponent();
            RootPanel.DataContext = dataContext;

        }
        private void OnSaveColorClicked(object sender, RoutedEventArgs e)
        {
            var selectedColor = ColorPicker.Color;
            // Bạn có thể truyền selectedColor về ViewModel nếu cần.
            var vm = (MainViewModel)RootPanel.DataContext;
            vm.SaveColor(ColorPicker.Color);
            // Đóng cửa sổ sau khi chọn màu.
            this.Close();
        }
    }
}
