namespace SocialNetwork.UI.Console.InputOutput
{
    public interface IPasswordReader
    {
        /// <summary>
        ///     Reads password from source in a secure way.
        /// </summary>
        /// <returns></returns>
        string ReadPassword();
    }
}
