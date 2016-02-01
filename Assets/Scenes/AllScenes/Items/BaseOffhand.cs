using System;

[Serializable]
public class BaseOffhand : Equipment
{
    public BaseOffhand()
    {
        Slot = Enumerations.EquipmentSlot.Offhand;
    }
}
