﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendStorage.UI.Wrapper
{
  public partial class FriendWrapper
  {
    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrWhiteSpace(UnitName))
      {
        yield return new ValidationResult("Unit Name is required",
          new[] { nameof(UnitName) });
      }

      if (string.IsNullOrWhiteSpace(FirstName))
      {
        yield return new ValidationResult("Firstname is required",
          new[] { nameof(FirstName) });
      }
      if (IsDeveloper && Emails.Count == 0)
      {
        yield return new ValidationResult("A developer must have an email-address",
          new[] { nameof(IsDeveloper), nameof(Emails) });
      }
    }
  }
}
