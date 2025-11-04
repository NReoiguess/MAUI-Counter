using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Counter.Model
{
    public class ColorRGB : INotifyPropertyChanged
    {
        private int red;
        public int Red
        {
            get => red;
            set
            {
                if (red != value)
                {
                    red = value;
                    OnPropertyChanged(nameof(Red));
                    OnPropertyChanged(nameof(Value));

                }
            }
        }

        private int green;
        public int Green
        {
            get => green;
            set
            {
                if (green != value)
                {
                    green = value;
                    OnPropertyChanged(nameof(Green));
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        private int blue;
        public int Blue

        {
            get => blue;
            set
            {
                if (blue != value)
                {
                    blue = value; ;
                    OnPropertyChanged(nameof(Blue));
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public string Value => Microsoft.Maui.Graphics.Color
                                   .FromRgb(Red, Green, Blue)
                                   .ToArgbHex();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
