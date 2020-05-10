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
  public class ChangeNotificationCollectionProperty
  {
    private UnitConfig _unitConfigs;
    private FriendEmail _friendEmail;

    [TestInitialize]
    public void Initialize()
    {
      _friendEmail = new FriendEmail { Email = "thomas@thomasclaudiushuber.com" };
      _unitConfigs = new UnitConfig
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
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      Assert.IsNotNull(wrapper.Emails);
      CheckIfModelEmailsCollectionIsInSync(wrapper);
    }

    [TestMethod]
    public void ShouldBeInSyncAfterRemovingEmail()
    {
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      var emailToRemove = wrapper.Emails.Single(ew => ew.Model == _friendEmail);
      wrapper.Emails.Remove(emailToRemove);
      CheckIfModelEmailsCollectionIsInSync(wrapper);
    }

    [TestMethod]
    public void ShouldBeInSyncAfterAddingEmail()
    {
      _unitConfigs.Emails.Remove(_friendEmail);
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      wrapper.Emails.Add(new FriendEmailWrapper(_friendEmail));
      CheckIfModelEmailsCollectionIsInSync(wrapper);
    }

    [TestMethod]
    public void ShouldBeInSyncAfterClearingEmails()
    {
      var wrapper = new UnitConfigWrapper(_unitConfigs);
      wrapper.Emails.Clear();
      CheckIfModelEmailsCollectionIsInSync(wrapper);
    }

    private void CheckIfModelEmailsCollectionIsInSync(UnitConfigWrapper wrapper)
    {
      Assert.AreEqual(_unitConfigs.Emails.Count, wrapper.Emails.Count);
      Assert.IsTrue(_unitConfigs.Emails.All(e =>
                  wrapper.Emails.Any(we => we.Model == e)));
    }
  }
}
