using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SpeedyPass
{
    //This service performs three functions
    // - Loads data on startup, 
    //   puts into ref variable
    //
    // - Tracks changes to data file, 
    //   encase passwords added on another pc
    //
    // - Saves changes to data file
    //   just serialises variable if variable changes.

    public class TrackedList<T>
    {
        public delegate void Changed();
        public event Changed OnChanged;

        public List<T> Value { get { return this.value; } set { this.value = value;this.OnChanged?.Invoke(); } }
        private List<T> value;

        public TrackedList()
        {
            this.value = new List<T>();
        }

        public void Add(T v)
        {
            this.value.Add(v);
            this.OnChanged?.Invoke();
        }

        public void Remove(T v)
        {
            this.value.Remove(v);
            this.OnChanged?.Invoke();
        }

        public void Clear()
        {
            this.value.Clear();
            this.OnChanged?.Invoke();
        }
    }

    public class PasswordDataService
    {
        public TrackedList<PasswordDataModel> passwordDataModelList;
        private FileSystemWatcher fileSystemWatcher;
        private string appDataFilePath;

        public PasswordDataService()
        {
            this.passwordDataModelList = new TrackedList<PasswordDataModel>();
            this.appDataFilePath = File.Exists("./data.dat") ? "data.dat" : File.ReadAllText("./config.cfg");
            //this.fileSystemWatcher = new FileSystemWatcher(this.appDataFilePath);
           // this.fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;

            this.passwordDataModelList.OnChanged += PasswordDataModelList_OnChanged;
            //this.fileSystemWatcher.Changed += FileSystemWatcher_Changed;

            this.passwordDataModelList.Value = this.Load();
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            this.passwordDataModelList.Value = this.Load();
        }

        private void PasswordDataModelList_OnChanged()
        {
            this.Save(this.passwordDataModelList.Value);
        }

        public List<PasswordDataModel> Load()
        {
            List<PasswordDataModel> returnDataModelList = new List<PasswordDataModel>();

            if (File.Exists(this.appDataFilePath))
            {
                string encodedSaveData = File.ReadAllText(this.appDataFilePath);
                string decodedSaveData = this.Base64Decode(encodedSaveData);
                List<PasswordDataModel> passwordDataModelList = this.DeserialiseFromString<List<PasswordDataModel>>(decodedSaveData);

                returnDataModelList = passwordDataModelList;
            }
            else
            {
                returnDataModelList.Add(new PasswordDataModel
                {
                    Domain = "Welcome",
                    Username = "You can click the domain or password to copy it.",
                    Password = "Double click to delete.",
                });
            }

            return returnDataModelList;
        }

        public void Save(List<PasswordDataModel> passwordDataModelList)
        {
            string serialisedSaveData = this.SerialiseToString(passwordDataModelList);
            string encodedSerialisedData = this.Base64Encode(serialisedSaveData);

            File.WriteAllText(this.appDataFilePath, encodedSerialisedData);
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
