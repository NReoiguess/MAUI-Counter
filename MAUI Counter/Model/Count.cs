using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Counter.Model
{
    public class Count : INotifyPropertyChanged
    {
        public int value;
        public int Value
        {
            get => value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }

        }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ColorRGB Color { get; set; }

        public int initialValue { get; set; }
        public string initialName { get; set; }
        public ColorRGB initialColor { get; set; }

        public Count(int value, string name, ColorRGB color)
        {
            this.value = value;
            this.name = name;
            this.Color = color;
            this.initialValue = value;
            this.initialName = name;
            this.initialColor = new ColorRGB() { Red = color.Red, Blue = color.Blue, Green = color.Green };
        }
        public Count()
        {

            Color = new ColorRGB()
            {
                Red = 0,
                Green = 0,
                Blue = 0
            };
            initialColor = new ColorRGB()
            {
                Red = 0,
                Green = 0,
                Blue = 0
            };
        }
        public void Reset()
        {
            Value = initialValue;
            Name = initialName;
            Color.Red = initialColor.Red;
            Color.Green = initialColor.Green;
            Color.Blue = initialColor.Blue;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
