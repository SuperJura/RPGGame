using System;
using System.Collections.Generic;

[Serializable]
public class BaseShoulders : Equipment
{
    public BaseShoulders()
    {
        this.Slot = Enumerations.EquipmentSlot.Shoulders;
    }
}
