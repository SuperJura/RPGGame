using System;
using System.Collections.Generic;

[Serializable]
public class BaseFeet : Equipment
{
    public BaseFeet()
    {
        this.Slot = Enumerations.EquipmentSlot.Feet;
    }
}
