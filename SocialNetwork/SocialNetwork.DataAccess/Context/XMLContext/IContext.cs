using System.Collections.Generic;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.DataAccess.Context
{
    public interface IContext
    {
        void Write<T>(IList<T> entities)
            where T : BaseEntity;

        List<T> Read<T>()
            where T : BaseEntity;
    }
}
