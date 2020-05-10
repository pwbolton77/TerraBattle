using System;
using System.Collections.Generic;

namespace TerraBattle.Model
{
  public class Friend
  {
    public int Id { get; set; }

    public string UnitName { get; set; }

    // @@ PWB: Obslete fields
    public int FriendGroupId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime? Birthday { get; set; }

    public bool IsDeveloper { get; set; }

    public Address Address { get; set; }

    public List<FriendEmail> Emails { get; set; }
  }
}
