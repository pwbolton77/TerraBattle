using System;
using System.Collections.Generic;
using TerraBattle.Model;

namespace TerraBattle.DataAccess
{
    public interface IDataService : IDisposable
    {
        UnitConfig GetUnitConfigById(int unitConfigId);

        void SaveUnitConfig(UnitConfig unitConfig);

        void DeleteUnitConfig(int unitConfigId);

        IEnumerable<UnitConfig> GetAllUnitConfigs();

        IEnumerable<FriendGroup> GetAllFriendGroups();
    }
}
