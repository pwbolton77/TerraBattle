using System;
using System.Collections.Generic;
using TerraBattle.Model;

namespace TerraBattle.DataAccess
{
    public interface IDataService : IDisposable
    {
        UnitConfig GetFriendById(int unitConfigId);

        void SaveFriend(UnitConfig unit_config);

        void DeleteFriend(int unitConfigId);

        IEnumerable<UnitConfig> GetAllFriends();

        IEnumerable<FriendGroup> GetAllFriendGroups();
    }
}
