using System;
using NDesk.Options;
using SocialNetwork.Services;
using SocialNetwork.UI.Console.CommandInfos;
using SocialNetwork.UI.Console.InputOutput;
using SocialNetwork.UI.Console.Properties;

namespace SocialNetwork.UI.Console
{
    internal class CommandParser
    {
        private readonly IInputOutputSevice printer;
        private readonly UserValidationService userValidator;
        private CommandInfo commandInfo;
        private bool helpIndicator;

        /// <summary>
        ///     Initializes all dependencies for CommandParser and options in OptionSet
        /// </summary>
        public CommandParser()
        {
            userValidator = NinjectKernel.Get<UserValidationService>();
            printer = NinjectKernel.Get<IInputOutputSevice>();

            InitializeOptionSet();
        }

        /// <summary>
        ///     Containes all command options that Parser would recognize
        /// </summary>
        public OptionSet OptionSet { get; set; }

        /// <summary>
        ///     Parses recieved command line and returns appropriately filled CommandInfo object
        /// </summary>
        /// <param name="commandLine"></param>
        /// <returns></returns>
        public CommandInfo GetCommandInfo(string commandLine)
        {
            var tokens = commandLine.Split(
                new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            commandInfo = new CommandInfo { CommandName = string.Empty };
            helpIndicator = false;
            try
            {
                OptionSet.Parse(tokens);
            }
            catch (OptionException e)
            {
                printer.Out.WriteLine(e.Message);
                printer.Out.WriteLine(Resources.ParserHelpOffer);

                return null;
            }

            if (!helpIndicator)
            {
                return commandInfo;
            }

            ShowHelp(OptionSet);

            return null;
        }

        private void ShowHelp(OptionSet p)
        {
            printer.Out.WriteLine("Usage: [OPTIONS]+ message");
            printer.Out.WriteLine("Options:");
            p.WriteOptionDescriptions(printer.Out);
        }

        private void AppendToCommandName(string commandPart)
        {
            if (string.IsNullOrEmpty(commandInfo.CommandName))
            {
                commandInfo.CommandName = commandPart;
                return;
            }

            commandInfo.CommandName += $" {commandPart}";
        }

        private void ValidateEmail(string email)
        {
            if (!userValidator.ValidEmail(email))
            {
                throw new OptionException(Resources.ErrorNotValidEmail, "e|email");
            }

            commandInfo.Email = email;
        }

        private void InitializeOptionSet()
        {
            commandInfo = new CommandInfo { CommandName = string.Empty };
            OptionSet = new OptionSet
            {
                {
                    "<>|c", Resources.ParserOptionCommandInfo,
                    AppendToCommandName
                },
                {
                    "u|user", Resources.ParserOptionUser,
                    v =>
                    {
                        if (v != null)
                        {
                            commandInfo.User = true;
                        }
                    }
                },
                {
                    "m|message", Resources.ParserOptionMessage,
                    v =>
                    {
                        if (v != null)
                        {
                            commandInfo.Message = true;
                        }
                    }
                },
                {
                    "e|email:", Resources.ParserOptionEmail,
                    v =>
                    {
                        if (v != null)
                        {
                             ValidateEmail(v);
                        }
                    }
                },
                {
                    "f|file=", Resources.ParserOptionFileName,
                    v =>
                    {
                        if (v != null)
                        {
                            commandInfo.FileName = v;
                        }
                    }
                },
                {
                    "r|received", Resources.ParserOptionRecieved,
                    v =>
                    {
                        if (v != null)
                        {
                            commandInfo.Received = true;
                        }
                    }
                },
                {
                    "s|sended", Resources.ParserOptionSended,
                    v =>
                    {
                        if (v != null)
                        {
                            commandInfo.Sended = true;
                        }
                    }
                },
                {
                    "mutual", Resources.ParserOptionMutual,
                    v =>
                    {
                        if (v != null)
                        {
                            commandInfo.Mutual = true;
                        }
                    }
                },
                {
                    "?|h|help", Resources.ParserOptionHelp,
                    v => helpIndicator = v != null
                }
            };
        }
    }
}