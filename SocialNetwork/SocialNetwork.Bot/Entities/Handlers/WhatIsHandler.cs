using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;
using HTMLConverter;
using SocialNetwork.Bot.Bot2Helpers;
using SocialNetwork.Bot.Properties;

namespace SocialNetwork.Bot.Handlers
{
    internal class WhatIsHandler : IHandler
    {
        private readonly CachingService cachedAnswers;

        public WhatIsHandler()
        {
            cachedAnswers = new CachingService();
        }

        public string Usage { get; } = "what is <word>";

        public string GetAnswer(RequestInfo info)
        {
            string answer = cachedAnswers.GetAnswer(info);

            if (answer == string.Empty)
            {
                var searchingResult = GetAnswer(info.Data);
                answer = new TextRange(searchingResult.ContentStart, searchingResult.ContentEnd).Text;
                cachedAnswers.Cache(info, answer);
            }

            return answer;
        }

        public FlowDocument GetAnswer(string searchingText)
        {
            var webClient = new WebClient();
            string pageSourceCode =
                webClient.DownloadString(
                    "https://en.wikipedia.org/w/api.php?format=xml&action=query&prop=extracts&titles=" + searchingText +
                    "&redirects=true");

            var doc = new XmlDocument();
            doc.LoadXml(pageSourceCode);

            var fnode = doc.GetElementsByTagName("extract")[0];
            var docum = new FlowDocument();

            try
            {
                string text = fnode.InnerText;
                var regex = new Regex("\\<[*\\>]*\\>");
                text = regex.Replace(text, string.Empty);
                string res = string.Format(text);
                res = HtmlToXamlConverter.ConvertHtmlToXaml(res, false);

                using (var stringReader = new StringReader(res))
                {
                    var xmlReader = XmlReader.Create(stringReader);
                    var sec = XamlReader.Load(xmlReader) as Section;

                    var countOfBlocks = 0;
                    while (sec.Blocks.Count > 0 && countOfBlocks < 2)
                    {
                        docum.Blocks.Add(sec.Blocks.FirstBlock);
                        countOfBlocks++;
                    }

                    var para = new Paragraph();
                    docum.Blocks.Add(para);

                    var link = new Hyperlink();
                    link.IsEnabled = true;
                    string wikiUrl = Resources.wikiURL + searchingText.Replace(' ', '_');
                    link.Inlines.Add(wikiUrl);
                    link.NavigateUri = new Uri(wikiUrl);
                    para.Inlines.Add(link);
                }
            }
            catch (Exception)
            {
                docum = new FlowDocument();
                docum.Blocks.Add(new Paragraph(new Run(Resources.searchingTextDoesNotExistMessage)));
            }

            return docum;
        }
    }
}