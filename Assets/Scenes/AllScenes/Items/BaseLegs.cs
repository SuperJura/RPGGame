using System;
using System.Collections.Generic;

[Serializable]
public class BaseLegs : Equipment
{
    public BaseLegs()
    {
        this.Slot = Enumerations.EquipmentSlot.Legs;
    }
} 
