using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;

namespace MAUI_Counter
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Count> Counters;

        DataService dataService = new();


        public MainPage()
        {
            InitializeComponent();
            var loaded = dataService.Load();
            Counters = new ObservableCollection<Count>(loaded);
            CountersList.ItemsSource = Counters;
        }
        protected override void OnDisappearing()
        {
            dataService.Save(Counters.ToList());
        }
        private void IncreasCounter(object? sender, EventArgs e)
        {

            if (sender is Button btn && btn.CommandParameter is Count counter)
            {
                counter.Value++;
                dataService.Save(Counters.ToList());
               
            }


        }
        private void DecreasCounter(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is Count counter)
            {
                counter.Value--;
                dataService.Save(Counters.ToList());    
            }
        }




        private void OnAddCounterClicked(object sender, EventArgs e)
        {
            var name = NewCounterName.Text;
            var valueText = NewCounterValue.Text;
            if (!string.IsNullOrWhiteSpace(name) && int.TryParse(valueText, out int value))
            {
                Counters.Add(new Count
                {
                    Name = name,
                    Value = value
                });
                NewCounterName.Text = string.Empty;
                NewCounterValue.Text= string.Empty;
            }
            else
            {
                String alert = "";
                if(string.IsNullOrWhiteSpace(name))
                {
                    alert = " Enter a name for counter";
                }
                
                if (!int.TryParse(valueText, out _))
                {
                    if (alert == "")
                    {
                        alert = "Enter the correct counter start number";
                    }
                    else
                    {
                        alert += " and the correct counter start number";
                    };
                }
                

                DisplayAlert("Error", alert, "OK");
            }
            
        }

    }
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
    
    public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}