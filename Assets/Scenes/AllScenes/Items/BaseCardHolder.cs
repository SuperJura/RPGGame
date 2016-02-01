using System;
using System.Collections.Generic;

[Serializable]
public class BaseCardHolder : Equipment
{
    public BaseCardHolder()
    {
        this.Slot = Enumerations.EquipmentSlot.CardHolder;
    }
}
