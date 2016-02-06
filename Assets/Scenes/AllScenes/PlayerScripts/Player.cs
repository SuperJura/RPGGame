using System;

[Serializable]
public class Player
{
    public delegate void OnStatsChangedHandler();
    [field: NonSerialized]
    public event OnStatsChangedHandler OnStatsChanged;

    public string PathToSave { get; set; }

    public int IDLevel { get; set; }
    public float PosX { get; set; }
    public float PosY { get; set; }
    public float PosZ { get; set; }

    public int IDPlayer { get; set; }
    public string PlayerName { get; set; }
    public Enumerations.CharClass CharClass { get; set; }
    public CharacterClass Class { get; set; }
    public Enumerations.Gender Gender { get; set; }

    public float MoveSpeed { get; set; }
    public float JumpForce { get; set; }

    public int PhysicalDMG { get; set; }
    public int MagicDMG { get; set; }
    public float PhysDMGMultiplication { get; set; }
    public float MagicDMGMultiplication { get; set; }

    private int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
        set 
        {
            maxHealth = value;
            CallOnStatsChanged();
        }
    }

    private int maxMana;
    public int MaxMana
    {
        get { return maxMana; }
        set 
        {
            maxMana = value;
            CallOnStatsChanged();
        }
    }

    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            CallOnStatsChanged();
        }
    }

    private int currentMana;
    public int CurrentMana
    {
        get { return currentMana; }
        set
        {
            currentMana = value;
            CallOnStatsChanged();
        }
    }

    private int playerLvl;
    public int PlayerLvl
    {
        get { return playerLvl; }
        set 
        {
            playerLvl = value;
            CallOnStatsChanged();
        }
    }

    private float experience;
    public float Experience
    {
        get { return experience; }
        set 
        { 
            experience = value;
            CallOnStatsChanged();
        }
    }

    public Inventory PlayerInventory;

    public Player()
    {
        PlayerInventory = new Inventory();
    }

    private void CallOnStatsChanged()
    {
        if (OnStatsChanged != null)
        {
            OnStatsChanged();
        }
    }

    public void CalcNewEquiped(Equipment oldEquip, Equipment newEquip)
    {
        
        if (oldEquip != null)
        {
            MaxHealth -= oldEquip.Health;
            MaxMana -= oldEquip.Mana;
            PhysDMGMultiplication -= oldEquip.PhysDMGMultiplication;
            MagicDMGMultiplication -= oldEquip.MagicDMGMultiplication;
            MoveSpeed -= oldEquip.MoveSpeed;
        }

        if (newEquip != null)
        {
            MaxHealth += newEquip.Health;
            MaxMana += newEquip.Mana;
            PhysDMGMultiplication += newEquip.PhysDMGMultiplication;
            MagicDMGMultiplication += newEquip.MagicDMGMultiplication;
            MoveSpeed += newEquip.MoveSpeed;
        }
    }

    public int CalcPhysDMG()
    {
        return (int)(PhysicalDMG * PhysDMGMultiplication);
    }

    public int CalcMagicDMG()
    {
        return (int)(MagicDMG * MagicDMGMultiplication);
    }

    public void RemoveHealth(int amount)
    {
        CurrentHealth -= amount;
    }
}