using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Photo.HostBuilders;
using Microsoft.Windows.ApplicationModel.Resources;
using Photo.Models;

namespace Photo
{
    public partial class App : Application
    {
        public static void ApplyTheme(ColorData colorData, bool isDarkMode)
        {
            // Truy cập tài nguyên của ứng dụng
            var resources = Application.Current.Resources;

            // Áp dụng màu theo chế độ sáng/tối
            if (isDarkMode)
            {
                resources["RegionColor"] = colorData.DarkRegionColor;
                resources["BaseColor"] = colorData.DarkBaseColor;
                resources["PrimaryColor"] = colorData.DarkPrimaryColor;
            }
            else
            {
                resources["RegionColor"] = colorData.LightRegionColor;
                resources["BaseColor"] = colorData.LightBaseColor;
                resources["PrimaryColor"] = colorData.LightPrimaryColor;
            }
        }

        private readonly IHost _host;
        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddViewModels()
                .AddViews();
        }
        public App()
        {
            this.InitializeComponent();
            _host = CreateHostBuilder().Build();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Activate();
        }
        public void SetLanguage(string language)
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = language;
        }
        public static MainWindow MainWindow { get; private set; }
    }
}
