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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EditPhotoApp.Views.FeatureWindow
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExportOptionsPage : Page
    {
        public ExportOptionsPage()
        {
            this.InitializeComponent();
        }
        private void ExportButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem sender có phải là MenuFlyoutItem không
            if (sender is MenuFlyoutItem menuFlyoutItem)
            {
                // Lấy văn bản của item được chọn
                string selectedText = menuFlyoutItem.Text;

                // Thực hiện hành động dựa trên item được chọn
                switch (selectedText)
                {
                    case "Send":
                        // Xử lý gửi email
                        break;
                    case "Reply":
                        // Xử lý trả lời email
                        break;
                    case "Reply All":
                        // Xử lý trả lời tất cả
                        break;
                }

                // Hiển thị thông báo hoặc thực hiện các hành động khác
                Console.WriteLine($"Selected: {selectedText}");
            }
        }
    }
    //private void ExportButtonClick(object sender, RoutedEventArgs e)
    //{
    //    // Tìm kiếm loại xuất đã chọn
    //    string selectedExportType = string.Empty;

    //    foreach (var radioButton in this.Children.OfType<RadioButton>())
    //    {
    //        if (radioButton.IsChecked == true)
    //        {
    //            selectedExportType = radioButton.Tag.ToString();
    //            break;
    //        }
    //    }

    //    // Thực hiện xuất (ở đây bạn có thể thêm mã để xuất dữ liệu theo loại đã chọn)
    //    // Ví dụ: Console.WriteLine($"Exporting as {selectedExportType}");

    //    // Đóng cửa sổ
    //    Window.Current.Close();
    //}
   
   }
