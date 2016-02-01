using System;

[Serializable]
public class BaseWeapon : Equipment
{
    public BaseWeapon()
    {
        Slot = Enumerations.EquipmentSlot.Weapon;
    }
}