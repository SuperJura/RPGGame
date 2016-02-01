using System;
using System.Collections.Generic;

[Serializable]
public class BaseWeapon : Equipment
{
    public BaseWeapon()
    {
        this.Slot = Enumerations.EquipmentSlot.Weapon;
    }
}
