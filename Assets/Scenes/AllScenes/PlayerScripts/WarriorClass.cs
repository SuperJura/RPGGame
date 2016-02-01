using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class WarriorClass : CharacterClass
{
    private IAbilityDatabase abilityDatabase;
    public WarriorClass()
    {
        abilityDatabase = Repository.GetAbilityDatabaseInstance();
        listOfCurrentAbilitys = abilityDatabase.GetAbilitys(Enumerations.CharClass.Warrior, 1);

        Ability1 = listOfCurrentAbilitys[0];
        Ability2 = listOfCurrentAbilitys[1];
        Ability3 = listOfCurrentAbilitys[2];
        Ability4 = listOfCurrentAbilitys[3];
    }

    public override List<Ability> GetAbilitysOnLevel(int level)
    {
        return abilityDatabase.GetAbilitys(Enumerations.CharClass.Warrior, level);
    }
}