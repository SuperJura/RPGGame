using System;
using System.Collections.Generic;

[Serializable]
class MageClass : CharacterClass
{
    public MageClass()   //abilityDatabase nemoze biti variabla u klasi jer je klasa Serializable
    {
        IAbilityDatabase abilityDatabase = Repository.GetAbilityDatabaseInstance();
        List<Ability> listOfCurrentAbilitys = abilityDatabase.GetAbilitys(Enumerations.CharClass.Mage, 1);

        Ability1 = listOfCurrentAbilitys[0];
        Ability2 = listOfCurrentAbilitys[1];
        Ability3 = listOfCurrentAbilitys[2];
        Ability4 = listOfCurrentAbilitys[3];
    }

    public override List<Ability> GetAbilitysOnLevel(int level)
    {
        IAbilityDatabase abilityDatabase = Repository.GetAbilityDatabaseInstance();
        return abilityDatabase.GetAbilitys(Enumerations.CharClass.Mage, level);
    }
}
