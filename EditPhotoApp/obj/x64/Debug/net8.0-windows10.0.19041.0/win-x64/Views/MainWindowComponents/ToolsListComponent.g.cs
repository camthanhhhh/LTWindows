﻿#pragma checksum "C:\Users\THUY TRANG\Downloads\Code chính\EditPhotoApp\Views\MainWindowComponents\ToolsListComponent.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0AABF6EEDC51AFEAEE6C1E425E5FCE23BF3FEEAD6B464FF1B12CFEF7818102C9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EditPhotoApp.Views.MainWindowComponents
{
    partial class ToolsListComponent : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2409")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\MainWindowComponents\ToolsListComponent.xaml line 15
                {
                    this.BrightnessContrastButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.BrightnessContrastButton).Click += this.BrightnessContrastButton_Click;
                }
                break;
            case 3: // Views\MainWindowComponents\ToolsListComponent.xaml line 66
                {
                    this.InsertPictureButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.InsertPictureButton).Click += this.InsertPictureButton_Click;
                }
                break;
            case 4: // Views\MainWindowComponents\ToolsListComponent.xaml line 91
                {
                    this.InsertShapesButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.InsertShapesButton).Click += this.InsertShapesButton_Click;
                }
                break;
            case 5: // Views\MainWindowComponents\ToolsListComponent.xaml line 114
                {
                    this.DrawingCanvas = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Canvas>(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }


        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2409")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
