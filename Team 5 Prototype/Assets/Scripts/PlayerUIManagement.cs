using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManagement : MonoBehaviour
{


    public Image healthDisplay;
    public Image shieldDisplay;

    public Image overHealthDisplay;
    public Image overShieldDisplay;

    public Image abilityOneCooldownDisplay;
    public Image abilityTwoCooldownDisplay;
    public Image ultimateCooldownDisplay;
    public Image abilityOneIcon;
    public Image abilityTwoIcon;
    public Image ultimateIcon;
    private PlayerInputController player;
    public GameObject playerGO;
    // Start is called before the first frame update


    public GameObject topCross;
    private Vector3 topStartPos;

    public GameObject botCross;
    private Vector3 botStartPos;


    public Light spotLight;




    void Start()
    {
        botStartPos = botCross.GetComponent<RectTransform>().position;
        topStartPos = topCross.GetComponent<RectTransform>().position;

        player= playerGO.GetComponent<PlayerInputController>();
        StartCoroutine(waitForStart());

    }

    private IEnumerator waitForStart()
    {
        yield return new WaitForSeconds(0.1f);
        abilityOneIcon.sprite = player.abilityOne.abilitySprite;

        abilityTwoIcon.sprite = player.abilityTwo.abilitySprite;
        ultimateIcon.sprite = player.Ultimate.abilitySprite;
    }
    // Update is called once per frame
    void Update()
    {
        topCross.GetComponent<RectTransform>().position = new Vector3(botStartPos.x, botStartPos.y - spotLight.spotAngle, botStartPos.z);
        botCross.GetComponent<RectTransform>().position = new Vector3(topStartPos.x, topStartPos.y + spotLight.spotAngle, topStartPos.z);
        Display();
    }

    private void Display()
    {
        healthDisplay.fillAmount = (player.playerHealth / player.playerHealthMax);
        shieldDisplay.fillAmount = (player.playerShield / player.playerShieldMax);
        overShieldDisplay.fillAmount = (player.playerOverShield / 100);
        overHealthDisplay.fillAmount = (player.playerOverHealth / 100);

        abilityOneCooldownDisplay.fillAmount = (player.abilityOneCoolDown / player.abilityOne.coolDown);
        abilityTwoCooldownDisplay.fillAmount = (player.abilityTwoCoolDown / player.abilityTwo.coolDown);
        ultimateCooldownDisplay.fillAmount = (player.ultimateCoolDown / player.Ultimate.coolDown);


    }
}
