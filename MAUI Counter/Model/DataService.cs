using MAUI_Counter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MAUI_Counter
{
    public class DataService
    {
        private readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "counters.xml");

        public void Save(List<Count>counters)
        {
            var serializer = new XmlSerializer(typeof(List<Count>));
            using var writer = new StreamWriter(filePath);
            serializer.Serialize(writer, counters); 
        }
         public List<Count> Load()
        {

            if (!File.Exists(filePath)) 
                return new List<Count>(); 

            var serializer= new XmlSerializer(typeof(List<Count>));
            using var reader = new StreamReader(filePath);
            return (List<Count>)serializer.Deserialize(reader);
        }
    }

}
