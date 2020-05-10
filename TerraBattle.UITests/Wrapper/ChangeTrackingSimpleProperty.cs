using TerraBattle.Model;
using TerraBattle.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TerraBattle.UITests.Wrapper
{
  [TestClass]
  public class ChangeTrackingSimpleProperty
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
    public void ShouldStoreOriginalValue()
    {
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      Assert.AreEqual("Thomas", wrapper.FirstNameOriginalValue);

      wrapper.FirstName = "Julia";
      Assert.AreEqual("Thomas", wrapper.FirstNameOriginalValue);
    }

    [TestMethod]
    public void ShouldSetIsChanged()
    {
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      Assert.IsFalse(wrapper.FirstNameIsChanged);
      Assert.IsFalse(wrapper.IsChanged);

      wrapper.FirstName = "Julia";
      Assert.IsTrue(wrapper.FirstNameIsChanged);
      Assert.IsTrue(wrapper.IsChanged);

      wrapper.FirstName = "Thomas";
      Assert.IsFalse(wrapper.FirstNameIsChanged);
      Assert.IsFalse(wrapper.IsChanged);
    }

    [TestMethod]
    public void ShouldRaisePropertyChangedEventForFirstNameIsChanged()
    {
      var fired = false;
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      wrapper.PropertyChanged += (s, e) =>
      {
        if (e.PropertyName == nameof(wrapper.FirstNameIsChanged))
        {
          fired = true;
        }
      };
      wrapper.FirstName = "Julia";
      Assert.IsTrue(fired);
    }

    [TestMethod]
    public void ShouldRaisePropertyChangedEventForIsChanged()
    {
      var fired = false;
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      wrapper.PropertyChanged += (s, e) =>
      {
        if (e.PropertyName == nameof(wrapper.IsChanged))
        {
          fired = true;
        }
      };
      wrapper.FirstName = "Julia";
      Assert.IsTrue(fired);
    }

    [TestMethod]
    public void ShouldAcceptChanges()
    {
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      wrapper.FirstName = "Julia";
      Assert.AreEqual("Julia", wrapper.FirstName);
      Assert.AreEqual("Thomas", wrapper.FirstNameOriginalValue);
      Assert.IsTrue(wrapper.FirstNameIsChanged);
      Assert.IsTrue(wrapper.IsChanged);

      wrapper.AcceptChanges();

      Assert.AreEqual("Julia", wrapper.FirstName);
      Assert.AreEqual("Julia", wrapper.FirstNameOriginalValue);
      Assert.IsFalse(wrapper.FirstNameIsChanged);
      Assert.IsFalse(wrapper.IsChanged);
    }

    [TestMethod]
    public void ShouldRejectChanges()
    {
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      wrapper.FirstName = "Julia";
      Assert.AreEqual("Julia", wrapper.FirstName);
      Assert.AreEqual("Thomas", wrapper.FirstNameOriginalValue);
      Assert.IsTrue(wrapper.FirstNameIsChanged);
      Assert.IsTrue(wrapper.IsChanged);

      wrapper.RejectChanges();

      Assert.AreEqual("Thomas", wrapper.FirstName);
      Assert.AreEqual("Thomas", wrapper.FirstNameOriginalValue);
      Assert.IsFalse(wrapper.FirstNameIsChanged);
      Assert.IsFalse(wrapper.IsChanged);
    }
  }
}
