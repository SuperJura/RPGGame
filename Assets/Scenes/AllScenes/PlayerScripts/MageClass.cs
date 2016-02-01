using System;
using System.Collections.Generic;

[Serializable]
class MageClass : CharacterClass
{
    private IAbilityDatabase abilityDatabase;
    public MageClass()
    {
        abilityDatabase = Repository.GetAbilityDatabaseInstance();
        listOfCurrentAbilitys = abilityDatabase.GetAbilitys(Enumerations.CharClass.Mage, 1);

        Ability1 = listOfCurrentAbilitys[0];
        Ability2 = listOfCurrentAbilitys[1];
        Ability3 = listOfCurrentAbilitys[2];
        Ability4 = listOfCurrentAbilitys[3];
    }

    public override List<Ability> GetAbilitysOnLevel(int level)
    {
        return abilityDatabase.GetAbilitys(Enumerations.CharClass.Mage, level);
    }
}
