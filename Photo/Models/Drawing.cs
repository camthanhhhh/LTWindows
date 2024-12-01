using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photo.Models
{
    public class Drawing: ObservableObject
    {
        public bool IsDrawing
        {
            get=> isDrawing;
            set
            {
                isDrawing = value;
                OnPropertyChanged(nameof(IsDrawing));
            }
        }
        public bool IsEraser
        {
            get => isEraser;
            set
            {
                isEraser = value;  
                OnPropertyChanged(nameof(IsEraser));
            }
        }

        public double LastX
        {
            get => lastX;
            set
            {
                lastX = value;
                OnPropertyChanged(nameof(LastX));   
            }
        }
        public double LastY
        {
            get => lastY;
            set
            {
                lastY = value;
                OnPropertyChanged(nameof(LastY));
            }
        }
        public bool Status
        {
            get => status;
            set
            {
                status= value;
                OnPropertyChanged(nameof(Status));
            }
        }
        private bool isDrawing;
        private bool isEraser;
        private double lastX;
        private double lastY;
        private bool status;
    }
}
