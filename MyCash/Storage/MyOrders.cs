using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCash.Storage
{
    public class MyOrders : INotifyPropertyChanged
    {
        private string _FIO;
        private string _Subject;
        private int _LrSubject;
        private int _Variant;
        private int _Cost;
        private bool _Report;
        private bool _Status;
        private DateTime _Date;
        private string _Description;

        public string FIO
        {
            get => _FIO;
            set
            {
                _FIO = value;
                OnPropertyChanged();
            }
        }

        public string Subject
        {
            get => _Subject;
            set
            {
                _Subject = value;
                OnPropertyChanged();
            }
        }

        public int LrSubject
        {
            get => _LrSubject;
            set
            {
                _LrSubject = value;
                OnPropertyChanged();
            }
        }

        public int Variant
        {
            get => _Variant;
            set
            {
                _Variant = value;
                OnPropertyChanged();
            }
        }

        public int Cost
        {
            get => _Cost;
            set
            {
                _Cost = value;
                OnPropertyChanged();
            }
        }

        public bool Report
        {
            get => _Report;
            set
            {
                _Report = value;
                OnPropertyChanged();
            }
        }
        public bool Status
        {
            get => _Status;
            set
            {
                _Status = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => _Date;
            set
            {
                _Date = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _Description;
            set
            {
                _Description = value;
                OnPropertyChanged();
            }
        }

        public MyOrders() { }

        /// <summary>
        /// Объект - заказы
        /// </summary>
        /// <param name="fio">ФИО</param>
        /// <param name="subject">Предмет</param>
        /// <param name="lr">Лабораторная работа</param>
        /// <param name="var">Вариант</param>
        /// <param name="cost">Стоимость</param>
        /// <param name="report">Отчёт</param>
        /// <param name="status">Статус</param>
        /// <param name="date">Дата добавления</param>
        /// <param name="description">Описание</param>
        public MyOrders(string fio, string subject, int lr, int var, int cost,
            bool report, bool status, DateTime date, string description)
        {
            this.FIO = fio;
            this.Subject = subject;
            this.LrSubject = lr;
            this.Variant = var;
            this.Cost = cost;
            this.Report = report;
            this.Status = status;
            this.Date = date;
            this.Description = description;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
