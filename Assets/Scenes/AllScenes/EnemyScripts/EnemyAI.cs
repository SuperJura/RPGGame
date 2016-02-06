using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    private Transform myTransform;
    private Transform mesh;    //mesh je brat skripte
    private EnemyInformation enemyInfo;
    private Transform targetTransform;
    private Transform myParent;
    private Vector3 initialPosition;
    private bool aggro;
    private float currentAttackCooldown;
    private int currentHealth;

    public int noticeRange = 35;
    public int aggroRange = 25;
    public float moveSpeed = 10;
    public float attackCooldown = 1;
    public float attackRange = 25;

    void Start () {
        myTransform = GetComponentInParent<Transform>();
        myParent = myTransform.parent;
        currentAttackCooldown = attackCooldown;

        aggro = false;

        foreach (Transform child in myParent)
        {
            if (child.name == "Mesh")
            {
                mesh = child;
            }
        }
        enemyInfo = mesh.GetComponent<Enemy>().GetEnemyInformation();
        enemyInfo.OnEnemyDeath += information_OnEnemyDeath;
        currentHealth = enemyInfo.Health;

        initialPosition = mesh.position;
	}

    void information_OnEnemyDeath(EnemyInformation information)
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (myParent.position != initialPosition && aggro == false)
        {
            myParent.position = Vector3.Slerp(myParent.position, initialPosition, Time.deltaTime * moveSpeed);
        }
        if (currentAttackCooldown >= 0)
        {
            currentAttackCooldown -= Time.deltaTime;
        }
        if (enemyInfo.Health != currentHealth)
        {
            targetTransform = enemyInfo.Focus.transform;
            LookAtTarget();
            FollowAndAttackTarget();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            targetTransform = other.transform;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            float distance = Vector3.Distance(targetTransform.position, myParent.position);
            if (distance < noticeRange)    //ako je izmedu notice i aggro
            {
                LookAtTarget();
            }
            if (distance < noticeRange && distance < aggroRange)   //ako je manji od aggro
            {
                FollowAndAttackTarget();
            }
            if(distance > noticeRange) //ako je vece od notice
            {
                ResetAggro();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ResetAggro();
        }
    }

    private void ResetAggro()
    {
        aggro = false;
    }

    private void LookAtTarget()
    {
        Quaternion rotation = Quaternion.LookRotation(targetTransform.position - myParent.position);
        myParent.rotation = Quaternion.Slerp(myParent.rotation, rotation, Time.deltaTime * 5);
    }

    private void FollowAndAttackTarget()
    {
        float distance = Vector3.Distance(targetTransform.position, myParent.position);
        if (distance > 5)
        {
            myParent.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        aggro = true;
        if (currentAttackCooldown <= 0 && distance < attackRange)
        {
            CurrentPlayer.currentPlayer.RemoveHealth(enemyInfo.Attack);
            currentAttackCooldown = attackCooldown;
        }
    }
}