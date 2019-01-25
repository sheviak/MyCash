using GalaSoft.MvvmLight.Command;
using MyCash.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;
using MyCash.Windows;

namespace MyCash.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MyOrders myOrders { get; set; } = new MyOrders();
        private JsonOperations jsonOperations = new JsonOperations();

        private ObservableCollection<MyOrders> MyOrdersCollections { get; set; } = new ObservableCollection<MyOrders>();
        private ObservableCollection<MyOrders> MyOrdersComplete { get; set; }
        public ObservableCollection<string> Lessons { get; set; }
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

        // открытие и закрытие окна popup (добавления и редактирвоания)
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

        // подсчет заработанных денег
        private int _Cash;
        public int Cash
        {
            get => _Cash;
            set
            {
                _Cash = value;
                OnPropertyChanged();
            }
        }

        // индексы выбора предмета, варианта и лабы
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

        // äëÿ êîíòåêñòíîãî ìåíþ (Îïëà÷÷åíî), âîçìîæíîñòü îòìå÷àòü òîëüêî ÍÅ îïëà÷åííûå
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

        // áûëè ëè ïðîèçâåäåíû èçìåíåíèÿ
        private bool Changes { get; set; }

        private bool ChangesComplete { get; set; } = false;
        private bool? AddOrEdit { get; set; } = null;

        public MainViewModel()
        {
            MyOrdersCollections = jsonOperations.DeserializingCollectionsObjects("MyOrders.json");
            MyColl = MyOrdersCollections = new ObservableCollection<MyOrders>(MyOrdersCollections.OrderBy(x => x.Status));

            Lessons = MediatorClass.Lessons = jsonOperations.DeserializingCollectionsSubjects("Subjects.json");
            
            // установление кол-ва лаб и вариантов
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
               // MyOrdersComplete = new ObservableCollection<MyOrders>();
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
                if (MyOrdersComplete == null) //  || MyOrdersComplete.Count == 0
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

        public ICommand ClosePopupToAdd => new RelayCommand(() => OpenDialog = false);

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
                    MessageBox.Show("Заполните все поля!");
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
                } else MessageBox.Show("Заполните все поля!");
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
            for (int i = 0; i <= size; i++)
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


        public ICommand ShowGraphics => new RelayCommand(() =>
        {
            if (MyColl.Count == 0)
            {
                return;
            }

            var obj = MyColl
                .GroupBy(p => p.Date.ToString("MMMM"), p => p.Cost)
                .Select(x => new { Mounth = x.Key, Sum = x.Sum()} ).ToList();

            // добавление обьекутов в серию
            foreach (var item in obj)
            {
                MediatorClass.LabelsForColumn.Add(item.Mounth);
                MediatorClass.ColumnSeries[0].Values.Add(item.Sum);
            }

            obj.Clear(); // Удаляет из коллекции List<T> все элементы.
            obj.TrimExcess(); // Задает емкость, равную фактическому числу элементов в списке

            //выбор месяцов без повторений
            //var ttt = MyColl.Select(x => x.Date.ToString("MMMM")).Distinct();

            var pieCollections = MyColl
                .GroupBy(p => p.Subject)
                .Select(x => new { Subjext = x.Key, Count = x.Count() }).ToList();

            foreach (var item in pieCollections)
            {
                MediatorClass.PieSeries.Add(
                    new PieSeries
                    {
                        Title = item.Subjext,
                        Values = new ChartValues<int> { item.Count },
                        DataLabels = true,
                        //процентное отображение на графике
                        LabelPoint = chartPoint => string.Format("{0} шт.\n({1:P})", chartPoint.Y, chartPoint.Participation),
                        FontSize = 12,
                        PushOut = 8,
                        Stroke = null
                    }
                );
            }

            pieCollections.Clear();
            pieCollections.TrimExcess();

            GraphWindow graph = new GraphWindow();
            graph.ShowDialog();
        });

        public ICommand ShowSettings => new RelayCommand(() =>
        {
            var settings = new SettingWindow();
            settings.ShowDialog();
        });

    }
}   