using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Header("Player Stats")]
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

    public float playerDamage;
    public float playerAttackSpeed;
    public float playerArmour;
    public float playerDuration;

    [Header("Player Abilities")]
    public Ability abilityOne;
    public float abilityOneCoolDown;
    public Ability abilityTwo;
    public float abilityTwoCoolDown;
    public Ability Ultimate;
    public float ultimateCoolDown;


    [Header("Player Movement")]
    public Rigidbody rb;
    public float moveSpeed = 12f;
    public float jumpForce = 10f;
    public bool isGrounded = true;
    public GameObject origin;

    int layerMask = 1 << 8;




    private void FixedUpdate()
    {
        Jumping();
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) { moveSpeed = 24f; }
        else { moveSpeed = 12f; }

        float x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float y = Input.GetAxisRaw("Vertical") * moveSpeed;

        Vector3 movePos = transform.right * x + transform.forward * y ;
        Vector3 newMovePos = new Vector3(movePos.x, rb.velocity.y, movePos.z);
        rb.velocity = newMovePos;
    }

    void Jumping()
    {
        rb.AddForce(new Vector3(0, -35 * Time.deltaTime, 0), ForceMode.Impulse);
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce * Time.deltaTime, 0), ForceMode.Impulse);
            isGrounded = false;
        }
    }


    void CheckForGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2.5f, layerMask))
        {
            isGrounded = true;
        }
    }

    private void OnDrawGizmos()
    {
        //  Gizmos.color = Color.red;
        //   Gizmos.DrawSphere(origin.transform.position, 1f);
    }

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        //these will be set by the game controller once class select is in place
        abilityOne.abilityOwner = this.gameObject;
        abilityTwo.abilityOwner = this.gameObject;
        Ultimate.abilityOwner = this.gameObject;

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
        if (abilityOneCoolDown <= 0 && Input.GetKeyDown(KeyCode.Q))
        {
            abilityOne.DoAbility();
            abilityOneCoolDown = abilityOne.coolDown;
        }
        if (abilityTwoCoolDown <= 0 && Input.GetKeyDown(KeyCode.E))
        {
            abilityTwo.DoAbility();
            abilityTwoCoolDown = abilityTwo.coolDown;
        }
        if (ultimateCoolDown <= 0 && Input.GetKeyDown(KeyCode.X))
        {
            Ultimate.DoAbility();
            ultimateCoolDown = Ultimate.coolDown;
        }
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckForGrounded();
        OnDrawGizmos();
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
        print("took Damage");
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
