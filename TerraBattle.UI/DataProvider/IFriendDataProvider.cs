using TerraBattle.Model;

namespace TerraBattle.UI.DataProvider
{
  public interface IFriendDataProvider
  {
    BattleUnit GetFriendById(int id);

    void SaveFriend(BattleUnit friend);

    void DeleteFriend(int id);
  }
}