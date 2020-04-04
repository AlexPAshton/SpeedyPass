using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SpeedyPass.Helpers
{
    public class ModelStorageHelper : IModelStorageHelper
    {
        public T LoadModel<T>(string path)
        {
            string fileContents = File.ReadAllText(path);
            return this.DeserialiseFromString<T>(fileContents);
        }

        public void SaveModel<T>(T o, string path)
        {
            try
            {
                string serialisedModel = this.SerialiseToString(o);
                File.WriteAllText(path, serialisedModel);
            }
            catch
            {

            }
        }

        private string SerialiseToString<T>(T dataToSerialise)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Default);
                    xmlTextWriter.Formatting = Formatting.Indented;
                    xmlSerializer.Serialize(xmlTextWriter, dataToSerialise);
                    xmlTextWriter.Close();

                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch
            {
                return null;
            }
        }

        private T DeserialiseFromString<T>(string data)
        {
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(data);
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    T serializedData;
                    serializedData = (T)serializer.Deserialize(stream);
                    return serializedData;
                }
            }
            catch
            {
                return default(T);
            }
        }
    }
}
