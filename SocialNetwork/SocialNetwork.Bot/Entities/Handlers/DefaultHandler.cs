using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SocialNetwork.Bot.Bot2Helpers;
using SocialNetwork.Bot.Entities;

namespace SocialNetwork.Bot.Handlers
{    
    public class DefaultHandler : IHandler
    {
        private readonly List<Joke> jokes;

        public DefaultHandler()
        {
            try
            {
                jokes = new List<Joke>();
                jokes.Add(new Joke("It's hard to explain puns to kleptomaniacs because they always take things literally."));
                jokes.Add(new Joke("I went on a once in a lifetime holiday. Never again."));
                jokes.Add(new Joke("My granddad has the heart of a lion and a life time ban from the Edinburgh zoo."));
                jokes.Add(new Joke("I can't stand being in a wheelchair."));
                jokes.Add(new Joke("What's the best thing about Switzerland? I don't know, but their flag is a huge plus."));
                jokes.Add(new Joke("A Buddhish walks up to a hotdog stand and says, \"Make ne one with everything\""));
                jokes.Add(new Joke("Don't you hate it when people answer their own question? I do. "));
                jokes.Add(new Joke("I told my doctor that I broke my arm in two places. He told me to stop going to those places."));
                jokes.Add(new Joke("What's orange and sound like a parrot? A carrot."));
                jokes.Add(new Joke("A photon is going through airport security. The TSA agent asks if he has any luggage. The photon says, “No, I’m traveling light.”"));
                jokes.Add(new Joke("Why do engineers confuse Halloween and Christmas? Because Oct 31 = Dec 25"));
                jokes.Add(new Joke("A logician’s wife is having a baby. The doctor immediately hands the newborn to the dad. His wife asks impatiently: “So, is it a boy or a girl?” The logician replies: “Yes.”"));
                jokes.Add(new Joke("Einstein, Newton and Pascal are playing hide and go seek. It’s Einstein’s turn to count so he covers his eyes and starts counting to ten. Pascal runs off and hides. Newton draws a one meter by one meter square on the ground in front of Einstein then stands in the middle of it. Einstein reaches ten and uncovers his eyes. He sees Newton immediately and exclaims, ”Newton! I found you! You’re it!”  Newton smiles and says “You didn’t find me, you found a Newton over a square meter. You found Pascal!”"));
            }
            catch (Exception)
            {
                jokes = new List<Joke>();
            }
        }

        public string Usage { get; } = "By default bot sends you some jokes.";

        public string GetAnswer(RequestInfo info)
        {
            try
            {
                var randIndex = new Random();
                return jokes[randIndex.Next(0, jokes.Count - 1)].Text;
            }
            catch (Exception)
            {
                return "This command doesn't exist! Sorry";
            }
        }
    }
}