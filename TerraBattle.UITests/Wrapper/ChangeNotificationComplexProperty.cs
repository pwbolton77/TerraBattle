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
    private UnitConfig _unitConfigs;

    [TestInitialize]
    public void Initialize()
    {
      _unitConfigs = new UnitConfig
      {
        FirstName = "Thomas",
        Address = new Address(),
        Emails = new List<FriendEmail>()
      };
    }

    [TestMethod]
    public void ShouldInitializeAddressProperty()
    {
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      Assert.IsNotNull(wrapper.Address);
      Assert.AreEqual(_unitConfigs.Address, wrapper.Address.Model);
    }
  }
}
