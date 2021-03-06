using System;
using System.Linq;
using TerraBattle.Model;

namespace TerraBattle.UI.Wrapper
{
  public partial class UnitConfigWrapper : ModelWrapper<UnitConfig>
  {
    public UnitConfigWrapper(UnitConfig model) : base(model)
    {
    }

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));

    public bool IdIsChanged => GetIsChanged(nameof(Id));

    public System.String UnitName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String UnitNameOriginalValue => GetOriginalValue<System.String>(nameof(UnitName));

    public bool UnitNameIsChanged => GetIsChanged(nameof(UnitName));

    public System.Int32 OffenseRating
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 OffenseRatingOriginalValue => GetOriginalValue<System.Int32>(nameof(OffenseRating));

    public bool OffenseRatingIsChanged => GetIsChanged(nameof(OffenseRating));

    public System.Int32 FriendGroupId
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 FriendGroupIdOriginalValue => GetOriginalValue<System.Int32>(nameof(FriendGroupId));

    public bool FriendGroupIdIsChanged => GetIsChanged(nameof(FriendGroupId));

    public System.String FirstName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String FirstNameOriginalValue => GetOriginalValue<System.String>(nameof(FirstName));

    public bool FirstNameIsChanged => GetIsChanged(nameof(FirstName));

    public System.String LastName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String LastNameOriginalValue => GetOriginalValue<System.String>(nameof(LastName));

    public bool LastNameIsChanged => GetIsChanged(nameof(LastName));

    public System.Nullable<System.DateTime> Birthday
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.DateTime> BirthdayOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(Birthday));

    public bool BirthdayIsChanged => GetIsChanged(nameof(Birthday));

    public System.Boolean IsDeveloper
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean IsDeveloperOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsDeveloper));

    public bool IsDeveloperIsChanged => GetIsChanged(nameof(IsDeveloper));
 
    public AddressWrapper Address { get; private set; }
 
    public ChangeTrackingCollection<EquipConfigWrapper> EquipConfigs { get; private set; }
 
    public ChangeTrackingCollection<FriendEmailWrapper> Emails { get; private set; }
    
    protected override void InitializeComplexProperties(UnitConfig model)
    {
      if (model.Address == null)
      {
        throw new ArgumentException("Address cannot be null");
      }
      Address = new AddressWrapper(model.Address);
      RegisterComplex(Address);
    }

    protected override void InitializeCollectionProperties(UnitConfig model)
    {
      if (model.EquipConfigs == null)
      {
        throw new ArgumentException("EquipConfigs cannot be null");
      }
 
      EquipConfigs = new ChangeTrackingCollection<EquipConfigWrapper>(
        model.EquipConfigs.Select(e => new EquipConfigWrapper(e)));
      RegisterCollection(EquipConfigs, model.EquipConfigs);
      if (model.Emails == null)
      {
        throw new ArgumentException("Emails cannot be null");
      }
 
      Emails = new ChangeTrackingCollection<FriendEmailWrapper>(
        model.Emails.Select(e => new FriendEmailWrapper(e)));
      RegisterCollection(Emails, model.Emails);
    }
  }
}
