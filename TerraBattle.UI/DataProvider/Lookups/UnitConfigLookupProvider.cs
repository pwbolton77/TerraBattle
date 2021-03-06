﻿using System;
using System.Collections.Generic;
using System.Linq;
using TerraBattle.DataAccess;
using TerraBattle.Model;

namespace TerraBattle.UI.DataProvider.Lookups
{
  public class UnitConfigLookupProvider : ILookupProvider<UnitConfig>
  {
    private readonly Func<IDataService> _dataServiceCreator;

    public UnitConfigLookupProvider(Func<IDataService> dataServiceCreator)
    {
      _dataServiceCreator = dataServiceCreator;
    }

    public IEnumerable<LookupItem> GetLookup()
    {
      using (var service = _dataServiceCreator())
      {
        return service.GetAllUnitConfigs()
                .Select(f => new LookupItem
                {
                  Id = f.Id,
                  DisplayValue = string.Format("{0} {1}", f.FirstName, f.LastName)
                })
                .OrderBy(l => l.DisplayValue)
                .ToList();
      }
    }
  }
}