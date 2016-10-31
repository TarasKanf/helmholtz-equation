using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using SocialNetwork.Bot.Bot2Helpers;
using SocialNetwork.Bot.Handlers;
using SocialNetwork.Bot.Properties;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Services;

namespace SocialNetwork.Bot.Bots
{
    internal class Bot2 : IBot
    {        
        private readonly Dictionary<string, IHandler> handlers;
        private readonly MessangerService messenger;
        private readonly BotMessageParser parser;

        public Bot2()
        {
            BotProfile = new BotProfile();
            parser = new BotMessageParser();
            messenger = new MessangerService();
            handlers = new Dictionary<string, IHandler>();

            InitializeHandlers();
        }

        public BotProfile BotProfile { get; internal set; }

        public void HandleMessage(Message message)
        {
            var info = parser.Parse(message.Data);
            string answer = string.Empty;           

            try
            {
                answer = handlers[info.HandlerName].GetAnswer(info);
            }
            catch (Exception ex)
            {
                answer = ex.ToString(); // Resources.DontKnowAnswer;
            }
            finally
            {
                var messageAnswer = new Message(BotProfile.Id, message.SenderId, answer);
                messenger.Send(messageAnswer);
            }
        }

        private void InitializeHandlers()
        {
            handlers.Add(Resources.WhatIsHandler, new WhatIsHandler());
            handlers.Add(Resources.MathHandler, new MathHandler());
            handlers.Add(Resources.DefaultHandler, new DefaultHandler());

            var usages = from h in handlers.Values select h.Usage;
            handlers.Add(Resources.HelpHandler, new HelpHandler(usages.ToArray()));
        }
    }
}