using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace MyCash.Storage
{
    public class JsonOperations : IJsonObjects, IJsonSubjects
    {
        public ObservableCollection<MyOrders> DeserializingCollectionsObjects(string fileName)
        {
            string json = "";
            ObservableCollection<MyOrders> temp = new ObservableCollection<MyOrders>();
            if (File.Exists(fileName))
            {
                json = File.ReadAllText(fileName);
                temp = JsonConvert.DeserializeObject<ObservableCollection<MyOrders>>(json);
            }
            return temp;
        }

        public ObservableCollection<string> DeserializingCollectionsSubjects(string fileName)
        {
            string json = "";
            ObservableCollection<string> temp = new ObservableCollection<string>();
            if (File.Exists(fileName))
            {
                json = File.ReadAllText(fileName);
                temp = JsonConvert.DeserializeObject<ObservableCollection<string>>(json);
            }
            return temp;
        }

        public void SerializingCollectionsObjects(string fileName, ObservableCollection<MyOrders> list)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, list);
                }
            }
        }

        public void SerializingCollectionsSubjects(string fileName, ObservableCollection<string> list)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, list);
                }
            }
        }
    }
}
