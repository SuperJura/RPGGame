using System;
using System.Collections.Generic;

[Serializable]
public class BaseOffhand : Equipment
{
    public BaseOffhand()
    {
        this.Slot = Enumerations.EquipmentSlot.Offhand;
    }
}
