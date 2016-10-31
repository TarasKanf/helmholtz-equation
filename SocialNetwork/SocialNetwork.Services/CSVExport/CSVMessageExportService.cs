using System.Collections.Generic;
using System.IO;
using System.Text;
using SocialNetwork.Common;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.Properties;

namespace SocialNetwork.Services.CSVExport
{ 
    public class CsvMessageExportService : IDataExport<Message>
    {
        private static readonly Logger Logger =
            new Logger(typeof(CsvMessageExportService).FullName);

        /// <summary>
        /// Extention of csv file
        /// </summary>
        public string Extention { get; } = ".csv";
      
        /// <summary>
        /// Save given message list into csv file.
        /// </summary>
        /// <param name="filePath">File where list must be saved.</param>
        /// <param name="messageList">List to save.</param>
        /// <param name="append"></param>    
        public void Save(string filePath, List<Message> messageList, bool append = false)
        {
            var csvText = new StringBuilder();
            string csvFile = filePath + Extention;

            foreach (var message in messageList)
            {
                var id = message.Id;
                var data = message.Data;
                var id1 = message.SenderId;
                var id2 = message.ReceiverId;

                csvText.AppendLine(string.Format($"{id},{data},{id1},{id2}"));
            }

            using (StreamWriter stream = new StreamWriter(csvFile, append))
            {
                stream.Write(csvText.ToString());
            }

            Logger.Info(string.Format(Resources.CustomMessageListSaved, csvFile));
        }

        /// <summary>
        /// Save message list from xml into csv file.
        /// </summary>
        /// <param name="filePath">File where list must be saved.</param>
        /// <param name="append"></param>        
        public void Save(string filePath, bool append = false)
        {
            var csvText = new StringBuilder();
            UnitOfWork work = new UnitOfWork();
            string csvFile = filePath + Extention;

            foreach (var message in work.Messages.GetAll())
            {
                var id = message.Id;
                var data = message.Data;
                var id1 = message.SenderId;
                var id2 = message.ReceiverId;

                csvText.AppendLine(string.Format($"{id},{data},{id1},{id2}"));
            }

            using (StreamWriter stream = new StreamWriter(csvFile, append))
            {
                stream.Write(csvText.ToString());
            }

            Logger.Info(string.Format(Resources.MessageListSaved, csvFile));
        }
    }
}
