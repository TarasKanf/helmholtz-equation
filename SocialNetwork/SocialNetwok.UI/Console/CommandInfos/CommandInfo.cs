namespace SocialNetwork.UI.Console.CommandInfos
{
    /// <summary>
    /// Containes all data that command parser could recognize in command line entered by user 
    /// </summary>
    internal class CommandInfo : ICommandInfo
    {
        public string CommandName { get; set; }

        public string Email { get; set; }

        public bool User { get; set; }

        public bool Message { get; set; }

        public string FileName { get; set; }

        public bool Received { get; set; }

        public bool Sended { get; set; }

        public bool Mutual { get; set; }
    }
}
