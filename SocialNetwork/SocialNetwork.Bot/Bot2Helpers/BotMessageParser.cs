using System.Globalization;
using System.Text.RegularExpressions;
using SocialNetwork.Bot.Properties;

namespace SocialNetwork.Bot.Bot2Helpers
{
    internal class BotMessageParser
    {
        private const string WhatIsString = "what is";
        private const string MathRegexString = @"(?x)
                ^
                (?> (?<p> \( )* (?>-?\d+(?:\.\d+)?) (?<-p> \) )* )
                (?>(?:
                    [-+*/]
                    (?> (?<p> \( )* (?>-?\d+(?:\.\d+)?) (?<-p> \) )* )
                )*)
                (?(p)(?!))
                $
            ";

        private const int SizeOfCommandWhatIs = 8;

        private readonly Regex mathPattern = new Regex(MathRegexString);

        private readonly Regex whatIsPattern = new Regex(WhatIsString);

        public RequestInfo Parse(string messageText)
        {
            var info = new RequestInfo();
            string text = messageText.ToLower(CultureInfo.InvariantCulture);

            if (mathPattern.IsMatch(text))
            {
                info.Data = text.Replace(" ", string.Empty);
                info.HandlerName = Resources.MathHandler;
            }
            else if (whatIsPattern.IsMatch(text))
            {
                if (text.IndexOf(WhatIsString) == 0)
                {
                    int endNumber = text.Length - 1;
                    char end = messageText[endNumber];

                    if (end == '?' || end == '.' || end == '!')
                    {
                        string result = text.Remove(endNumber);

                        info.Data = result.Remove(0, SizeOfCommandWhatIs);
                        info.HandlerName = Resources.WhatIsHandler;
                    }
                    else
                    {
                        info.Data = text.Remove(0, SizeOfCommandWhatIs);
                        info.HandlerName = Resources.WhatIsHandler;
                    }
                }
                else
                {
                    info.Data = string.Empty;
                    info.HandlerName = Resources.DefaultHandler;
                }
            }
            else
            {
                info.Data = string.Empty;
                info.HandlerName = Resources.DefaultHandler;
            }

            return info;
        }
    }
}