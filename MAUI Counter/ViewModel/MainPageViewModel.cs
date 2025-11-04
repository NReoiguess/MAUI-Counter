using MAUI_Counter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUI_Counter.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly DataService dataService = new();
 
        public ObservableCollection<Count> Counters { get; }
 
        private string newCounterName;
        public string NewCounterName
        {
            get => newCounterName;
            set
            {
                if (newCounterName != value)
                {
                    newCounterName = value;
                    OnPropertyChanged(nameof(NewCounterName));
                }
            }
        }
 
        private string newCounterValue;
        public string NewCounterValue
        {
            get => newCounterValue;
            set
            {
                if (newCounterValue != value)
                {
                    newCounterValue = value;
                    OnPropertyChanged(nameof(NewCounterValue));
                }
            }
        }
 
        public ColorRGB NewCounterColor { get; } = new();
 
        private Color previewColor;
        public Color PreviewColor
        {
            get => previewColor;
            set
            {
                if (previewColor != value)
                {
                    previewColor = value;
                    OnPropertyChanged(nameof(PreviewColor));
                }
            }
        }
 
        public ICommand IncreaseCounterCommand { get; }
        public ICommand DecreaseCounterCommand { get; }
        public ICommand ResetCounterCommand { get; }
        public ICommand DeleteCounterCommand { get; }
        public ICommand AddCounterCommand { get; }
        public ICommand BlendColorCommand { get; }
        public MainPageViewModel()
        {
            var loaded = dataService.Load();
            Counters = new ObservableCollection<Count>((IEnumerable<Count>)loaded);

            IncreaseCounterCommand = new Command<Count>(IncreaseCounter);
            DecreaseCounterCommand = new Command<Count>(DecreaseCounter);
            ResetCounterCommand = new Command<Count>(ResetCounter);
            DeleteCounterCommand = new Command<Count>(DeleteCounter);
            AddCounterCommand = new Command(AddCounter);
            BlendColorCommand = new Command(BlendColor);

            NewCounterColor.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ColorRGB.Red) ||
                    e.PropertyName == nameof(ColorRGB.Green) ||
                    e.PropertyName == nameof(ColorRGB.Blue))
                {
                    BlendColor();
                }
            };
            BlendColor();
        }

        private void IncreaseCounter(Count counter)
        {
            if (counter != null)
            {
                counter.Value++;
                dataService.Save(Counters.ToList());
            }
        }

        private void DecreaseCounter(Count counter)
        {
            if (counter != null)
            {
                counter.Value--;
                dataService.Save(Counters.ToList());
            }
        }

        private void ResetCounter(Count counter)
        {
            if (counter != null)
            {
                counter.Reset();
                dataService.Save(Counters.ToList());
            }
        }

        private void DeleteCounter(Count counter)
        {
            if (counter != null)
            {
                Counters.Remove(counter);
                dataService.Save(Counters.ToList());
            }
        }

        private void AddCounter()
        {
            var name = NewCounterName;
            var valueText = NewCounterValue;
            var color = new ColorRGB
            {
                Red = NewCounterColor.Red,
                Green = NewCounterColor.Green,
                Blue = NewCounterColor.Blue
            };

            if (!string.IsNullOrWhiteSpace(name) && int.TryParse(valueText, out int value))
            {
                Counters.Add(new Count(value, name, color));
                NewCounterName = string.Empty;
                NewCounterValue = string.Empty;
                NewCounterColor.Red = 0;
                NewCounterColor.Green = 0;
                NewCounterColor.Blue = 0;
                dataService.Save(Counters.ToList());
            }
            else
            {
                string alert = "";
                if (string.IsNullOrWhiteSpace(name))
                    alert = "Podaj nazwę licznika";
                if (!int.TryParse(valueText, out _))
                    alert = string.IsNullOrEmpty(alert) ? "Podaj poprawną wartość początkową" : alert + " oraz poprawną wartość początkową";

            }
        }

        private void BlendColor()
        {
            PreviewColor = Color.FromRgb(
                NewCounterColor.Red,
                NewCounterColor.Green,
                NewCounterColor.Blue);
        }

        public void SaveCounters()
        {
            dataService.Save(Counters.ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}

