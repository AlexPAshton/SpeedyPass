using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SpeedyPass.Services
{
    public class PersistantModelStorageService
    {
        public enum StorageTypes
        {
            Default,
            Encoded,
        }

        public void Save<T>(string filePath, T dataModel, StorageTypes storageType = StorageTypes.Default)
        {
            this.save(filePath, dataModel, storageType);
        }

        public T Load<T>(string filePath, StorageTypes storageType = StorageTypes.Default)
        {
            return this.load<T>(filePath, storageType);
        }

        private T load<T>(string filePath, StorageTypes storageType)
        {
            T returnDataModel = default(T);

            if (File.Exists(filePath))
            {
                if (storageType == StorageTypes.Encoded)
                {
                    string encodedSaveData = File.ReadAllText(filePath);
                    string decodedSaveData = this.Base64Decode(encodedSaveData);
                    returnDataModel = this.DeserialiseFromString<T>(decodedSaveData);
                }
                else
                {
                    string serialisedData = File.ReadAllText(filePath);
                    returnDataModel = this.DeserialiseFromString<T>(serialisedData);
                }
            }

            return returnDataModel;
        }

        private void save<T>(string filePath, T dataModel, StorageTypes storageType)
        {
            if (storageType == StorageTypes.Encoded)
            {
                string serialisedSaveData = this.SerialiseToString(dataModel);
                string encodedSerialisedData = this.Base64Encode(serialisedSaveData);

                File.WriteAllText(filePath, encodedSerialisedData);
            }
            else
            {
                string serialisedSaveData = this.SerialiseToString(dataModel);

                File.WriteAllText(filePath, serialisedSaveData);
            }
        }

        //Encode Functions
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        // Serialise Functions
        private string SerialiseToString<T>(T dataToSerialise)
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
                throw;
            }
        }
    }
}
