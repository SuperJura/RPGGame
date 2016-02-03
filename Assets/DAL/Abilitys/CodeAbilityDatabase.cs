using System.Collections.Generic;

public class CodeAbilityDatabase : IAbilityDatabase
{
    Dictionary<int, List<Ability>> allMageAbilitys;  //int = level
    Dictionary<int, List<Ability>> allWarriorAbilitys;

    public CodeAbilityDatabase()
    {
        FillMageAbilitys();
        FillWarriorAbilitys();
    }

    private void FillMageAbilitys()
    {
        allMageAbilitys = new Dictionary<int, List<Ability>>();

        //LEVEL 1
        Ability abFrostbolt = new Ability() { Name = "Icebolt", Damage = 15, ManaCost = 20, StaticID = 101, Range = 50, Cooldown = 3 };
        abFrostbolt.Description = string.Format("Simple bolt made of ice that does {0} damage", abFrostbolt.Damage);

        Ability abFirebolt = new Ability() { Name = "Firebolt", Damage = 25, ManaCost = 25, StaticID = 102, Range = 50, Cooldown = 2 };
        abFirebolt.Description = string.Format("Firebolt that does {0} damage", abFirebolt.Damage);

        Ability abMindslash = new Ability() { Name = "Mindeslah", Damage = 20, ManaCost = 35, StaticID = 103, Range = 25, Cooldown = 6 };
        abMindslash.Description = string.Format("Slash the foes mind for {0} damage", abMindslash.Damage);

        Ability abShoot = new Ability() { Name = "Shoot", Damage = 20, ManaCost = 10, StaticID = 104, Range = 75, Cooldown = 10 };
        abShoot.Description = string.Format("Shoot enemy with your wand for {0} damage", abShoot.Damage);

        allMageAbilitys.Add(1, new List<Ability>() { abFrostbolt, abFirebolt, abMindslash, abShoot });

        //LEVEL 2
        Ability abIceSpike = new Ability() { Name = "Ice Spike", Damage = 50, ManaCost = 70, StaticID = 105, Range = 25, Cooldown = 10 };
        abIceSpike.Description = string.Format("Impale enemy for {0} damage", abIceSpike.Damage);

        allMageAbilitys.Add(2, new List<Ability>(){abIceSpike});
    }

    private void FillWarriorAbilitys()
    {
        allWarriorAbilitys = new Dictionary<int, List<Ability>>();

        //LEVEL 1
        Ability abSlash = new Ability() { Name = "Slash", Damage = 15, ManaCost = 10, StaticID = 1, Range = 15, Cooldown = 3 };
        abSlash.Description = string.Format("Slash your enemy with you weapon for {0} damage", abSlash.Damage);

        Ability abHack = new Ability() { Name = "Hack", Damage = 10, ManaCost = 20, StaticID = 2, Range = 15, Cooldown = 7 };
        abHack.Description = string.Format("Hack your enemy with you weapon for {0} damage", abHack.Damage);

        Ability abBash = new Ability() { Name = "Bash", Damage = 5, ManaCost = 15, StaticID = 3, Range = 15, Cooldown = 3 };
        abBash.Description = string.Format("Bash attack that does {0} damage", abBash.Damage);

        Ability abPummel = new Ability() { Name = "Pummel", Damage = 20, ManaCost = 10, StaticID = 4, Range = 15, Cooldown = 7 };
        abPummel.Description = string.Format("Pummel attack that does {0} damage", abPummel.Damage);

        allWarriorAbilitys.Add(1, new List<Ability>() {abSlash, abHack, abBash, abPummel});

        //LEVEL 2
        Ability abReadySlash = new Ability() { Name = "Ready Slash", Damage = 50, ManaCost = 20, StaticID = 5, Range = 15, Cooldown = 15 };
        abReadySlash.Description = string.Format("Focus your slash so it does {0} damage", abReadySlash.Damage);
        allWarriorAbilitys.Add(2, new List<Ability>() {abReadySlash});
    }

    public List<Ability> GetAbilitys(Enumerations.CharClass charClass, int level)
    {
        switch (charClass)
        {
            case Enumerations.CharClass.Warrior:
                return allWarriorAbilitys[level];
            case Enumerations.CharClass.Mage:
                return allMageAbilitys[level];
            default:
                return null;
        }
    }

    public Ability GetAbility(int staticID)
    {
        Ability ab = null;
        ab = GetAbilityFromDictionary(allMageAbilitys, staticID);
        if (ab == null)
        {
            ab = GetAbilityFromDictionary(allWarriorAbilitys, staticID);
        }

        return ab;
    }

    private Ability GetAbilityFromDictionary(Dictionary<int, List<Ability>> dictionary, int staticID)
    {
        foreach (int key in dictionary.Keys)
        {
            foreach (Ability ab in dictionary[key])
            {
                if (ab.StaticID == staticID)
                {
                    return ab;
                }
            }
        }
        return null;
    }
}