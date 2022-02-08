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
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        abilityOneIcon.sprite = player.abilityOne.abilitySprite;
        abilityTwoIcon.sprite = player.abilityTwo.abilitySprite;
        ultimateIcon.sprite = player.Ultimate.abilitySprite;
}


    // Update is called once per frame
    void Update()
    {
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
