using System;
using System.Collections.Generic;

public interface IAbilityDatabase
{
    List<Ability> GetAbilitys(Enumerations.CharClass charClass, int level);
    Ability GetAbility(int staticID);
}
