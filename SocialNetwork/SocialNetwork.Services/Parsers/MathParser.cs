using System.Collections.Generic;
using System.Text.RegularExpressions;
using SocialNetwork.Services.Contracts;
using SocialNetwork.Services.ValidationAndHashingServices;

namespace SocialNetwork.Services.Parsers
{
    public class MathParser : IParser
    {
        private readonly MathValidation mathValidator;

        public MathParser()
        {
            mathValidator = new MathValidation();
        }

        /// <summary>
        ///     Method parses message into math(..) expressions, if they are in
        /// </summary>
        /// <param name="input">User's message</param>
        /// <returns>Dictionary of start index and length of math(..) expressions</returns>
        public Dictionary<int, int> Parse(string input)
        {
            var mathDict = new Dictionary<int, int>();

            if (mathValidator.ValidMath(input))
            {
                var matches = Regex.Matches(input, mathValidator.Pattern, RegexOptions.IgnoreCase);
                foreach (Match item in matches)
                {
                    int startIndex = item.Index + 5;
                    int length = item.Length - 6;
                    mathDict.Add(startIndex, length);
                }
            }

            return mathDict;
        }
    }
}