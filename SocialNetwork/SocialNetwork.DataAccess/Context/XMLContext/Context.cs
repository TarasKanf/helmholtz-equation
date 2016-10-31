using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using SocialNetwork.Common;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.DataAccess.Context
{
    public class XmlContext : IContext
    {
        private static readonly Logger Logger = new Logger(typeof(XmlContext).FullName);
        private readonly string fileLocation;

        public XmlContext()
        {
            fileLocation = ConfigurationManager.AppSettings["XmlPath"];
        }

        public XmlContext(string xmlFilePath)
        {
            fileLocation = xmlFilePath;
        }

        /// <summary>
        /// Save List of entities into file
        /// </summary>
        /// <typeparam name="T">BaseEntity: Message, Connection or User</typeparam>
        /// <param name="entities">List of entities</param>
        public void Write<T>(IList<T> entities)
        where T : BaseEntity
        {
            if (entities == null)
            {
                return;
            }

            string filePath = Path.Combine(fileLocation, $"{typeof(T).Name}s.xml");
            try
            {
                using (StreamWriter stream = new StreamWriter(filePath, false))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(List<T>));
                    ser.Serialize(stream, entities);
                }
            }
            catch (FileNotFoundException)
            {
                Logger.Error($"File with path {filePath} doesn't exist.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Load List of entities from file
        /// </summary>
        /// <typeparam name="T">BaseEntity: Message, Connection or User</typeparam>
        /// <returns>List of entities</returns>
        public List<T> Read<T>()
        where T : BaseEntity
        {
            var filePath = Path.Combine(fileLocation, $"{typeof(T).Name}s.xml");
            if (!File.Exists(filePath))
            {
                return null;
            }
                
            using (var stream = File.OpenRead(filePath))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                return (List<T>)serializer.Deserialize(stream);
            }
        }
    }
}
