using System.Collections.Generic;

namespace TerraBattle.UI.DataProvider.Lookups
{
  public interface ILookupProvider<T>
  {
    IEnumerable<LookupItem> GetLookup();
  }
}
