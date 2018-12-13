using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyCash.Storage
{
    interface IJsonOperations
    {
        /// <summary>
        /// Сериализация коллекции
        /// </summary>
        /// <param name="fileName">Имя файла для записи</param>
        /// <param name="list">Коллекция для записи</param>
        void SerializingCollections(string fileName, ObservableCollection<MyOrders> list);

        /// <summary>
        /// Десериализация коллекции
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns></returns>
        ObservableCollection<MyOrders> DeserializingCollections(string fileName);
    }
}
