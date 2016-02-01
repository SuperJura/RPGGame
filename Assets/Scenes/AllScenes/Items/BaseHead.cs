using System;
using System.Collections;

[Serializable]
public class BaseHead : Equipment{

    public BaseHead()
    {
        Slot = Enumerations.EquipmentSlot.Head;
    }
}
