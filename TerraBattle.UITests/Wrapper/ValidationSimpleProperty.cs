using TerraBattle.Model;
using TerraBattle.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace TerraBattle.UITests.Wrapper
{
  [TestClass]
  public class ValidationSimpleProperty
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
    public void ShouldReturnValidationErrorIfFirstNameIsEmpty()
    {
      var wrapper = new UnitConfigWrapper(_friend);
      Assert.IsFalse(wrapper.HasErrors);

      wrapper.FirstName = "";
      Assert.IsTrue(wrapper.HasErrors);

      var errors = wrapper.GetErrors(nameof(wrapper.FirstName)).Cast<string>().ToList();
      Assert.AreEqual(1, errors.Count);
      Assert.AreEqual("Firstname is required", errors.First());

      wrapper.FirstName = "Julia";
      Assert.IsFalse(wrapper.HasErrors);
    }

    [TestMethod]
    public void ShouldRaiseErrorsChangedEventWhenFirstNameIsSetToEmptyAndBack()
    {
      var fired = false;
      var wrapper = new UnitConfigWrapper(_friend);

      wrapper.ErrorsChanged += (s, e) =>
      {
        if (e.PropertyName == nameof(wrapper.FirstName))
        {
          fired = true;
        }
      };

      wrapper.FirstName = "";
      Assert.IsTrue(fired);

      fired = false;
      wrapper.FirstName = "Julia";
      Assert.IsTrue(fired);
    }

    [TestMethod]
    public void ShouldSetIsValid()
    {
      var wrapper = new UnitConfigWrapper(_friend);
      Assert.IsTrue(wrapper.IsValid);

      wrapper.FirstName = "";
      Assert.IsFalse(wrapper.IsValid);

      wrapper.FirstName = "Julia";
      Assert.IsTrue(wrapper.IsValid);
    }

    [TestMethod]
    public void ShouldRaisePropertyChangedEventForIsValid()
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

      wrapper.FirstName = "";
      Assert.IsTrue(fired);

      fired = false;
      wrapper.FirstName = "Julia";
      Assert.IsTrue(fired);
    }

    [TestMethod]
    public void ShouldSetErrorsAndIsValidAfterInitialization()
    {
      _friend.FirstName = "";
      var wrapper = new UnitConfigWrapper(_friend);

      Assert.IsFalse(wrapper.IsValid);
      Assert.IsTrue(wrapper.HasErrors);

      var errors = wrapper.GetErrors(nameof(wrapper.FirstName)).Cast<string>().ToList();
      Assert.AreEqual(1, errors.Count);
      Assert.AreEqual("Firstname is required", errors.First());
    }

    [TestMethod]
    public void ShouldRefreshErrorsAndIsValidWhenRejectingChanges()
    {
      var wrapper = new UnitConfigWrapper(_friend);
      Assert.IsTrue(wrapper.IsValid);
      Assert.IsFalse(wrapper.HasErrors);

      wrapper.FirstName = "";

      Assert.IsFalse(wrapper.IsValid);
      Assert.IsTrue(wrapper.HasErrors);

      wrapper.RejectChanges();

      Assert.IsTrue(wrapper.IsValid);
      Assert.IsFalse(wrapper.HasErrors);
    }
  }
}
