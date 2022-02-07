using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerShieldRegen;
    public float playerHealthRegen;

    public float playerRegenTimer;
    public float regenTimerDone;

    public float playerOverHealth;
    public float playerOverShield;

    public float playerHealth;
    public float playerHealthMax;
    public float playerShield;
    public float playerShieldMax;
    public float playerSpeed;
    public float playerDamage;
    public float playerAttackSpeed;
    public float playerArmour;
    public float playerDuration;

    public Ability abilityOne;
    public float abilityOneCoolDown;
    public Ability abilityTwo;
    public float abilityTwoCoolDown;
    public Ability Ultimate;
    public float ultimateCoolDown;


    // Start is called before the first frame update
    void Start()
    {
        abilityOne.abilityOwner = this.gameObject;
        abilityTwo.abilityOwner = this.gameObject;
      //  Ultimate.abilityOwner = this.gameObject;

        abilityOneCoolDown = abilityOne.coolDown;
        StartCoroutine(RegenTimerTicker());
        StartCoroutine(AbilityCoolDowns());
    }

    private IEnumerator AbilityCoolDowns()
    {
        abilityOneCoolDown -= 0.05f;
        abilityTwoCoolDown -= 0.05f;
        ultimateCoolDown -= 0.05f;
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(AbilityCoolDowns());
    }

    private IEnumerator RegenTimerTicker()
    {
        yield return new WaitForSeconds(0.05f);

        if (playerOverShield > 0)
        {
            playerOverShield -= 0.05f;
            playerOverShield -= 0.05f * playerOverShield / 2;
        }
        if (playerOverHealth > 0)
        {
            playerOverHealth -= 0.05f;
            playerOverHealth -= 0.05f * playerOverHealth / 2;

        }
        regenTimerDone -= 0.05f;
        StartCoroutine(RegenTimerTicker());


    }


    void UseAbilities()
    {
        if (abilityOneCoolDown <= 0)
        {
            abilityOne.DoAbility();
            abilityOneCoolDown = abilityOne.coolDown;
        }
        if (abilityTwoCoolDown <= 0)
        {
            abilityTwo.DoAbility();
            abilityTwoCoolDown = abilityTwo.coolDown;
        }
        {
            //if for using abiliteis 

        }
    }


    // Update is called once per frame
    void Update()
    {
        UseAbilities();
        RegenPlayer();
        OverCalcs();
    }

    private void OverCalcs()
    {
        if (playerHealth > playerHealthMax)
        {
            float diff = playerHealth - playerHealthMax;
            playerOverHealth += diff;
            playerHealth -= diff;
        }
        if (playerShield > playerShieldMax)
        {
            float diff = playerShield - playerShieldMax;
            playerOverShield += diff;
            playerShield -= diff;
        }
    }

    private void RegenPlayer()
    {
        if (regenTimerDone <= 0)
        {
            if (playerShield < playerShieldMax)
            {
                playerShield += playerShieldRegen;

            }
            else if (playerHealth < playerHealthMax)
            {
                playerHealth += playerHealthRegen;
            }
            regenTimerDone += 0.05f;
        }
    }

    public void TakeDamage(float Damage)
    {
        regenTimerDone = playerRegenTimer;
        if (playerShield > 0)
        {
            playerShield -= Damage;
        }
        else
        {
            playerHealth -= Damage;
        }
    }

    public void Heal(float Heal)
    {
        playerHealth += Heal;
    }
    public void ShieldRestore(float Energy)
    {
        playerShield += Energy;
    }
}
