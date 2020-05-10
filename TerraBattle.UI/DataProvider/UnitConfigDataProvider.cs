using System;
using TerraBattle.DataAccess;
using TerraBattle.Model;

namespace TerraBattle.UI.DataProvider
{
  public class UnitConfigDataProvider : IUnitConfigDataProvider
  {
    private readonly Func<IDataService> _dataServiceCreator;

    public UnitConfigDataProvider(Func<IDataService> dataServiceCreator)
    {
      _dataServiceCreator = dataServiceCreator;
    }

    public UnitConfig GetUnitConfigById(int id)
    {
      using (var dataService = _dataServiceCreator())
      {
        return dataService.GetUnitConfigById(id);
      }
    }

    public void SaveUnitConfig(UnitConfig unitConfig)
    {
      using (var dataService = _dataServiceCreator())
      {
        dataService.SaveUnitConfig(unitConfig);
      }
    }

    public void DeleteUnitConfig(int id)
    {
      using (var dataService = _dataServiceCreator())
      {
        dataService.DeleteUnitConfig(id);
      }
    }
  }
}
