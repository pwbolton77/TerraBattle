using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraBattle.UI.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraBattle.Model;

namespace TerraBattle.UI.Wrapper.Tests
{
  [TestClass()]
  public class BasicTests
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

    [TestMethod()]
    public void ShouldContainModelInModelProperty()
    {
      var wrapper = new UnitConfigWrapper(_friend);
      Assert.AreEqual(_friend, wrapper.Model);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ShouldThrowArgumentNullExceptionIfModelIsNull()
    {
      try
      {
        var wrapper = new UnitConfigWrapper(null);
      }
      catch (ArgumentNullException ex)
      {
        Assert.AreEqual("model", ex.ParamName);
        throw;
      }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ShouldThrowArgumentExceptionIfAddressIsNull()
    {
      try
      {
        _friend.Address = null;
        var wrapper = new UnitConfigWrapper(_friend);
      }
      catch (ArgumentException ex)
      {
        Assert.AreEqual("Address cannot be null", ex.Message);
        throw;
      }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ShouldThrowArgumentExceptionIfEmailsCollectionIsNull()
    {
      try
      {
        _friend.Emails = null;
        var wrapper = new UnitConfigWrapper(_friend);
      }
      catch (ArgumentException ex)
      {
        Assert.AreEqual("Emails cannot be null", ex.Message);
        throw;
      }
    }

    [TestMethod]
    public void ShouldGetValueOfUnderlyingModelProperty()
    {
      var wrapper = new UnitConfigWrapper(_friend);
      Assert.AreEqual(_friend.FirstName, wrapper.FirstName);
    }

    [TestMethod]
    public void ShouldSetValueOfUnderlyingModelProperty()
    {
      var wrapper = new UnitConfigWrapper(_friend);
      wrapper.FirstName = "Julia";
      Assert.AreEqual("Julia", _friend.FirstName);
    }
  }
}