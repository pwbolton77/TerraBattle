using System;
using System.Linq;
using TerraBattle.Model;

namespace TerraBattle.UI.Wrapper
{
  public partial class EquipConfigWrapper : ModelWrapper<EquipConfig>
  {
    public EquipConfigWrapper(EquipConfig model) : base(model)
    {
    }

    public System.Int32 EquipConfigId
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 EquipConfigIdOriginalValue => GetOriginalValue<System.Int32>(nameof(EquipConfigId));

    public bool EquipConfigIdIsChanged => GetIsChanged(nameof(EquipConfigId));

    public System.String EquipConfigName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String EquipConfigNameOriginalValue => GetOriginalValue<System.String>(nameof(EquipConfigName));

    public bool EquipConfigNameIsChanged => GetIsChanged(nameof(EquipConfigName));

    public System.String Damage
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String DamageOriginalValue => GetOriginalValue<System.String>(nameof(Damage));

    public bool DamageIsChanged => GetIsChanged(nameof(Damage));
  }
}
