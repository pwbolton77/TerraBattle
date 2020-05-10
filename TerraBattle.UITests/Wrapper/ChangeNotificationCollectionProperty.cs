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
  public class ChangeNotificationCollectionProperty
  {
    private BattleUnit _friend;
    private FriendEmail _friendEmail;

    [TestInitialize]
    public void Initialize()
    {
      _friendEmail = new FriendEmail { Email = "thomas@thomasclaudiushuber.com" };
      _friend = new BattleUnit
      {
        FirstName = "Thomas",
        Address = new Address(),
        Emails = new List<FriendEmail>
        {
          new FriendEmail {Email="julia@juhu-design.com" },
          _friendEmail,
        }
      };
    }

    [TestMethod]
    public void ShouldInitializeEmailsProperty()
    {
      var wrapper = new BattleUnitWrapper(_friend);
      Assert.IsNotNull(wrapper.Emails);
      CheckIfModelEmailsCollectionIsInSync(wrapper);
    }

    [TestMethod]
    public void ShouldBeInSyncAfterRemovingEmail()
    {
      var wrapper = new BattleUnitWrapper(_friend);
      var emailToRemove = wrapper.Emails.Single(ew => ew.Model == _friendEmail);
      wrapper.Emails.Remove(emailToRemove);
      CheckIfModelEmailsCollectionIsInSync(wrapper);
    }

    [TestMethod]
    public void ShouldBeInSyncAfterAddingEmail()
    {
      _friend.Emails.Remove(_friendEmail);
      var wrapper = new BattleUnitWrapper(_friend);
      wrapper.Emails.Add(new FriendEmailWrapper(_friendEmail));
      CheckIfModelEmailsCollectionIsInSync(wrapper);
    }

    [TestMethod]
    public void ShouldBeInSyncAfterClearingEmails()
    {
      var wrapper = new BattleUnitWrapper(_friend);
      wrapper.Emails.Clear();
      CheckIfModelEmailsCollectionIsInSync(wrapper);
    }

    private void CheckIfModelEmailsCollectionIsInSync(BattleUnitWrapper wrapper)
    {
      Assert.AreEqual(_friend.Emails.Count, wrapper.Emails.Count);
      Assert.IsTrue(_friend.Emails.All(e =>
                  wrapper.Emails.Any(we => we.Model == e)));
    }
  }
}
