using System;

[Serializable]
public class BaseLegs : Equipment
{
    public BaseLegs()
    {
        Slot = Enumerations.EquipmentSlot.Legs;
    }
} 
