using System;

[Serializable]
public class Equipment  {

    private static int IDCounter = 0;
    private int iDEquipment;
    public int IDEquipment
    {
        get { return iDEquipment; }
    }

    public string StaticIDEquipment { get; set; }  //static id se NIKADA ne mjenja
    public string Name { get; set; }

    public Enumerations.EquipmentSlot Slot { get; set; }
    public Enumerations.EquipmentQuality Quality { get; set; }

    public int Health { get; set; }
    public int Mana { get; set; }
    public float MoveSpeed { get; set; }
    public float MagicDMGMultiplication { get; set; }
    public float PhysDMGMultiplication { get; set; }
    public int JumpForce { get; set; }

    public Equipment ()
    {
        iDEquipment = ++IDCounter;
        Name = "Generic Equipment";
        Health = 0;
        Mana = 0;
        MoveSpeed = 0;
        MagicDMGMultiplication = 0;
        PhysDMGMultiplication = 0;
        JumpForce = 0;
    }

    static public Equipment GetCopy(Equipment e)
    {
        object copy = Activator.CreateInstance(e.GetType());
        Equipment output = (Equipment)copy;
        output.Name = e.Name;
        output.Health = e.Health;
        output.Mana = e.Mana;
        output.MoveSpeed = e.MoveSpeed;
        output.MagicDMGMultiplication = e.MagicDMGMultiplication;
        output.PhysDMGMultiplication = e.PhysDMGMultiplication;
        output.JumpForce = e.JumpForce;
        output.Slot = e.Slot;
        output.Quality = e.Quality;
        output.StaticIDEquipment = e.StaticIDEquipment;

        return output;
    }
}