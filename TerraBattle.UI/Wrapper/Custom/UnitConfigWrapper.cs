using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraBattle.UI.Wrapper
{
    public partial class UnitConfigWrapper
    {
        public static int OffenseRatingMin { get { return 1; } }
        public static int OffenseRatingMax { get { return 100; } }

        public bool OffenseRatingInRange(int value)  { return (value >= OffenseRatingMin && value <= OffenseRatingMax); }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(UnitName))
            {
                yield return new ValidationResult("Unit Name is required",
                  new[] { nameof(UnitName) });
            }

            if (OffenseRating < 1 || OffenseRating > 100)
            {
                var msg = $"Offense Rating min is {OffenseRatingMin} and max is {OffenseRatingMax}";
                yield return new ValidationResult(msg,
                  new[] { nameof(OffenseRating) });
            }

            //if (string.IsNullOrWhiteSpace(FirstName))
            //{
            //    yield return new ValidationResult("Firstname is required",
            //      new[] { nameof(FirstName) });
            //}
            //if (IsDeveloper && Emails.Count == 0)
            //{
            //    yield return new ValidationResult("A developer must have an email-address",
            //      new[] { nameof(IsDeveloper), nameof(Emails) });
            //}
        }
    }
}
