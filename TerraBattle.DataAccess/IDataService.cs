using System;
using System.Collections.Generic;
using TerraBattle.Model;

namespace TerraBattle.DataAccess
{
    public interface IDataService : IDisposable
    {
        BattleUnit GetFriendById(int friendId);

        void SaveFriend(BattleUnit friend);

        void DeleteFriend(int friendId);

        IEnumerable<BattleUnit> GetAllFriends();

        IEnumerable<FriendGroup> GetAllFriendGroups();
    }
}
