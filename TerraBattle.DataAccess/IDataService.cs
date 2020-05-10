using System;
using System.Collections.Generic;
using TerraBattle.Model;

namespace TerraBattle.DataAccess
{
    public interface IDataService : IDisposable
    {
        UnitConfig GetFriendById(int friendId);

        void SaveFriend(UnitConfig friend);

        void DeleteFriend(int friendId);

        IEnumerable<UnitConfig> GetAllFriends();

        IEnumerable<FriendGroup> GetAllFriendGroups();
    }
}
