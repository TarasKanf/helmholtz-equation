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
    internal class SignUpHandler : CommandHandler
    {
        private static readonly Logger Logger = new Logger();
        private readonly Authenticator authenticator = NinjectKernel.Get<Authenticator>();       

        public SignUpHandler()
        {
            UserValidator = NinjectKernel.Get<UserValidationService>();
        }

        private UserValidationService UserValidator { get; set; }

        /// <summary>
        ///     Requests data about user and registers it into socialnetwork
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <returns></returns>
        public override bool Execute(CommandInfo commandInfo)
        {
            SignUp();

            return true;
        }

        private void SignUp()
        {
            Logger.Debug(Resources.InfoStartedSignUp);

            IoService.Out.Write("Login:");
            string login = IoService.In.ReadLine();

            IoService.Out.Write(Resources.InfoFirstName);
            string firstName = IoService.In.ReadLine();

            IoService.Out.Write(Resources.InfoLastName);
            string lastName = IoService.In.ReadLine();

            string email = ReadAndValidateEmail();
            string password = ReadAndValidatePassword();

            try
            {
                RegistrationDTO regModel = new RegistrationDTO()
                {
                    Email = email,
                    LastName = lastName,
                    FirstName = firstName,
                    Password = password,
                    ConfirmPassword = password,
                    UserName = login
                };

                var result = authenticator.Register(regModel);
                IoService.Out.WriteLine(result);

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
            }
            catch (Exception ex)
            {
                IoService.Out.WriteLine(ex.Message);
            }
        }

        private string ReadAndValidateEmail()
        {
            string email;
            bool valid;
            do
            {
                IoService.Out.Write(Resources.InfoEmail);
                email = IoService.In.ReadLine();

                valid = UserValidator.ValidEmail(email);
                if (!valid)
                {
                    IoService.Out.WriteLine(Resources.ErrorNotValidEmail);
                }
            }
            while (!valid);

            return email;
        }

        private string ReadAndValidatePassword()
        {
            bool valid;
            string password;
            do
            {
                IoService.Out.Write(Resources.InfoPassword);
                password = IoService.ReadPassword();

                valid = UserValidator.ValidPassword(password);
                if (!valid)
                {
                    IoService.Out.WriteLine(
                        Resources.ErrorNotValidPassword,
                        PasswordValidation.MinPasswordLength);
                }
            }
            while (!valid);

            return password;
        }
    }
}