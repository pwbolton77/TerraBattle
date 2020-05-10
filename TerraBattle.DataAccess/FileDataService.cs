﻿using System;
using System.Collections.Generic;
using System.Linq;
using TerraBattle.Model;
using System.IO;
using Newtonsoft.Json;

namespace TerraBattle.DataAccess
{
  public class FileDataService : IDataService
  {
    private const string StorageFile = "Friends.json";

    public UnitConfig GetFriendById(int unitConfigId)
    {
      var unitConfigs = ReadFromFile();
      return unitConfigs.Single(u => u.Id == unitConfigId);
    }

    public void SaveFriend(UnitConfig unit_config)
    {
      if (unit_config.Id <= 0)
      {
        InsertFriend(unit_config);
      }
      else
      {
        UpdateFriend(unit_config);
      }
    }

    public void DeleteFriend(int unitConfigId)
    {
      var unitConfigs = ReadFromFile();
      var existing = unitConfigs.Single(u => u.Id == unitConfigId);
      unitConfigs.Remove(existing);
      SaveToFile(unitConfigs);
    }

    private void UpdateFriend(UnitConfig unit_config)
    {
      var unitConfigs = ReadFromFile();
      var existing = unitConfigs.Single(u => u.Id == unit_config.Id);
      var indexOfExisting = unitConfigs.IndexOf(existing);
      unitConfigs.Insert(indexOfExisting, unit_config);
      unitConfigs.Remove(existing);
      SaveToFile(unitConfigs);
    }

    private void InsertFriend(UnitConfig unit_config)
    {
      var unitConfigs = ReadFromFile();
      var maxFriendId = unitConfigs.Max(f => f.Id);
      unit_config.Id = maxFriendId + 1;
      unitConfigs.Add(unit_config);
      SaveToFile(unitConfigs);
    }

    public IEnumerable<FriendGroup> GetAllFriendGroups()
    {
      // Just yielding back four hard-coded groups
      yield return new FriendGroup { Id = 1, Name = "Family" };
      yield return new FriendGroup { Id = 2, Name = "Friends" };
      yield return new FriendGroup { Id = 3, Name = "Colleague" };
      yield return new FriendGroup { Id = 4, Name = "Other" };
    }

    public IEnumerable<UnitConfig> GetAllFriends()
    {
      return ReadFromFile();
    }

    public void Dispose()
    {
      // Usually Service-Proxies are disposable. This method is added as demo-purpose
      // to show how to use an IDisposable in the client with a Func<T>. =>  Look for example at the FriendDataProvider-class
    }

    private void SaveToFile(List<UnitConfig> unitConfigList)
    {
      string json = JsonConvert.SerializeObject(unitConfigList, Formatting.Indented);
      File.WriteAllText(StorageFile, json);
    }

    private List<UnitConfig> ReadFromFile()
    {
      if (!File.Exists(StorageFile))
      {
        return new List<UnitConfig>
                {
                    new UnitConfig{Id=1,FirstName = "Thomas",LastName="Huber",Address=new Address{City="Müllheim",Street="Elmstreet",StreetNumber = "12345"},
                        Birthday = new DateTime(1980,10,28), IsDeveloper = true,Emails=new List<FriendEmail>{new FriendEmail{Email="thomas@thomasclaudiushuber.com"}},FriendGroupId = 1},
                    new UnitConfig{Id=2,FirstName = "Julia",LastName="Huber",Address=new Address{City="Müllheim",Street="Elmstreet",StreetNumber = "12345"},
                        Birthday = new DateTime(1982,10,10),Emails=new List<FriendEmail>{new FriendEmail{Email="julia@juhu-design.com"}},FriendGroupId = 1},
                    new UnitConfig{Id=3,FirstName="Anna",LastName="Huber",Address=new Address{City="Müllheim",Street="Elmstreet",StreetNumber = "12345"},
                        Birthday = new DateTime(2011,05,13),Emails=new List<FriendEmail>(),FriendGroupId = 1},
                    new UnitConfig{Id=4,FirstName="Sara",LastName="Huber",Address=new Address{City="Müllheim",Street="Elmstreet",StreetNumber = "12345"},
                        Birthday = new DateTime(2013,02,25),Emails=new List<FriendEmail>(),FriendGroupId = 1},
                    new UnitConfig{Id=5,FirstName = "Andreas",LastName="Böhler",Address=new Address{City="Tiengen",Street="Hardstreet",StreetNumber = "5"},
                        Birthday = new DateTime(1981,01,10), IsDeveloper = true,Emails=new List<FriendEmail>{new FriendEmail{Email="andreas@strenggeheim.de"}},FriendGroupId = 2},
                    new UnitConfig{Id=6,FirstName="Urs",LastName="Meier",Address=new Address{City="Bern",Street="Baslerstrasse",StreetNumber = "17"},
                        Birthday = new DateTime(1970,03,5), IsDeveloper = true,Emails=new List<FriendEmail>{new FriendEmail{Email="urs@strenggeheim.ch"}},FriendGroupId = 2},
                     new UnitConfig{Id=7,FirstName="Chrissi",LastName="Heuberger",Address=new Address{City="Hillhome",Street="Freiburgerstrasse",StreetNumber = "32"},
                        Birthday = new DateTime(1987,07,16),Emails=new List<FriendEmail>{new FriendEmail{Email="chrissi@web.de"}},FriendGroupId = 2},
                     new UnitConfig{Id=8,FirstName="Erkan",LastName="Egin",Address=new Address{City="Neuenburg",Street="Rheinweg",StreetNumber = "4"},
                        Birthday = new DateTime(1983,05,23),Emails=new List<FriendEmail>{new FriendEmail{Email="erko@web.de"}},FriendGroupId = 2},
                };
      }

      string json = File.ReadAllText(StorageFile);
      return JsonConvert.DeserializeObject<List<UnitConfig>>(json);
    }
  }
}
