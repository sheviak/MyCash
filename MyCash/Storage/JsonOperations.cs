using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace MyCash.Storage
{
    public class JsonOperations : IJsonObjects, IJsonSubjects
    {
        public ObservableCollection<MyOrders> DeserializingCollectionsObjects(string fileName)
        {
            string json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<ObservableCollection<MyOrders>>(json);
        }

        public ObservableCollection<string> DeserializingCollectionsSubjects(string fileName)
        {
            string json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<ObservableCollection<string>>(json);
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
