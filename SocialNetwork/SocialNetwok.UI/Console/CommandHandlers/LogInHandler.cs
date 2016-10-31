using System;
using SocialNetwork.Common;
using SocialNetwork.Domain.DataTransferObjects;
using SocialNetwork.Services;
using SocialNetwork.Services.ValidationAndHashingServices;
using SocialNetwork.UI.Console.CommandInfos;
using SocialNetwork.UI.Console.Properties;
using SocialNetwork.Web.WebApiWrapper;
using SocialNetwork.Web.WebApiWrapper.Services;

namespace SocialNetwork.UI.Console.CommandHandlers
{
    internal class LogInHandler : CommandHandler
    {
        private static readonly Logger Logger = new Logger();
        private readonly Authenticator authenticator = NinjectKernel.Get<Authenticator>();

        public LogInHandler()
        {
            UserValidator = NinjectKernel.Get<UserValidationService>();
        }
              
        public event EventHandler LoggedIn;

        private UserValidationService UserValidator { get; set; }
        
        /// <summary>
        ///     Logs in user and filles currentSession with logged in user reference.
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <returns></returns>
        public override bool Execute(CommandInfo commandInfo)
        {
            return LogIn();
        }

        private bool LogIn()
        {
            IoService.Out.Write(Resources.InfoLogin);
            string login = IoService.In.ReadLine()?.Trim(' ');

            IoService.Out.Write(Resources.InfoPassword);
            string password = IoService.ReadPassword();

            bool valid = UserValidator.ValidPassword(password);
            if (!valid)
            {
                IoService.Out.WriteLine(
                    Resources.ErrorNotValidPassword,
                    PasswordValidation.MinPasswordLength);
                return false;
            }

            if (Session.IsLogged)
            {
                IoService.Out.WriteLine(Resources.ErrorDoubleLogIn);
                Logger.Error(Resources.ErrorDoubleLogIn);

                throw new Exception(Resources.ErrorDoubleLogIn);
            }

            try
            {
                LoginDTO loginModel = new LoginDTO()
                {
                    UserName = login,
                    Password = password
                };

                Response<SessionInfo> response = authenticator.LogIn(loginModel);

                if (response.IsSuccessful)
                {
                    Session = response.ResultTask.Result;
                }

                Logger.Info(Resources.InfoEndedSignUp);

                LoggedIn?.Invoke(this, new EventArgs());
            }
            catch (Exception ex)
            {
                IoService.Out.WriteLine(ex.Message);
                Logger.Error(ex.Message);
                return false;
            }

            return true;
        }
    }
}
