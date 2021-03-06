﻿using TerraBattle.Model;
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
 public class ChangeNotificationSimpleProperty
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
    public void ShouldRaisePropertyChangedEventOnPropertyChange()
    {
      var fired = false;
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      wrapper.PropertyChanged += (s, e) =>
      {
        if (e.PropertyName == "FirstName")
        {
          fired = true;
        }
      };
      wrapper.FirstName = "Julia";
      Assert.IsTrue(fired);
    }

    [TestMethod]
    public void ShouldNotRaisePropertyChangedEventIfPropertyIsSetToSameValue()
    {
      var fired = false;
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      wrapper.PropertyChanged += (s, e) =>
      {
        if (e.PropertyName == "FirstName")
        {
          fired = true;
        }
      };
      wrapper.FirstName = "Thomas";
      Assert.IsFalse(fired);
    }
  }
}
