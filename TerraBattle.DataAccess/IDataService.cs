using System;
using System.Collections.Generic;
using TerraBattle.Model;

namespace TerraBattle.DataAccess
{
    public interface IDataService : IDisposable
    {
        Friend GetFriendById(int friendId);

        void SaveFriend(Friend friend);

        void DeleteFriend(int friendId);

        IEnumerable<Friend> GetAllFriends();

        IEnumerable<FriendGroup> GetAllFriendGroups();
    }
}
