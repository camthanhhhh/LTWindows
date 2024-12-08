using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;
using Photo.Models;
using Photo.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using static Photo.ViewModels.MainViewModel;

namespace Photo.ViewModels.Tests
{
    [TestClass()]
    public class MainViewModelTests
    {
        private MainViewModel viewModel;

        [TestInitialize]  // Thay [SetUp] bằng [TestInitialize] cho MSTest
        public void Setup()
        {
            this.viewModel = new MainViewModel();
            var testFilePath = @"D:\test.jpg";

            var viewModel = new MainViewModel();
            viewModel.Image = new Mat(testFilePath);
            viewModel.OriginalImage = new Mat(testFilePath);
        }



        //[TestMethod()]
        //public async void ImportImageAsyncTest()
        //{
        //    // Arrange
        //    var viewModel = new MainViewModel();
        //    var testFilePath = @"D:\test.png";

        //    // Giả lập việc đặt trực tiếp các giá trị cho Image và OriginalImage
        //    await Task.Run(() =>
        //    {
        //        viewModel.Image = new Mat(testFilePath);
        //        viewModel.OriginalImage = new Mat(testFilePath);
        //        viewModel.ImagePath = testFilePath;
        //        viewModel.OperationVisibility = Visibility.Visible;
        //    });

        //    // Act
        //    await viewModel.ImportImageAsync();

        //    // Assert
        //    Assert.IsNotNull(viewModel.Image, "Image should not be null");
        //    Assert.IsNotNull(viewModel.OriginalImage, "OriginalImage should not be null");
        //    Assert.AreEqual(testFilePath, viewModel.ImagePath, "ImagePath should match the selected file path");
        //    Assert.AreEqual(Visibility.Visible, viewModel.OperationVisibility, "OperationVisibility should be visible");
        //    //Assert.Fail();
        //}

        //[TestMethod()]
        //public void SaveImageAsyncTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void CropImageTest()
        {
            var testFilePath = @"D:\test.jpg";

            var viewModel = new MainViewModel();
            viewModel.Image = new Mat(testFilePath);
            viewModel.OriginalImage = new Mat(testFilePath);
            int p1 = 16;
            int p2 = 9;

            // Act
            viewModel.CropImage(p1, p2);

            // Assert
            Assert.AreEqual(1920, viewModel.Image.Width); // Expected width based on 16:9
            Assert.AreEqual(1080, viewModel.Image.Height); // Expected height
        }

        [TestMethod()]
        public void CropImageLevel1Test() //16:9
        {
            var testFilePath = @"D:\test.jpg";

            var viewModel = new MainViewModel();
            viewModel.Image = new Mat(testFilePath);
            viewModel.OriginalImage = new Mat(testFilePath);


            // Act
            viewModel.CropImageLevel1();

            // Assert
            Assert.AreEqual(1920, viewModel.Image.Width); 
            Assert.AreEqual(1080, viewModel.Image.Height); 
        }

        [TestMethod()]
        public void CropImageLevel2Test() //4:3
        {
            var testFilePath = @"D:\test.jpg";

            var viewModel = new MainViewModel();
            viewModel.Image = new Mat(testFilePath);
            viewModel.OriginalImage = new Mat(testFilePath);


            // Act
            viewModel.CropImageLevel2();

            // Assert
            Assert.AreEqual(1600, viewModel.Image.Width);
            Assert.AreEqual(1200, viewModel.Image.Height);
        }

        [TestMethod()]
        public void CropImageLevel3Test() //3:4
        {
            var testFilePath = @"D:\test.jpg";

            var viewModel = new MainViewModel();
            viewModel.Image = new Mat(testFilePath);
            viewModel.OriginalImage = new Mat(testFilePath);


            // Act
            viewModel.CropImageLevel3();

            // Assert
            Assert.AreEqual(900, viewModel.Image.Width);
            Assert.AreEqual(1200, viewModel.Image.Height);
        }

        [TestMethod()]
        public void CropImageLevel4Test() //1:1
        {
            var testFilePath = @"D:\test.jpg";

            var viewModel = new MainViewModel();
            viewModel.Image = new Mat(testFilePath);
            viewModel.OriginalImage = new Mat(testFilePath);


            // Act
            viewModel.CropImageLevel4();

            // Assert
            Assert.AreEqual(1200, viewModel.Image.Width);
            Assert.AreEqual(1200, viewModel.Image.Height);
        }

        [TestMethod()]
        public void RotateLevel1Test()
        {
            viewModel = new MainViewModel();
            viewModel.Image = new Mat(100, 100, MatType.CV_8UC3); 
            viewModel.OriginalImage = viewModel.Image.Clone(); 
                                                              
            Mat originalImage = viewModel.Image.Clone();

            viewModel.RotateLevel1();

            Assert.AreNotEqual(originalImage.Data, viewModel.Image.Data);
        }

        [TestMethod()]
        public void RotateLevel2Test()
        {
            viewModel = new MainViewModel();
            viewModel.Image = new Mat(100, 100, MatType.CV_8UC3); 
            viewModel.OriginalImage = viewModel.Image.Clone(); 
            Mat originalImage = viewModel.Image.Clone();

            viewModel.RotateLevel2();

            Assert.AreNotEqual(originalImage.Data, viewModel.Image.Data);
        }

        
        [TestMethod()]
        public void FlipLevel1Test()
        {

            viewModel = new MainViewModel();
            viewModel.Image = new Mat(100, 100, MatType.CV_8UC3); 
            viewModel.OriginalImage = viewModel.Image.Clone();
            Mat originalImage = viewModel.Image.Clone();

            viewModel.FlipLevel1();
 
            Assert.AreNotEqual(originalImage.Data, viewModel.Image.Data);
        }

        [TestMethod()]
        public void FlipLevel2Test()
        {
            viewModel = new MainViewModel();
            viewModel.Image = new Mat(100, 100, MatType.CV_8UC3); 
            viewModel.OriginalImage = viewModel.Image.Clone(); 
            Mat originalImage = viewModel.Image.Clone();

            viewModel.FlipLevel2();

            Assert.AreNotEqual(originalImage.Data, viewModel.Image.Data);
        }

        [TestMethod()]
        public void FlipLevel3Test()
        {
            viewModel = new MainViewModel();
            viewModel.Image = new Mat(100, 100, MatType.CV_8UC3); 
            viewModel.OriginalImage = viewModel.Image.Clone();
            Mat originalImage = viewModel.Image.Clone();

            viewModel.FlipLevel3();

            Assert.AreNotEqual(originalImage.Data, viewModel.Image.Data);
        }
        [TestMethod()]
        public void PictureStyle_ShouldApplySolidBorder()
        {
            viewModel = new MainViewModel();
            viewModel.Image = new Mat(100, 100, MatType.CV_8UC3);
            Mat originalImage = viewModel.Image.Clone();

            viewModel.PictureStyle(BorderStyle.Solid);

            Assert.AreNotEqual(originalImage.Data, viewModel.Image.Data);
         
        }

        [TestMethod()]
        public void PictureStyle_ShouldApplyCornerWrapBorder()
        {
            viewModel = new MainViewModel();
            viewModel.Image = new Mat(100, 100, MatType.CV_8UC3);
            Mat originalImage = viewModel.Image.Clone();

            viewModel.PictureStyle(BorderStyle.CornerWrap);

            Assert.AreNotEqual(originalImage.Data, viewModel.Image.Data);
        }

        [TestMethod()]
        public void PictureStyle_ShouldApplyDoubleBorder()
        {
            viewModel = new MainViewModel();
            viewModel.Image = new Mat(100, 100, MatType.CV_8UC3); Mat originalImage = viewModel.Image.Clone();

            viewModel.PictureStyle(BorderStyle.DoubleBorder);

            Assert.AreNotEqual(originalImage.Data, viewModel.Image.Data);
        }

        [TestMethod()]
        public void SetBorder_ShouldAddBorderAroundImage()
        {
            viewModel = new MainViewModel();
            viewModel.Image = new Mat(100, 100, MatType.CV_8UC3);
            Mat originalImage = viewModel.Image.Clone();

            viewModel.SetBorder();

            Assert.AreNotEqual(originalImage.Data, viewModel.Image.Data);
            
        
        }
        //[TestMethod()]
        //public void PictureStyleTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void PictureStyleLevel1Test()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void PictureStyleLevel2Test()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void PictureStyleLevel3Test()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void PictureStyleLevel4Test()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void PictureStyleLevel5Test()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void PictureStyleLevel6Test()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void PictureStyleLevel7Test()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void PictureStyleLevel8Test()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void SetBorderTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void AdjustBrightnessContrastTest()
        {
            var testImagePath = @"D:\test.jpg";
            var originalImage = Cv2.ImRead(testImagePath);
            Assert.IsNotNull(originalImage, "Không đọc được ảnh mẫu");

            float brightnessValue = 20; 
            float contrastValue = 50;  

         
            var adjustedImage = MainViewModel.AdjustBrightnessContrast(originalImage, brightnessValue, contrastValue);

            Assert.IsNotNull(adjustedImage, "Ảnh điều chỉnh không được tạo");
            Assert.AreEqual(originalImage.Size(), adjustedImage.Size(), "Kích thước ảnh không khớp");

            var originalPixel = originalImage.At<Vec3b>(0, 0); 
            var adjustedPixel = adjustedImage.At<Vec3b>(0, 0);

            Assert.AreNotEqual(originalPixel, adjustedPixel, "Pixel không thay đổi sau điều chỉnh");
        }

        [TestMethod()]
        public void OnBrightnessContrastChangedTest()
        {
            var viewModel = new MainViewModel();

            var testImagePath = @"D:\test.jpg";
            viewModel.OriginalImage = Cv2.ImRead(testImagePath);
            viewModel.OriginalImageFixed = Cv2.ImRead(testImagePath);

            Assert.IsNotNull(viewModel.OriginalImage, "Không đọc được ảnh gốc");

            viewModel.SelectedBrightnessContrast = new BrightnessContrast
            {
                Brightness = 20, 
                Contrast = 50    
            };

            var args = new PropertyChangedEventArgs("SelectedBrightnessContrast");

            viewModel.OnBrightnessContrastChanged(viewModel, args);

            Assert.IsNotNull(viewModel.Image, "Image không được cập nhật");
            Assert.AreNotEqual(viewModel.OriginalImageFixed, viewModel.Image, "Image không thay đổi sau khi điều chỉnh");

        }


        [TestMethod()]
        public void SelectPencilTool_Test()
        {
            var viewModel = new MainViewModel();

            viewModel.SelectPencilTool();

           
            Assert.IsFalse(viewModel.DrawingStatus.IsEraser, "IsEraser should be false for pencil tool");
            Assert.AreEqual(viewModel.CurrentColor, viewModel.SelectedColor.Value, "CurrentColor should match the selected color");
            Assert.AreEqual(viewModel.StrokeThickness, 2, "StrokeThickness should be 2 for pencil tool");
        }


        [TestMethod()]
        public void SelectBrushTool_Test()
        {
            var viewModel = new MainViewModel();

            viewModel.SelectBrushTool();

            Assert.IsFalse(viewModel.DrawingStatus.IsEraser, "IsEraser should be false for brush tool");
            Assert.AreEqual(viewModel.CurrentColor, viewModel.SelectedColor.Value, "CurrentColor should match the selected color");
            Assert.AreEqual(viewModel.StrokeThickness, 5, "StrokeThickness should be 5 for brush tool");
        }


        [TestMethod()]
        public void SelectEraserTool_Test()
        {
            var viewModel = new MainViewModel();

            viewModel.SelectEraserTool();

            Assert.IsTrue(viewModel.DrawingStatus.IsEraser, "IsEraser should be true for eraser tool");
            Assert.AreEqual(viewModel.CurrentColor, new Scalar(255, 255, 255), "CurrentColor should be white for eraser tool");
            Assert.AreEqual(viewModel.StrokeThickness, 10, "StrokeThickness should be 10 for eraser tool");
        }


        [TestMethod()]
        public void StartDrawingTest()
        {
            var viewModel = new MainViewModel();
            double x = 100, y = 200;

            viewModel.StartDrawing(x, y);

            Assert.IsTrue(viewModel.DrawingStatus.IsDrawing, "IsDrawing should be true after calling StartDrawing");
            Assert.AreEqual(viewModel.DrawingStatus.LastX, x, "LastX should be updated to the starting X coordinate");
            Assert.AreEqual(viewModel.DrawingStatus.LastY, y, "LastY should be updated to the starting Y coordinate");
        }

       

        [TestMethod()]
        public void StopDrawingTest()
        {
            var viewModel = new MainViewModel();
            viewModel.StartDrawing(100, 200);  

            viewModel.StopDrawing();

            Assert.IsFalse(viewModel.DrawingStatus.IsDrawing, "IsDrawing should be false after calling StopDrawing");
        }
        [TestMethod]
        public void DrawLineOnMat_Test()
        {
            var testFilePath = @"D:\test.jpg";
            var viewModel = new MainViewModel();

            viewModel.Image = new Mat(testFilePath);
            viewModel.OriginalImage = new Mat(testFilePath);
        
            var initialImage = viewModel.Image.Clone(); 
            var startPoint = new Point(100, 200);
            var endPoint = new Point(150, 250);

         
            viewModel.DrawLineOnMat(startPoint, endPoint);

            Assert.AreNotEqual(viewModel.Image, initialImage, "The image should have changed after drawing a line");
        }

        [TestMethod()]
        public void SaveColorTest()
        {
 
            var viewModel = new MainViewModel();
            var testColor = Color.FromArgb(255, 255, 0, 0); 

            viewModel.SaveColor(testColor);

        
            Assert.AreEqual(Scalar.Red, viewModel.SelectedColor.Value, "SelectedColor.Value không đúng");
            Assert.AreEqual("RGB(255,0,0)", viewModel.SelectedColor.Name, "Tên của màu không đúng");
            Assert.AreEqual(Scalar.Red, viewModel.CurrentColor, "CurrentColor không đúng");
        }

        [TestMethod()]
        public void OpenGridTextBlockTest()
        {
           
                var viewModel = new MainViewModel();
                string expectedText = "Hello World";
                string expectedFont = "Arial";
                int expectedSize = 16;
                var expectedColor = new Scalar(255, 0, 0); 
                viewModel.SelectedColor = new ColorItem { Value = expectedColor };

                viewModel.OpenGridTextBlock(expectedText, expectedFont, expectedSize);

                Assert.IsTrue(viewModel.AddTextStatus.IsAddText, "IsAddText should be true.");
                Assert.IsFalse(viewModel.AddTextStatus.isDragging, "isDragging should be false.");
                Assert.AreEqual(expectedSize, viewModel.AddTextStatus.FontSize, "FontSize should match the expected size.");
                Assert.AreEqual(expectedFont, viewModel.AddTextStatus.FontFamily, "FontFamily should match the expected font.");
                Assert.AreEqual(expectedText, viewModel.AddTextStatus.Text, "Text should match the expected text.");
                Assert.AreEqual(expectedColor, viewModel.AddTextStatus.Color, "Color should match the selected color.");
            

        }
        [TestMethod]
        public void AddTextToMat_ValidParameters_ShouldDrawTextOnImage()
        {
           
            var viewModel = new MainViewModel();
            viewModel.Image = new Mat(500, 500, MatType.CV_8UC3, new Scalar(0, 0, 0)); // Ảnh nền đen
            viewModel.OriginalImage = viewModel.Image;
            var position = new Point(100, 100);
            string text = "Test";
            string font = "Simplex";
            int size = 30;

            viewModel.AddTextToMat(position, text, font, size);

            Assert.IsNotNull(viewModel.Image, "Image should not be null after drawing text.");
            Assert.AreNotEqual(viewModel.OriginalImage, viewModel.Image, "Image should be updated after drawing text.");
        }

        [TestMethod]
      
        public void AddTextToMat_EmptyOrNullText_ShouldNotModifyImage()
        {
            var viewModel = new MainViewModel();
            var originalImage = new Mat(500, 500, MatType.CV_8UC3, new Scalar(0, 0, 0)); // Ảnh nền đen
            viewModel.Image = originalImage.Clone();
            var position = new Point(100, 100);
            string text = ""; 
            string font = "Simplex";
            int size = 30;

            viewModel.AddTextToMat(position, text, font, size);

            Mat diff = new Mat();
            Cv2.Absdiff(viewModel.Image, originalImage, diff); 

            Mat diffGray = new Mat();
            Cv2.CvtColor(diff, diffGray, ColorConversionCodes.BGR2GRAY);

            int nonZeroCount = Cv2.CountNonZero(diffGray);

            Assert.AreEqual(0, nonZeroCount, "Image should remain unchanged when text is empty or null.");
        }



        
    }
}