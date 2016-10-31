using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Services.Contracts;
using SocialNetwork.Services.ValidationAndHashingServices;

namespace SocialNetwork.Services.Parsers
{
    public class EmailParser : IParser
    {
        private const string Spliters = " ,!?()*^%$;:<>№#'";
        private const char EmailElement = '@';
        private readonly EmailValidation emailValidator;

        public EmailParser()
        {
            emailValidator = new EmailValidation();
        }

        public Dictionary<int, int> Parse(string input)
        {
            var list = new Dictionary<int, int>();
            string modified = input;

            while (modified.Contains(EmailElement))
            {
                int indexDog = modified.IndexOf(EmailElement);
                int start = indexDog;
                int end = indexDog;

                while (!Spliters.Contains(modified[start]) && start != 0)
                {
                    start--;
                }

                if (start != 0)
                {
                    start++;
                }

                while (!Spliters.Contains(modified[end]) && end != modified.Length - 1)
                {
                    end++;
                }

                if (end != modified.Length - 1)
                {
                    end--;
                }

                int lengthPossibleEmail = end + 1 - start;

                if (lengthPossibleEmail >= 5)
                {
                    string possibleEmail = modified.Substring(start, lengthPossibleEmail);

                    if (emailValidator.ValidEmail(possibleEmail))
                    {
                        list.Add(input.IndexOf(possibleEmail), lengthPossibleEmail);
                    }
                }

                modified = modified.Remove(start, lengthPossibleEmail);
            }

            return list;
        }
    }
}