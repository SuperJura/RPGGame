public static class Repository
{
    static IEnemyDatabase enemyDatabase;
    static IItemDataBase itemDatabase;
    static IAbilityDatabase abilityDatabase;

    public static IEnemyDatabase GetEnemyDatabaseInstance()
    {
        if (enemyDatabase == null)
        {
            enemyDatabase = new CodeEnemyInformationDatabase();
        }

        return enemyDatabase;
    }

    public static IItemDataBase GetItemDatabaseInstance()
    {
        if (itemDatabase == null)
        {
            itemDatabase = new CodeItemDataBase();
        }

        return itemDatabase;
    }

    public static IAbilityDatabase GetAbilityDatabaseInstance()
    {
        if (abilityDatabase == null)
        {
            abilityDatabase = new CodeAbilityDatabase();
        }
        return abilityDatabase;
    }
}
