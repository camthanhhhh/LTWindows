using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photo.Models
{
    public class BrightnessContrast : ObservableObject
    {
        public float Brightness
        {
            get => brightness;
            set
            {
                brightness = value; OnPropertyChanged(nameof(Brightness));
            }
        }
            public float Contrast
        {
            get => contrast;
            set { contrast = value; OnPropertyChanged(nameof(Contrast));}
        }
        public float brightness { get; set; }
        public float contrast { get; set; }
    }
}
