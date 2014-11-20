using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Maize
{

    class SaveLoad
    {
        public static void Serialize<T>(T dataToSerialize, string filePath)
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Default);
                    writer.Formatting = Formatting.Indented;
                    serializer.Serialize(writer, dataToSerialize);
                    writer.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public static T Deserialize<T>(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T serializedData;

                using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    serializedData = (T)serializer.Deserialize(stream);
                }

                return serializedData;
            }
            catch
            {
                throw;
            }
        }
    }
}
