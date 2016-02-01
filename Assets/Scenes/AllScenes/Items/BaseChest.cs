using System;

[Serializable]
public class BaseChest : Equipment
{
    public BaseChest()
    {
        Slot = Enumerations.EquipmentSlot.Chest;
    }
}
