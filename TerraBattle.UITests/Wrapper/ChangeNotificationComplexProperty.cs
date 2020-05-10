using TerraBattle.Model;
using TerraBattle.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraBattle.UITests.Wrapper
{
  [TestClass]
  public class ChangeNotificationComplexProperty
  {
    private UnitConfig _friend;

    [TestInitialize]
    public void Initialize()
    {
      _friend = new UnitConfig
      {
        FirstName = "Thomas",
        Address = new Address(),
        Emails = new List<FriendEmail>()
      };
    }

    [TestMethod]
    public void ShouldInitializeAddressProperty()
    {
      var wrapper = new UnitConfigWrapper(_friend);
      Assert.IsNotNull(wrapper.Address);
      Assert.AreEqual(_friend.Address, wrapper.Address.Model);
    }
  }
}
