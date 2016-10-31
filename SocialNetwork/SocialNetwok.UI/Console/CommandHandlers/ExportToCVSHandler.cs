using System;
using SocialNetwork.Common;
using SocialNetwork.Services.CSVExport;
using SocialNetwork.UI.Console.CommandInfos;
using SocialNetwork.UI.Console.Properties;

namespace SocialNetwork.UI.Console.CommandHandlers
{
    internal class ExportToCvsHandler : CommandHandler
    {
        private static readonly Logger Logger = new Logger(typeof(ExportToCvsHandler).FullName);
        private readonly CsvUserExportService exportUserService;
        private readonly CsvMessageExportService exportMassageService;

        public ExportToCvsHandler()
        {
            exportMassageService = NinjectKernel.Get<CsvMessageExportService>();
            exportUserService = NinjectKernel.Get<CsvUserExportService>();
        }

        /// <summary>
        ///     Executes 'export' command. Writes appropriate data to file specified in commandInfo
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <returns></returns>
        public override bool Execute(CommandInfo commandInfo)
        {
            bool appendWrite = commandInfo.User
                               && commandInfo.Message;
            if (commandInfo.User)
            {
                ExportUsers(commandInfo.FileName, appendWrite);
            }

            if (commandInfo.Message)
            {
                ExportMessages(commandInfo.FileName, appendWrite);
            }

            IoService.Out.WriteLine(
                Resources.InfoDataExportedToCVS,
                commandInfo.FileName,
                AppDomain.CurrentDomain.BaseDirectory);
            Logger.Info(
                string.Format(
                    Resources.InfoDataExportedToCVS, 
                    commandInfo.FileName, 
                    AppDomain.CurrentDomain.BaseDirectory));

            return true;
        }

        private void ExportUsers(string filePath, bool append)
        {
            exportUserService.Save(filePath, append);
        }

        private void ExportMessages(string filePath, bool append)
        {
            exportMassageService.Save(filePath, append);
        }
    }
}
