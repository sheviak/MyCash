using System.Collections.ObjectModel;

namespace MyCash.Storage
{
    interface IJsonSubjects
    {
        /// <summary>
        /// Сериализация коллекции
        /// </summary>
        /// <param name="fileName">Имя файла для записи</param>
        /// <param name="list">Коллекция для записи</param>
        void SerializingCollectionsSubjects(string fileName, ObservableCollection<string> list);

        /// <summary>
        /// Десериализация коллекции
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns></returns>
        ObservableCollection<string> DeserializingCollectionsSubjects(string fileName);
    }
}
