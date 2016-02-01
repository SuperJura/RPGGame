using System;
using System.Collections.Generic;

[Serializable]
public abstract class CharacterClass
{
    public List<Ability> listOfCurrentAbilitys;

    public Ability Ability1 { get; set; }
    public Ability Ability2 { get; set; }
    public Ability Ability3 { get; set; }
    public Ability Ability4 { get; set; }

    public CharacterClass()
    {
        listOfCurrentAbilitys = new List<Ability>();
    }

    public abstract List<Ability> GetAbilitysOnLevel(int level);
}