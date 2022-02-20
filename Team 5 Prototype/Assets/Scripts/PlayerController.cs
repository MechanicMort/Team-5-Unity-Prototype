using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{


    public Image debugColour;

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

    [Header("Weapons")]
    public WeaponStats heldWeapon;
    private GunScript thisGun;

    [Header("Player Class")]
    public ClassHolder currentClass;


    [Header("Player Abilities")]
    public Ability abilityOne;
    public float abilityOneCoolDown;
    public Ability abilityTwo;
    public float abilityTwoCoolDown;
    public Ability Ultimate;
    public float ultimateCoolDown;


    [Header("Player Movement")]
    public Rigidbody rb;
    public float moveSpeed ;
    public float jumpForce;
    public bool isGrounded = true;
    public GameObject origin;

    int layerMask = 1 << 8;



    // Start is called before the first frame update
    void Start()
    {
        thisGun = GetComponent<GunScript>();
        rb = GetComponent<Rigidbody>();
        //these will be set by the game controller once class select is in place
        abilityOne.abilityOwner = this.gameObject;
        abilityTwo.abilityOwner = this.gameObject;
        Ultimate.abilityOwner = this.gameObject;

        abilityOneCoolDown = abilityOne.coolDown;
        StartCoroutine(RegenTimerTicker());
        StartCoroutine(AbilityCoolDowns());
        ApplyWeaponStats();
        ApplyClass();
    }
    void Update()
    {
        Movement();
        CheckForGrounded();
        UseAbilities();
        RegenPlayer();
        OverCalcs();
        getColour();
    }

    private void getColour()
    {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            RaycastHit raycastHit;
            
            if (Physics.Raycast(transform.position, -Vector3.up, out raycastHit))
            {
                Renderer renderer = raycastHit.collider.GetComponent<MeshRenderer>();
                Texture2D texture2D = renderer.material.GetTexture("_TexMask") as Texture2D;
                Vector2 pCoord = raycastHit.textureCoord2;
                print(pCoord);
                pCoord.x *= texture2D.width;
                pCoord.y *= texture2D.height;
                Vector2 tiling = renderer.material.mainTextureScale;
                Color color = texture2D.GetPixel(Mathf.FloorToInt(pCoord.x ), Mathf.FloorToInt(pCoord.y));
                debugColour.color = color;
            }
            
        }
    }
    public void ApplyWeaponStats()
    {
        thisGun.fireRate = heldWeapon.fireRate;
        thisGun.magazineSize = heldWeapon.magSize;
        thisGun.damage = heldWeapon.damage;
        thisGun.spotLight.spotAngle = heldWeapon.accuracy;
        thisGun.speed = heldWeapon.pelletSpeed;
        thisGun.shots = heldWeapon.pelletCount;
        
    }
    public void ApplyClass()
    {
        playerHealthMax = currentClass.HealthMax;
        playerShieldMax = currentClass.ShieldMax;
        playerShieldRegen = currentClass.ShieldRegen;
        playerHealthRegen = currentClass.HealthRegen;   
        moveSpeed = currentClass.moveSpeed;
        jumpForce = currentClass.jumpForce;

    }

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


    private IEnumerator AbilityCoolDowns()
    {
        if (abilityOneCoolDown > 0f)
        {
            abilityOneCoolDown -= 0.05f;
        }
        if (abilityTwoCoolDown > 0f)
        {
            abilityTwoCoolDown -= 0.05f;
        }
        if (ultimateCoolDown > 0f)
        {
            ultimateCoolDown -= 0.05f;
        }
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
