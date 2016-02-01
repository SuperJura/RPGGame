using System;

[Serializable]
public class BaseShoulders : Equipment
{
    public BaseShoulders()
    {
       Slot = Enumerations.EquipmentSlot.Shoulders;
    }
}