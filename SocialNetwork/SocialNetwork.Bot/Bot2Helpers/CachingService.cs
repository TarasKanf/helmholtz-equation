using System.Linq;
using SocialNetwork.DataAccess.UnitOfWork;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Bot.Bot2Helpers
{
    public class CachingService
    {
        private readonly UnitOfWork work;

        public CachingService()
        {
            work = new UnitOfWork();
        }

        /// <summary>
        /// Gets cached answer on request.
        /// </summary>
        /// <param name="handler">Type of request.</param>
        /// <param name="request"></param>
        /// <returns>Null if there is no answer.</returns>
        public string GetAnswer(RequestInfo requestInfo)
        {
            var firstOrDefault = work.HashAnswers.GetAll().FirstOrDefault(
                x => x.QuestionText == requestInfo.Data 
                     && x.Handler.Name == requestInfo.HandlerName);

            if (firstOrDefault != null)
            {
                return firstOrDefault.Answer;
            }

            return string.Empty;
        }

        public void Cache(RequestInfo requestInfo, string answer)
        {
            HashAnswer hashAnswer = new HashAnswer();
            hashAnswer.Answer = answer;
            hashAnswer.QuestionText = requestInfo.Data;

            var firstOrDefault = work.HandlerTypes.GetAll().FirstOrDefault(
                x => x.Name == requestInfo.HandlerName);

            if (firstOrDefault != null)
            {
                hashAnswer.HandlerTypeId = firstOrDefault.Id;
            }
            else
            {
                // Create handler type if note exists
                HandlerType handler = new HandlerType()
                {
                    Name = requestInfo.HandlerName
                };
                work.HandlerTypes.Create(handler);
                work.Save();
                hashAnswer.HandlerTypeId =
                    work.HandlerTypes.GetAll().First(x => x.Name == requestInfo.HandlerName).Id;
            }

            work.HashAnswers.Create(hashAnswer);
            work.Save();
        }
    }
}
