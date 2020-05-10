using TerraBattle.Model;

namespace TerraBattle.UI.DataProvider
{
  public interface IFriendDataProvider
  {
    UnitConfig GetFriendById(int id);

    void SaveFriend(UnitConfig unit_config);

    void DeleteFriend(int id);
  }
}