using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyCash.Storage;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MyCash.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<MyOrders> myOrders { get; set; }
        public ObservableCollection<string> tt { get; set; }
        public ObservableCollection<int> vv { get; set; }
        public ObservableCollection<int> lr { get; set; }


        public string FIO { get; set; }
        public string Subject { get; set; }
        public int LrSubject { get; set; }
        public int Variant { get; set; }
        public int Cost { get; set; }
        public bool Report { get; set; }
        public bool Status { get; set; }

        public int indexSubject { get; set; } = -1;
        public int indexVariant { get; set; } = -1;
        public int indexLrSubject { get; set; } = -1;

        public MainViewModel()
        {
            myOrders = new ObservableCollection<MyOrders>
            {
                new MyOrders("Максим", "С#", 3, 3, 150, false, true),
                new MyOrders("jack", "sddfg", 3, 3, 150, true, false),
                new MyOrders("Mosin", "sddfg", 3, 3, 150, false, true)
            };

            tt = new ObservableCollection<string> { "C#", "МСД", "Дужий" };
            vv = new ObservableCollection<int> { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25 };
            lr = new ObservableCollection<int> { 1,2,3,4,5,6,7,8,9,10 };
        }

        public ICommand Test = new RelayCommand(() => MessageBox.Show("sdf"));
        public ICommand AddNew => new RelayCommand(() => Add());
        public void Add()
        {
            myOrders.Add(new MyOrders(FIO, tt[indexSubject], lr[indexLrSubject], vv[indexVariant], Cost, Report, false));
        }
    }
}   