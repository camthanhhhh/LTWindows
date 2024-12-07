using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI;
using OpenCvSharp;

public class AddText : ObservableObject
{
    public bool IsDragging
    {
        get => isDragging;
        set
        {
            isDragging = value;
            OnPropertyChanged(nameof(IsDragging));
        }
    }

    public bool IsAddText
    {
        get => isAddText;
        set
        {
            isAddText = value;
            OnPropertyChanged(nameof(IsAddText));   
        }
    }
    public string Text
    {
        get => text;
        set
        {
            text = value;
            OnPropertyChanged(nameof(Text));
        }
    }
    public Scalar Color
    {
        get => color;
        set
        {
            color= value;
            OnPropertyChanged(nameof(Color));
        }
    }
    public int FontSize
    {
        get => fontSize;
        set
        {
            fontSize = value;
            OnPropertyChanged(nameof(FontSize));
        }
    }
    public string FontFamily
    {
        get => fontFamily;
        set
        {
            fontFamily = value;
            OnPropertyChanged(nameof(FontFamily));
        }
    }
    public bool isDragging { get; set; }
    public bool isAddText { get; set; }
    public string text { get; set; }
    public Scalar color { get; set; }
    public int fontSize { get; set; }
    public string fontFamily { get; set; }
    
 

}
