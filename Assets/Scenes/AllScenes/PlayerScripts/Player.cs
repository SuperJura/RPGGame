using System;

[Serializable]
public class Player
{
    public delegate void OnStatsChangedHandler();
    [field: NonSerialized]
    public event OnStatsChangedHandler OnStatsChanged;

    public delegate void OnLevelUpHandler();
    [field: NonSerialized]
    public event OnLevelUpHandler OnLevelUp;

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

    private int experience;
    public int Experience
    {
        get { return experience; }
        set 
        { 
            experience = value;
            if (experience >= ExperienceForNextLevel)
            {
                LevelUp();
            }
            CallOnStatsChanged();
        }
    }

    public int experienceForNextLevel;
    public int ExperienceForNextLevel
    {
        get { return experienceForNextLevel; }
        set { experienceForNextLevel = value; }
    }


    public Inventory PlayerInventory;

    public Player()
    {
        PlayerInventory = new Inventory();
        ExperienceForNextLevel = 555;
        experience = 0;
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
            maxHealth -= oldEquip.Health;
            maxMana -= oldEquip.Mana;
            PhysDMGMultiplication -= oldEquip.PhysDMGMultiplication;
            MagicDMGMultiplication -= oldEquip.MagicDMGMultiplication;
            MoveSpeed -= oldEquip.MoveSpeed;
        }

        if (newEquip != null)
        {
            maxHealth += newEquip.Health;
            maxMana += newEquip.Mana;
            PhysDMGMultiplication += newEquip.PhysDMGMultiplication;
            MagicDMGMultiplication += newEquip.MagicDMGMultiplication;
            MoveSpeed += newEquip.MoveSpeed;
        }
        CallOnStatsChanged();
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

    public void LevelUp()
    {
        playerLvl += 1;
        experience = 0;
        experienceForNextLevel *= 2;
        if (CharClass == Enumerations.CharClass.Mage)
        {
            LevelUpMage();
        }
        else if (CharClass == Enumerations.CharClass.Warrior)
        {
            LevelUpWarrior();
        }
        OnLevelUp();
    }

    private void LevelUpWarrior()
    {
        MagicDMG += 1;
        PhysicalDMG += 5;
        maxHealth += 11;
        currentHealth = maxHealth;
        maxMana += 9;
        currentMana = maxMana;
    }

    private void LevelUpMage()
    {
        MagicDMG += 5;
        PhysicalDMG += 2;
        maxHealth += 10;
        currentHealth = maxHealth;
        maxMana += 15;
        currentMana = maxMana;
    }
}