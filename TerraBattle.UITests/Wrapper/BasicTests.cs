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

    [TestMethod()]
    public void ShouldContainModelInModelProperty()
    {
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      Assert.AreEqual(_unitConfigs, wrapper.Model);
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
        _unitConfigs.Address = null;
        var wrapper = new UnitConfigWrapper(_unitConfigs);
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
        _unitConfigs.Emails = null;
        var wrapper = new UnitConfigWrapper(_unitConfigs);
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
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      Assert.AreEqual(_unitConfigs.FirstName, wrapper.FirstName);
    }

    [TestMethod]
    public void ShouldSetValueOfUnderlyingModelProperty()
    {
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      wrapper.FirstName = "Julia";
      Assert.AreEqual("Julia", _unitConfigs.FirstName);
    }
  }
}