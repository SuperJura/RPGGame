using System;
using System.Collections.Generic;

public class CodeEnemyInformationDatabase : IEnemyDatabase
{
    List<EnemyInformation> allEnemys;
    IItemDataBase itemDatabase;

    public CodeEnemyInformationDatabase()
    {
        allEnemys = new List<EnemyInformation>();
        itemDatabase = Repository.GetItemDatabaseInstance();
        FillDatabase();
    }


    private void FillDatabase()
    {
        EnemyInformation ei1 = new EnemyInformation() { StaticID = "E_1", Name = "CUbe", Health = 150, MaxHealth = 150,
            CurrentHealth=150, Attack = 5, ExpGained = 10 };
        ei1.EnemyCard = itemDatabase.GetCard("R_3");
        allEnemys.Add(ei1);
    }

    public EnemyInformation GetEnemyInformation(string staticID)
    {
        foreach (EnemyInformation ei in allEnemys)
        {
            if (ei.StaticID == staticID)
            {
                EnemyInformation output = new EnemyInformation() { StaticID = staticID, Attack = ei.Attack, 
                    EnemyCard = ei.EnemyCard, ExpGained = ei.ExpGained, Health = ei.Health, MaxHealth = ei.MaxHealth, Name = ei.Name};   //vratimo kopiju

                int numberOfItems = UnityEngine.Random.Range(1, 3);
                for (int i = 0; i < numberOfItems; i++)
                {
                    output.DropLoot.Add(itemDatabase.GetRandomEquipment());
                }

                return output;
            }
        }
        return null;
    }
}