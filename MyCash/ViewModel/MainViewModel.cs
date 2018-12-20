using GalaSoft.MvvmLight.Command;
using MyCash.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MyCash.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MyOrders myOrders { get; set; } = new MyOrders();
        private JsonOperations jsonOperations = new JsonOperations();

        private ObservableCollection<MyOrders> MyOrdersCollections { get; set; } = new ObservableCollection<MyOrders>();
        private ObservableCollection<MyOrders> MyOrdersComplete { get; set; }
        public ObservableCollection<string> Lessons { get; set; } = new ObservableCollection<string>();
        public List<int> Variant { get; set; } = new List<int>();
        public List<int> LabWork { get; set; } = new List<int>();

        private ObservableCollection<MyOrders> _MyColl;
        public ObservableCollection<MyOrders> MyColl
        {
            get => _MyColl;
            set
            {
                _MyColl = value;
                OnPropertyChanged();
            }
        }

        // закрытие и открытие окна popup
        private bool _OpenDialog;
        public bool OpenDialog
        {
            get => _OpenDialog;
            set
            {
                _OpenDialog = value;
                OnPropertyChanged();
            }
        }

        // свойство хранения заработанных денег
        private int _Cash;
        public int Cash
        {
            get => _Cash;
            set
            {
                _Cash = value;
                OnPropertyChanged("Cash");
            }
        }

        // свойства для создания нового обьекта
        private int _IndexSubject = -1;
        public int IndexSubject
        {
            get => _IndexSubject;
            set
            {
                _IndexSubject = value;
                OnPropertyChanged();
            }
        }

        private int _IndexVariant = -1;
        public int IndexVariant
        {
            get => _IndexVariant;
            set
            {
                _IndexVariant = value;
                OnPropertyChanged();
            }
        }

        private int _IndexLrSubject = -1;
        public int IndexLrSubject
        {
            get => _IndexLrSubject;
            set
            {
                _IndexLrSubject = value;
                OnPropertyChanged();
            }
        }

        private string _BtnText;
        public string BtnText
        {
            get => _BtnText;
            set
            {
                _BtnText = value;
                OnPropertyChanged();
            }
        }

        // для контекстного меню (Оплаччено), возможность отмечать только НЕ оплаченные
        private bool _EnabledItem = true;
        public bool EnabledItem
        {
            get => _EnabledItem;
            set
            {
                _EnabledItem = value;
                OnPropertyChanged();
            }
        }

        private DateTime _Date = DateTime.Now;
        public DateTime Date
        {
            get => _Date;
            set
            {
                _Date = value;
                OnPropertyChanged();
            }
        }

        private MyOrders _SelectedItem;
        public MyOrders SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }

        // были ли произведены изменения
        private bool Changes { get; set; }

        private bool ChangesComplete { get; set; } = false;
        private bool? AddOrEdit { get; set; } = null;

        public MainViewModel()
        {
            MyOrdersCollections = jsonOperations.DeserializingCollectionsObjects("MyOrders.json");
            MyColl = MyOrdersCollections = new ObservableCollection<MyOrders>(MyOrdersCollections.OrderBy(x => x.Status));

            Lessons = jsonOperations.DeserializingCollectionsSubjects("Subjects.json");

            // занесение данных
            SetNumbers(LabWork, 15);
            SetNumbers(Variant, 25);
        }

        public ICommand SetCollection => new RelayCommand(() =>
        {
            MyColl = MyOrdersCollections;
            EnabledItem = true;
        });

        public ICommand SetCompleteCollection => new RelayCommand(() =>
        {
            if (MyOrdersComplete == null)
            {
                MyOrdersComplete = new ObservableCollection<MyOrders>();
                MyColl = MyOrdersComplete = jsonOperations.DeserializingCollectionsObjects("MyOrdersComplete.json");
            } 
            else
                MyColl = MyOrdersComplete;

            GetPay();
            
            EnabledItem = false;
        });

        public ICommand CloseProgram => new RelayCommand(() =>
        {
            //if (Changes)
                jsonOperations.SerializingCollectionsObjects("MyOrders.json", MyOrdersCollections);
            if(ChangesComplete)
                jsonOperations.SerializingCollectionsObjects("MyOrdersComplete.json", MyOrdersComplete);
            Environment.Exit(0);
        });

        public ICommand MinimizedProgram => new RelayCommand(() => {
            var app = Application.Current.Windows[0];
            app.WindowState = WindowState.Minimized;
        });

        public ICommand Complete => new RelayCommand(() =>
        {
            if (SelectedItem != null)
            {
                if (MyOrdersComplete.Count == 0)
                    MyOrdersComplete = jsonOperations.DeserializingCollectionsObjects("MyOrdersComplete.json");

                SelectedItem.Status = true;
                MyOrdersComplete.Add(SelectedItem);
                MyOrdersCollections.Remove(SelectedItem);
                Changes = true;
                ChangesComplete = true;
            }
        });

        public ICommand Delete => new RelayCommand(() =>
        {
            if (SelectedItem != null)
            {
                MyColl.Remove(SelectedItem);
                GetPay();
                if (MyColl == MyOrdersCollections)
                    Changes = true;
                else
                    ChangesComplete = true;
            }
        });

        public ICommand OpenPopupToAdd => new RelayCommand(() =>
        {
            BtnText = "Добавить";
            AddOrEdit = true;
            OpenDialog = true;
        });

        public ICommand CreateOrChangeElement => new RelayCommand(() => AddOrEditElement());

        public void AddOrEditElement()
        {
            if (AddOrEdit == true)
            {
                if (ChechData())
                {
                    MyOrdersCollections.Add(new MyOrders(
                        myOrders.FIO, Lessons[IndexSubject], LabWork[IndexLrSubject], Variant[IndexVariant],
                        myOrders.Cost, myOrders.Report, false, Date, myOrders.Description
                     ));
                    RefreshMyOrdersCollections();
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }
            }
            else
            {
                if (ChechData())
                {
                    var t = MyColl.IndexOf(SelectedItem);
                    MyColl[t] = new MyOrders(
                        myOrders.FIO, Lessons[IndexSubject], LabWork[IndexLrSubject], Variant[IndexVariant],
                        myOrders.Cost, myOrders.Report, SelectedItem.Status, Date, myOrders.Description
                     );
                } else MessageBox.Show("Заполните все поля");
            }

            OpenDialog = false;
            Changes = true;
            AddOrEdit = null;
            ResetProperty();
        }

        public ICommand Edit => new RelayCommand(() => {
            if (SelectedItem != null)
            {
                myOrders.FIO = SelectedItem.FIO;
                IndexSubject = Lessons.IndexOf(SelectedItem.Subject);
                IndexLrSubject = LabWork.IndexOf(SelectedItem.LrSubject);
                IndexVariant = Variant.IndexOf(SelectedItem.Variant);
                myOrders.Cost = SelectedItem.Cost;
                myOrders.Report = SelectedItem.Report;
                myOrders.Description = SelectedItem.Description;
                Date = SelectedItem.Date;

                BtnText = "Изменить";
                AddOrEdit = false;
                OpenDialog = true;
            }
        });

        public ICommand Refresh => new RelayCommand(() => RefreshMyOrdersCollections());

        private void RefreshMyOrdersCollections()
        {
            if (MyColl == MyOrdersCollections)
            {
                MyOrdersCollections = new ObservableCollection<MyOrders>(MyOrdersCollections.OrderBy(x => x.Status));
                MyColl = MyOrdersCollections;
            }
        }

        private void SetNumbers(List<int> coll, int size)
        {
            for (int i = 1; i <= size; i++)
                coll.Add(i);
        }

        private void GetPay()
        {
            Cash = MyColl.Sum(m => m.Cost);
        }

        private bool ChechData()
        {
            if (myOrders.FIO != "" && IndexSubject != -1 && IndexLrSubject != -1 && IndexVariant != -1 && myOrders.Cost != 0)
                return true;
            else
                return false;
        }

        private void ResetProperty()
        {
            myOrders.FIO = null;
            IndexSubject = -1;
            IndexLrSubject = -1;
            IndexVariant = -1;
            myOrders.Cost = 0;
            myOrders.Report = false;
            myOrders.Description = null;
            Date = DateTime.Now;
        }

    }
}   