using System;

namespace SocialNetwork.Bot.Entities
{
    internal class Joke
    {      
        public Joke(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}
