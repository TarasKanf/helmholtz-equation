using System.Collections.Generic;

namespace SocialNetwork.Services.Contracts
{
    public interface IParser
    {
        Dictionary<int, int> Parse(string input);
    }
}
