using TerraBattle.Model;

namespace TerraBattle.UI.DataProvider
{
  public interface IUnitConfigDataProvider
  {
    UnitConfig GetUnitConfigById(int id);

    void SaveUnitConfig(UnitConfig unitConfig);

    void DeleteUnitConfig(int id);
  }
}