using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace MyCash.Storage
{
    public class JsonOperations : IJsonOperations
    {
        public ObservableCollection<MyOrders> DeserializingCollections(string fileName)
        {
            string json = File.ReadAllText(fileName);
            var arr = JsonConvert.DeserializeObject<ObservableCollection<MyOrders>>(json);
            return arr;
        }

        public void SerializingCollections(string fileName, ObservableCollection<MyOrders> list)
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
