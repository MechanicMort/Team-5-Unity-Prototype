using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInputController : MonoBehaviour
{
    //private CharacterController controller;
    public Camera camera;
    public Transform player;


    private Vector2 movementInput = Vector2.zero;

    private Vector2 cameraInput = Vector2.zero;
    private float mX, mY;
    public float mouseSen = 1f;



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

    public float fireRateMulti = 1.0f;

    [Header("Weapons")]
    public WeaponStats heldWeapon;
    private GunScript thisGun;
    private bool isShoot;

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

    private Rigidbody rb;
    public bool isGrounded = true;

    [SerializeField]
    private float moveSpeed = 2.0f;
    private float squidSpeed;
    public float abilitySpeed = 1.0f;
    private bool isAttemptSquid;
    public bool hasDoubleJump;
    public bool hasDoubleJumpUp;
    [SerializeField]
    private float jumpForce = 10f;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ApplyClass();
        thisGun = GetComponent<GunScript>();
        ApplyWeaponStats();
        rb = GetComponent<Rigidbody>();
        //these will be set by the game controller once class select is in place

        abilityOneCoolDown = abilityOne.coolDown;
        StartCoroutine(RegenTimerTicker());
        StartCoroutine(AbilityCoolDowns());
        if (currentClass.name == "Scout")
        {
            hasDoubleJump = true;
        }
    }

    private void CheckDead()
    {
        if (playerHealth<= 0)
        {
            FullReset();
           transform.position = GameObject.FindGameObjectsWithTag("Spawn Point")[Random.Range(0,GameObject.FindGameObjectsWithTag("Spawn Point").Length-1) ].transform.position;
        }
    }
    public void RestoreValue(float time)
    {
        StartCoroutine(RestoreNormal(time));
    }
    public IEnumerator RestoreNormal(float time)
    {
        yield return new WaitForSeconds(time);
        abilitySpeed = 1f;
        fireRateMulti = 1f;
    }
    void Update()
    {
        CheckForGrounded();
        RegenPlayer();
        OverCalcs();
        Movement();
        GetColour();
        CheckDead();
        thisGun.fireRateMod = fireRateMulti;
        if (isShoot)
        {
            
            thisGun.ShootBullet();
        }
    }


    private void Movement()
    {
        float x = movementInput.x * moveSpeed * squidSpeed * abilitySpeed;
        float y = movementInput.y * moveSpeed * squidSpeed * abilitySpeed;

        Vector3 movePos = transform.right * x + transform.forward * y;
        Vector3 newMovePos = new Vector3(movePos.x, rb.velocity.y, movePos.z);
        rb.velocity = newMovePos;


    }


    public void GetSquid(InputAction.CallbackContext context)
    {
        if (context.action.enabled)
        {
            isAttemptSquid = true;
        }
        if (context.canceled)
        {
            isAttemptSquid = false;
        }
    }
    public void GetColour()
    {

        RaycastHit raycastHit;

        if (Physics.Raycast(transform.position, -Vector3.up, out raycastHit) && isAttemptSquid)
        {
            Renderer renderer = raycastHit.collider.GetComponent<MeshRenderer>();
            Texture2D texture2D = renderer.material.GetTexture("_TexMask") as Texture2D;
            Vector2 pCoord = raycastHit.textureCoord2;
            pCoord.x *= texture2D.width;
            pCoord.y *= texture2D.height;
            Color color = texture2D.GetPixel(Mathf.FloorToInt(pCoord.x), Mathf.FloorToInt(pCoord.y));
            if (color.r >= 0.8f && gameObject.layer == 10)
            {
                squidSpeed = 2f;
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.25f, 0.25f, 1.25f), 0.05f);
                thisGun.isSquid = true;
                thisGun.Reload();
            }
            else if ( color.g >= 0.8f && gameObject.layer == 11)
            {
                squidSpeed = 2f;

                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.25f, 0.25f, 1.25f), 0.05f);
                thisGun.isSquid = true;
                thisGun.Reload();
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.25f, 2f, 1.25f), 0.05f);
                squidSpeed = 1f;
                thisGun.isSquid = false;
            }
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.25f, 2f, 1.25f), 0.05f);
            squidSpeed = 1f;
            thisGun.isSquid = false;
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
        thisGun.accuracy = heldWeapon.accuracy;
        thisGun.recoilAmount = heldWeapon.recoilAmount;

    }
    public void ApplyClass()
    {
        playerHealthMax = currentClass.HealthMax;
        playerHealth = currentClass.HealthMax;
        playerShieldMax = currentClass.ShieldMax;
        playerShield = currentClass.ShieldMax;
        playerShieldRegen = currentClass.ShieldRegen;
        playerHealthRegen = currentClass.HealthRegen;
        moveSpeed = currentClass.moveSpeed;
        jumpForce = currentClass.jumpForce;
        abilityOne = currentClass.ability1;
        abilityTwo = currentClass.ability2;
        Ultimate = currentClass.Ultimate;
        abilityOne.abilityOwner = this.gameObject;
        abilityTwo.abilityOwner = this.gameObject;
        Ultimate.abilityOwner = this.gameObject;
    }

    private void FullReset()
    {
        ApplyClass();
        ApplyWeaponStats();
    }

    private IEnumerator AbilityCoolDowns()
    {
        if (abilityOneCoolDown > 0f)
        {
            abilityOneCoolDown -= 0.05f;
        }
        if (abilityTwoCoolDown > 0f && !hasDoubleJump)
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


    public void UseAbility1(InputAction.CallbackContext context)
    {
        if (abilityOneCoolDown <= 0 && squidSpeed == 1)
        {
            abilityOne.DoAbility();
            abilityOneCoolDown = abilityOne.coolDown;
        }
    }
    public void UseAbility2(InputAction.CallbackContext context)
    {

        if (abilityTwoCoolDown <= 0 && squidSpeed == 1)
        {
            abilityTwo.DoAbility();
            abilityTwoCoolDown = abilityTwo.coolDown;
        }
    }

   public void UseUltimate(InputAction.CallbackContext context)
    {

        if (ultimateCoolDown <= 0 && squidSpeed == 1)
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



    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (CheckForGrounded())
            {
                Debug.Log("Jumped");
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
            else if (!CheckForGrounded() && hasDoubleJumpUp)
            {
                hasDoubleJumpUp = false;
                abilityTwoCoolDown = abilityTwo.coolDown;
                Debug.Log("Double Jumped");
                rb.AddForce(new Vector3(0, jumpForce * 1.3f, 0), ForceMode.Impulse);
            }
        }
           

    }

    public void OnCamera(InputAction.CallbackContext context)
    {
        cameraInput = context.ReadValue<Vector2>();

        mX += cameraInput.x * mouseSen;
        mY -= cameraInput.y * mouseSen;
        mY = Mathf.Clamp(mY, -80, 80);
        camera.transform.rotation = Quaternion.Euler(mY, mX, 0);
        player.transform.rotation = Quaternion.Euler(0, mX, 0);



    }


    private bool CheckForGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit,2f))
        {
            if (hasDoubleJump)
            {
                hasDoubleJumpUp = true;
                abilityTwoCoolDown = 0;
            }

            return true;
        }
        return false;

    }


    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.action.enabled)
        {
            isShoot = true;
        }
        if (context.canceled)
        {
            isShoot = false;
        }

    }



}
