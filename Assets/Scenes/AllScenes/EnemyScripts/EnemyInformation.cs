using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EnemyInformation
{
    public delegate void OnEnemyDeathHandler(EnemyInformation information);
    public event OnEnemyDeathHandler OnEnemyDeath;

    static int idCounter = 0;
    private int idEnemy;

    private int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if (IsDead == false)
            {
                health = value;
                if (health <= 0)
                {
                    OnEnemyDeath(this);
                    health = 0;
                    IsDead = true;
                } 
            }
        }
    }
    
    public int IdEnemy
    {
        get { return idEnemy; }
    }
    
    public string StaticID { get; set; }
    public string Name { get; set; }
    public int Attack { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public GameObject Focus { get; set; }

    public List<Equipment> DropLoot { get; set; }
    public Card EnemyCard { get; set; }
    public int ExpGained { get; set; }

    public EnemyInformation()
    {
        IsDead = false;
        DropLoot = new List<Equipment>();
        idEnemy = ++idCounter;
        Name = "Generic Enemy";
        Attack = 1;
        Health = 1;
        ExpGained = 10;
        MaxHealth = 1;
    }

    public void RemoveFromDropLoot(string staticID)
    {
        foreach (Equipment e in DropLoot)
        {
            if (e.StaticIDEquipment == staticID)
            {
                DropLoot.Remove(e);
                return;
            }
        }
    }

    public void RemoveHealth(int amount, GameObject attacker)
    {
        Health -= amount;
        Focus = attacker;
    }
}