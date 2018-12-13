namespace MyCash.Storage
{
    public class MyOrders
    {
        public string FIO { get; set; } 
        public string Subject { get; set; }
        public int LrSubject { get; set; }
        public int Variant { get; set; }
        public int Cost { get; set; }
        public bool Report { get; set; }
        public bool Status { get; set; }

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
        public MyOrders(string fio, string subject, int lr, int var, int cost,
            bool report, bool status)
        {
            this.FIO = fio;
            this.Subject = subject;
            this.LrSubject = lr;
            this.Variant = var;
            this.Cost = cost;
            this.Report = report;
            this.Status = status;
        }
    }
}
