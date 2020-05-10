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
  public class ValidationComplexProperty
  {
    private UnitConfig _friend;

    [TestInitialize]
    public void Initialize()
    {
      _friend = new UnitConfig
      {
        FirstName = "Thomas",
        Address = new Address { City = "Müllheim" },
        Emails = new List<FriendEmail>()
      };
    }

    [TestMethod]
    public void ShouldSetIsValidOfRoot()
    {
      var wrapper = new UnitConfigWrapper(_friend);
      Assert.IsTrue(wrapper.IsValid);

      wrapper.Address.City = "";
      Assert.IsFalse(wrapper.IsValid);

      wrapper.Address.City = "Salt Lake City";
      Assert.IsTrue(wrapper.IsValid);
    }

    [TestMethod]
    public void ShouldSetIsValidOfRootAfterInitialization()
    {
      _friend.Address.City = "";
      var wrapper = new UnitConfigWrapper(_friend);
      Assert.IsFalse(wrapper.IsValid);

      wrapper.Address.City = "Salt Lake City";
      Assert.IsTrue(wrapper.IsValid);
    }

    [TestMethod]
    public void ShouldRaisePropertyChangedEventForIsValidOfRoot()
    {
      var fired = false;
      var wrapper = new UnitConfigWrapper(_friend);
      wrapper.PropertyChanged += (s, e) =>
      {
        if (e.PropertyName == nameof(wrapper.IsValid))
        {
          fired = true;
        }
      };
      wrapper.Address.City = "";
      Assert.IsTrue(fired);

      fired = false;
      wrapper.Address.City = "Salt Lake City";
      Assert.IsTrue(fired);
    }
  }
}
