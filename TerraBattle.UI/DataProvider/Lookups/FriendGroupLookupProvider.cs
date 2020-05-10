using System;
using System.Collections.Generic;
using System.Linq;
using TerraBattle.DataAccess;
using TerraBattle.Model;

namespace TerraBattle.UI.DataProvider.Lookups
{
  public class FriendGroupLookupProvider : ILookupProvider<FriendGroup>
  {
    private readonly Func<IDataService> _dataServiceCreator;

    public FriendGroupLookupProvider(Func<IDataService> dataServiceCreator)
    {
      _dataServiceCreator = dataServiceCreator;
    }

    public IEnumerable<LookupItem> GetLookup()
    {
      using (var service = _dataServiceCreator())
      {
        return service.GetAllFriendGroups()
                .Select(f => new LookupItem
                {
                  Id = f.Id,
                  DisplayValue = f.Name
                })
                .OrderBy(l => l.DisplayValue)
                .ToList();
      }
    }

  }
}