using UnityEngine;

public class Enemy : MonoBehaviour {

    public string staticID;
    private EnemyInformation information;
    private IEnemyDatabase enemyDatabase;

    public EnemyInformation GetEnemyInformation()
    {
        return information;
    }

    void Awake () {
        enemyDatabase = Repository.GetEnemyDatabaseInstance();
        information = enemyDatabase.GetEnemyInformation(staticID);
    }
}