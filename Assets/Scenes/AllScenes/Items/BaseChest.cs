using System;
using System.Collections.Generic;

[Serializable]
public class BaseChest : Equipment
{
    public BaseChest()
    {
        this.Slot = Enumerations.EquipmentSlot.Chest;
    }
}
