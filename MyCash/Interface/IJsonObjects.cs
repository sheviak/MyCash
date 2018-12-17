using System.Collections.ObjectModel;

namespace MyCash.Storage
{
    interface IJsonObjects
    {
        /// <summary>
        /// Сериализация коллекции
        /// </summary>
        /// <param name="fileName">Имя файла для записи</param>
        /// <param name="list">Коллекция для записи</param>
        void SerializingCollectionsObjects(string fileName, ObservableCollection<MyOrders> list);

        /// <summary>
        /// Десериализация коллекции
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns></returns>
        ObservableCollection<MyOrders> DeserializingCollectionsObjects(string fileName);
    }
}
