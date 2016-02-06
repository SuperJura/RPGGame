using System;

public class Enumerations{

    [Serializable]
    public enum CharClass
    {
        Warrior,
        Mage
    }

    [Serializable]
    public enum Gender
    {
        Male,
        Female
    }

    [Serializable]
    public enum EquipmentSlot
    {   //with their static IDs
        Head,   //H
        Shoulders,  //S
        Chest,  //C
        Legs,   //L
        Feet,   //F
        CardHolder, //A
        Weapon, //W
        Offhand //O
    }

    [Serializable]
    public enum EquipmentQuality
    {
        Common,
        Rare,
        Legendary,
        Unheard
    }
}