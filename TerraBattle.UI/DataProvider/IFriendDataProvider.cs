using TerraBattle.Model;

namespace TerraBattle.UI.DataProvider
{
  public interface IFriendDataProvider
  {
    UnitConfig GetFriendById(int id);

    void SaveFriend(UnitConfig friend);

    void DeleteFriend(int id);
  }
}