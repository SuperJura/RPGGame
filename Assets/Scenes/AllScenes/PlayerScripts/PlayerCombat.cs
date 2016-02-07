using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour {

    public delegate void OnTargetChangedHandler(EnemyInformation info);
    public event OnTargetChangedHandler OnTargetChanged;

    public delegate void OnTargetHealthChangedHandler(EnemyInformation info);
    public event OnTargetHealthChangedHandler OnTargetHealthChanged;

    public delegate void OnAbilityFiredHandler(int abilityIndex);
    public event OnAbilityFiredHandler OnAbiliyFired;   //za GUI (da se pokaze da je pritisnut)

    public delegate void OnAbilityCooldownHandler(float cooldown, int index);   //cooldown je izmedu 0 i 1 ( 1 znaci da se moze koristiti opet)
    public event OnAbilityCooldownHandler OnAbilityCooldown;    //za GUI (da se pokaze cooldown)

    public delegate void OnExperienceGainedHandler();
    public event OnExperienceGainedHandler OnExperienceGained;

    private EnemyInformation information;
    public EnemyInformation EnemyInfo {
        get
        {
            return information;
        }
    }

    private Transform targetForCombat;  //enemy
    public Transform TargetForCombat
    {
        get { return targetForCombat; }
        set
        {
            if (targetForCombat == value)
            {
                return;
            }
            targetForCombat = value;

            try
            {
                information.OnEnemyDeath -= information_OnEnemyDeath;   //makni prijasnjeg enemya
                Debug.Log("Uspio sam nekoga maknuti s information_OnEnemyDeath");
            }
            catch (System.Exception)
            {
                Debug.Log("Nisam uspio nikoga maknuti s information_OnEnemyDeath");
            }
            information = (TargetForCombat.GetComponent<Enemy>()).GetEnemyInformation();
            information.OnEnemyDeath += information_OnEnemyDeath;
            
            OnTargetChanged(information);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AttackAbility1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AttackAbility2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AttackAbility3();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AttackAbility4();
        }
    }

    public void AttackAbility1()
    {
        Ability ab = CurrentPlayer.currentPlayer.Class.Ability1;
        AttackWithAbility(ab, 1);
    }

    public void AttackAbility2()
    {
        Ability ab = CurrentPlayer.currentPlayer.Class.Ability2;
        AttackWithAbility(ab, 2);
    }

    public void AttackAbility3()
    {
        Ability ab = CurrentPlayer.currentPlayer.Class.Ability3;
        AttackWithAbility(ab, 3);
    }

    public void AttackAbility4()
    {
        Ability ab = CurrentPlayer.currentPlayer.Class.Ability4;
        AttackWithAbility(ab, 4);
    }

    private void AttackWithAbility(Ability ab, int index)
    {
        if (targetForCombat != null && Vector3.Distance(this.transform.position, targetForCombat.position) < ab.Range && ab.CurrentCooldown <= 0)
        {
            if (EnemyInfo.IsDead)
            {
                return;
            }
            information.RemoveHealth(ab.Damage, this.gameObject);
            OnTargetChanged(information);
            OnTargetHealthChanged(information);
            StartCoroutine(DecreaseAbilityCooldown(ab, index));
        }
        OnAbiliyFired(index);
    }
    
    void information_OnEnemyDeath(EnemyInformation information)
    {
        Debug.Log("Neprijatelj je umro: " + information.IdEnemy);
        CurrentPlayer.currentPlayer.Experience += information.ExpGained;
        OnExperienceGained();
    }

    public IEnumerator DecreaseAbilityCooldown(Ability ab, int index)
    {
        ab.CurrentCooldown = ab.Cooldown;
        while (ab.CurrentCooldown >= 0)
        {
            ab.CurrentCooldown -= 0.05f;
            float fillAmount = 1 - (ab.CurrentCooldown / ab.Cooldown);
            OnAbilityCooldown(fillAmount, index);
            yield return new WaitForSeconds(0.05f);
        }
    }

}