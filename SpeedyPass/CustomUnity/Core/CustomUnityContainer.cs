using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CustomUnity
{
    public static class CustomUnityContainer
    {
        private const string CONFIG_PATH = "./Data/Cunity.cfg";
        private const string EXCEPTION_CONFIGMISSING = "(X0) CustomUnityContainer config is missing.";
        private const string EXCEPTION_FAILEDTOSAVE = "(X1) CustomUnityContainer failed to save config.";
        private const string EXCEPTION_FAILEDTOLOAD = "(X2) CustomUnityContainer failed to load config.";
        private const string EXCEPTION_CANNOTRESOLVETYPE = "(X2) CustomUnityContainer cannot resolve type. ";

        private static List<TypeMap> injectionMapList = new List<TypeMap>();

        static CustomUnityContainer()
        {
            if (File.Exists(CustomUnityContainer.CONFIG_PATH))
            {
                CustomUnityContainer.Load();
            }
        }

        public static T Resolve<T>()
        {
            try
            {
                string resolvedTypeString = injectionMapList.First(i => i.TypeName == typeof(T).Name).MapsTo;

                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

                List<Type> types = new List<Type>();

                foreach (Assembly assembly in assemblies)
                {
                    Type type = assembly.GetType(resolvedTypeString);
                    if (type != null)
                        types.Add(type);
                }

                return (T)Activator.CreateInstance(types.First(t => t.FullName == resolvedTypeString));
            }
            catch
            {
                return default(T);
            }
        }

        public static void Save()
        {
            try
            { 
                File.WriteAllText(CustomUnityContainer.CONFIG_PATH, CustomUnityContainer.SerialiseToString(CustomUnityContainer.injectionMapList));
            }
            catch
            {
                throw new Exception(CustomUnityContainer.EXCEPTION_FAILEDTOSAVE);
            }
        }

        public static void Load()
        {
            try
            {
                CustomUnityContainer.injectionMapList = DeserialiseFromString<List<TypeMap>>(File.ReadAllText(CustomUnityContainer.CONFIG_PATH));
            }
            catch
            {
                throw new Exception(CustomUnityContainer.EXCEPTION_FAILEDTOLOAD);
            }
        }

        private static string SerialiseToString<T>(T dataToSerialise)
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

        private static T DeserialiseFromString<T>(string data)
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
    }
}
