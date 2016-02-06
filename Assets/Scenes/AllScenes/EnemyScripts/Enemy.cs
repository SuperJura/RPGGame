using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public string staticID;
    private EnemyInformation information;

    public EnemyInformation GetEnemyInformation()
    {
        return information;
    }

	// Use this for initialization
	void Awake () {
        IEnemyDatabase enemyDatabase = Repository.GetEnemyDatabaseInstance();
        information = enemyDatabase.GetEnemyInformation(staticID);
	}
}