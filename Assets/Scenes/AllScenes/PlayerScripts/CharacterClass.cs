using System;
using System.Collections.Generic;

[Serializable]
public abstract class CharacterClass
{

    public Ability Ability1 { get; set; }
    public Ability Ability2 { get; set; }
    public Ability Ability3 { get; set; }
    public Ability Ability4 { get; set; }

    public abstract List<Ability> GetAbilitysOnLevel(int level);

    public bool ChangeAbility(int oldStaticID, int newStaticID)
    {
        IAbilityDatabase abilityDatabase = Repository.GetAbilityDatabaseInstance();

        if (Ability1.StaticID == oldStaticID)
        {
            Ability1 = abilityDatabase.GetAbility(newStaticID);
            return true;
        }
        if (Ability2.StaticID == oldStaticID)
        {
            Ability2 = abilityDatabase.GetAbility(newStaticID);
            return true;
        }
        if (Ability3.StaticID == oldStaticID)
        {
            Ability3 = abilityDatabase.GetAbility(newStaticID);
            return true;
        }
        if (Ability4.StaticID == oldStaticID)
        {
            Ability4 = abilityDatabase.GetAbility(newStaticID);
            return true;
        }

        return false;
    }
}