using TerraBattle.Model;
using TerraBattle.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace TerraBattle.UITests.Wrapper
{
  [TestClass]
  public class ValidationCollectionProperty
  {
    private UnitConfig _unitConfigs;

    [TestInitialize]
    public void Initialize()
    {
      _unitConfigs = new UnitConfig
      {
        FirstName = "Thomas",
        Address = new Address { City = "Müllheim" },
        Emails = new List<FriendEmail>
        {
          new FriendEmail { Email="thomas@thomasclaudiushuber.com" },
          new FriendEmail {Email="julia@juhu-design.com" }
        }
      };
    }

    [TestMethod]
    public void ShouldSetIsValidOfRoot()
    {
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      Assert.IsTrue(wrapper.IsValid);

      wrapper.Emails.First().Email = "";
      Assert.IsFalse(wrapper.IsValid);

      wrapper.Emails.First().Email = "thomas@thomasclaudiushuber.com";
      Assert.IsTrue(wrapper.IsValid);
    }

    [TestMethod]
    public void ShouldSetIsValidOfRootWhenInitializing()
    {
      _unitConfigs.Emails.First().Email = "";
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      Assert.IsFalse(wrapper.IsValid);
      Assert.IsFalse(wrapper.HasErrors);
      Assert.IsTrue(wrapper.Emails.First().HasErrors);
    }

    [TestMethod]
    public void ShouldSetIsValidOfRootWhenRemovingInvalidItem()
    {
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      Assert.IsTrue(wrapper.IsValid);

      wrapper.Emails.First().Email = "";
      Assert.IsFalse(wrapper.IsValid);

      wrapper.Emails.Remove(wrapper.Emails.First());
      Assert.IsTrue(wrapper.IsValid);
    }

    [TestMethod]
    public void ShouldSetIsValidOfRootWhenAddingInvalidItem()
    {
      var emailToAdd = new FriendEmailWrapper(new FriendEmail());
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      Assert.IsTrue(wrapper.IsValid); ;
      wrapper.Emails.Add(emailToAdd);
      Assert.IsFalse(wrapper.IsValid);
      emailToAdd.Email = "thomas@thomasclaudiushuber.com";
      Assert.IsTrue(wrapper.IsValid);
    }

    [TestMethod]
    public void ShouldRaisePropertyChangedEventForIsValidOfRoot()
    {
      var fired = false;
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      wrapper.PropertyChanged += (s, e) =>
      {
        if (e.PropertyName == "IsValid")
        {
          fired = true;
        }
      };
      wrapper.Emails.First().Email = "";
      Assert.IsTrue(fired);

      fired = false;
      wrapper.Emails.First().Email = "thomas@thomasclaudiushuber.com";
      Assert.IsTrue(fired);
    }

    [TestMethod]
    public void ShouldRaisePropertyChangedEventForIsValidOfRootWhenRemovingInvalidItem()
    {
      var fired = false;
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      wrapper.PropertyChanged += (s, e) =>
      {
        if (e.PropertyName == "IsValid")
        {
          fired = true;
        }
      };
      wrapper.Emails.First().Email = "";
      Assert.IsTrue(fired);

      fired = false;
      wrapper.Emails.Remove(wrapper.Emails.First());
      Assert.IsTrue(fired);
    }

    [TestMethod]
    public void ShouldRaisePropertyChangedEventForIsValidOfRootWhenAddingInvalidItem()
    {
      var fired = false;
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      wrapper.PropertyChanged += (s, e) =>
      {
        if (e.PropertyName == "IsValid")
        {
          fired = true;
        }
      };

      var emailToAdd = new FriendEmailWrapper(new FriendEmail());
      wrapper.Emails.Add(emailToAdd);
      Assert.IsTrue(fired);

      fired = false;
      emailToAdd.Email = "thomas@thomasclaudiushuber.com";
      Assert.IsTrue(fired);
    }
  }
}
