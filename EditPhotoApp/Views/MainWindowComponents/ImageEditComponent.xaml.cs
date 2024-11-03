using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using EditPhotoApp.ViewModels;

namespace EditPhotoApp.Views.MainWindowComponents
{
    public sealed partial class ImageEditComponent : Page
    {
        public Image saveImage => mainImage; // 'mainImage' là tên của Image trong XAML.
        public Canvas drawingCanvas => DrawingCanvas; // 'DrawingCanvas' là tên của Canvas trong XAML.

        public ImageEditComponent()
        {
            this.InitializeComponent();
        }

        // Các phương thức sự kiện sẽ được chuyển tiếp tới ViewModel
        
    }
}
