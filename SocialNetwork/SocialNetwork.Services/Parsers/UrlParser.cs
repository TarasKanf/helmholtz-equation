using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Services.Contracts;

namespace SocialNetwork.Services.ValidationAndHashingServices
{
    public class UrlParser : IParser
    {
        private const string UrlElement = "http";
        private const string Spliters = " ";
        private readonly UrlValidation urlValidator;

        public UrlParser()
        {
            urlValidator = new UrlValidation();
        }

        public Dictionary<int, int> Parse(string input)
        {
            var list = new Dictionary<int, int>();
            string modified = input;
            while (modified.Contains(UrlElement))
            {
                int start = modified.IndexOf(UrlElement);
                int end = start;

                while (!Spliters.Contains(modified[end]) && end != modified.Length - 1)
                {
                    end++;
                }

                if (end != modified.Length - 1)
                {
                    end--;
                }

                int lengthPossibleUrl = end + 1 - start;
                if (lengthPossibleUrl >= 0)
                {
                    string possibleUrl = modified.Substring(start, lengthPossibleUrl);
                    if (urlValidator.ValidUrl(possibleUrl))
                    {
                        list.Add(input.IndexOf(possibleUrl), lengthPossibleUrl);
                    }
                }

                modified = modified.Remove(start, lengthPossibleUrl);
            }

            return list;
        }
    }
}