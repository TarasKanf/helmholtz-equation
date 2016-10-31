using System.IO;

namespace SocialNetwork.UI.Console.InputOutput
{
    public interface IInputOutputSevice : IPasswordReader
    {
        /// <summary>
        ///     Gets output stream
        /// </summary>
        TextWriter Out { get; }

        /// <summary>
        ///     Gets input stream
        /// </summary>
        TextReader In { get; }        
    }
}