using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public string staticID;
    private EnemyInformation information;
    private EnemyInformationDatabase database;

    public EnemyInformation GetEnemyInformation()
    {
        return information;
    }

	// Use this for initialization
	void Awake () {
        database = GameObject.Find("GameMenager").GetComponent<EnemyInformationDatabase>();
        information = database.GetEnemyInformation(staticID);
	}
}
