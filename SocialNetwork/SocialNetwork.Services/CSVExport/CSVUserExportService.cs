using System.Collections.Generic;
using System.IO;
using System.Text;
using SocialNetwork.Common;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services.Properties;

namespace SocialNetwork.Services.CSVExport
{
    public class CsvUserExportService : IDataExport<User>
    {
        private static readonly Logger Logger = new Logger();

        /// <summary>
        ///     Extention of csv file.
        /// </summary>
        public string Extention { get; } = ".csv";

        /// <summary>
        ///     Save given user list into csv file.
        /// </summary>
        /// <param name="filePath">File where list must be saved.</param>
        /// <param name="userList">List to save.</param>
        /// <param name="append"></param>
        public void Save(string filePath, List<User> userList, bool append = false)
        {
            var csvText = new StringBuilder();
            string csvFile = filePath + Extention;

            foreach (var user in userList)
            {
                var id = user.Id;
                string fname = user.FirstName;
                string lname = user.LastName;
                string email = user.Email;

                csvText.AppendLine(string.Format($"{id},{fname},{lname},{email}"));
            }

            using (var stream = new StreamWriter(csvFile, append))
            {
                stream.Write(csvText.ToString());
            }

            Logger.Info(string.Format(Resources.CustomUserListSaved, csvFile));
        }

        /// <summary>
        ///     Save user list from xml into csv file.
        /// </summary>
        /// <param name="filePath">File where list must be saved.</param>
        /// <param name="append"></param>
        public void Save(string filePath, bool append = false)
        {
            var csvText = new StringBuilder();
            var work = new UnitOfWork();
            string csvFile = filePath + Extention;

            foreach (var user in work.Users.GetAll())
            {
                var id = user.Id;
                string fname = user.FirstName;
                string lname = user.LastName;
                string email = user.Email;

                csvText.AppendLine(string.Format($"{id},{fname},{lname},{email}"));
            }

            using (var stream = new StreamWriter(csvFile, append))
            {
                stream.Write(csvText.ToString());
            }

            Logger.Info(string.Format(Resources.UserListSaved, csvFile));
        }
    }
}