using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Photo.Models
{
    public class ColorData
    {
        public string LightRegionColor { get; set; }
        public string DarkRegionColor { get; set; }
        public string LightBaseColor { get; set; }
        public string DarkBaseColor { get; set; }
        public string LightPrimaryColor { get; set; }
        public string DarkPrimaryColor { get; set; }
    }

    public static class ColorManager
    {
        public static async Task<ColorData> LoadColorDataAsync()
        {
            try
            {
                // Xác định đường dẫn tệp cho ColorData.json
                var filePath = Path.Combine(AppContext.BaseDirectory, "Assets", "ColorData.json");

                // Kiểm tra xem tệp có tồn tại không trước khi mở
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Tệp dữ liệu màu không tìm thấy", filePath);
                }

                // Tải tệp một cách bất đồng bộ
                using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return await JsonSerializer.DeserializeAsync<ColorData>(stream);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi (ví dụ: tệp không tìm thấy, lỗi giải mã JSON)
                Console.WriteLine($"Lỗi khi tải dữ liệu màu: {ex.Message}");
                return null; // Bạn có thể trả về giá trị mặc định hoặc xử lý theo cách khác
            }
        }
    }
}
