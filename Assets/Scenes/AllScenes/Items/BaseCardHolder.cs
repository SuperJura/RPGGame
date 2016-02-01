using System;

[Serializable]
public class BaseCardHolder : Equipment
{
    public BaseCardHolder()
    {
        Slot = Enumerations.EquipmentSlot.CardHolder;
    }
}
