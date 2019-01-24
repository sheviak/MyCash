using GalaSoft.MvvmLight.Command;
using MyCash.Storage;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MyCash.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        public ObservableCollection<string> Lessons { get; set; } = MediatorClass.Lessons;

        private int sizeCollections = 0;

        private string _SelectedLesson = null;
        public string SelectedLesson
        {
            get => _SelectedLesson;
            set
            {
                _SelectedLesson = value;
                OnPropertyChanged();
            }
        }

        private string _NewItem = null;
        public string NewItem
        {
            get => _NewItem;
            set
            {
                _NewItem = value;
                OnPropertyChanged();
            }
        }

        public SettingViewModel()
        {
            sizeCollections = Lessons.Count;
        }

        public ICommand CloseWindow => new RelayCommand(() =>
        {
            if (sizeCollections != Lessons.Count)
            {
                var jsonOperations = new JsonOperations();
                jsonOperations.SerializingCollectionsSubjects("Subjects.json", MediatorClass.Lessons);
            }

            // получения количества окон и закрытие последнего
            Application.Current.Windows[1].Close();
        });

        public ICommand AddNewLesson => new RelayCommand(() =>
        {
            if (!string.IsNullOrEmpty(NewItem))
            {
                MediatorClass.Lessons.Add(NewItem);
                NewItem = null;
            }
        });

        public ICommand DeleteLesson => new RelayCommand(() =>
        {
            MediatorClass.Lessons.Remove(SelectedLesson);
        });

    }
}
