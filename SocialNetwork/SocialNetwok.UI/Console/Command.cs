using System;
using System.Collections.Generic;
using SocialNetwork.UI.Console.CommandHandlers;
using SocialNetwork.UI.Console.InputOutput;
using SocialNetwork.UI.Console.Properties;

namespace SocialNetwork.UI.Console
{
    /// <summary>
    ///     Executes command if it has appropriate command handler.
    /// </summary>
    internal class Command
    {
        private readonly IInputOutputSevice ioService;

        /// <summary>
        ///     Creates and initializes Command with appropriate objects
        /// </summary>
        public Command()
        {
            Handlers = new Dictionary<string, ICommandHandler>();
            ioService = NinjectKernel.Get<IInputOutputSevice>();
            Parser = NinjectKernel.Get<CommandParser>();
        }

        /// <summary>
        ///     Containes all command handlers that should execute appropriate command with name the same as dictinary key.
        /// </summary>
        public Dictionary<string, ICommandHandler> Handlers { get; set; }

        private CommandParser Parser { get; }

        /// <summary>
        ///     Executes command line.Firstly command line goes to CommandParser
        ///     and then appropriate CommandHandler handles recieved CommandInfo
        /// </summary>
        /// <param name="commandLine"> Containes line with command and command options </param>
        /// <returns></returns>
        public bool Execute(string commandLine)
        {
            var info = Parser.GetCommandInfo(commandLine);

            if (string.IsNullOrEmpty(info?.CommandName))
            {
                ioService.Out.WriteLine(Resources.UnknownCommand);
                return false;
            }

            ICommandHandler handler;
            try
            {
                handler = Handlers[info.CommandName];
            }
            catch (Exception)
            {
                ioService.Out.WriteLine(Resources.UnknownCommand);

                return false;
            }

            return handler.Execute(info);
        }
    }
}