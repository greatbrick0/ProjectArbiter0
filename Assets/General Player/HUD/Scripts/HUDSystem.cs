using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDSystem : MonoBehaviour
{
    [SerializeField]
    public List<IconCooldown> abilityIcons = new List<IconCooldown>();

    
    //UIHealth healthRef;

    [SerializeField]
    public HUDGunAmmoScript gunHUDRef;

    [SerializeField]
    public GameObject SanityBar;
    //private float targetSanity = 100;

    [SerializeField]
    public TextMeshProUGUI healthLabel;
    [SerializeField]
    public Image healthBar;
    [SerializeField]
    public DamageOpacity damageGradient;

    [SerializeField]
    public TextMeshProUGUI ObjectiveText;
    int progress, maxprogress;
    string objective;

    [SerializeField]
    private GameObject loseScreen;
 
    public void UseAbility(int tier)
    {
        abilityIcons[tier].UseSpell();
    }

    public void SetCooldownForIcon(int tier, float cooldownTime)
    {
        abilityIcons[tier].CooldownTimeManipulate(cooldownTime);
    }

    public void SetHealthLabel(string newText)
    {
        healthLabel.text = newText;
    }

    public void SetHealthBarFill(float newFillAmount)
    {
        healthBar.fillAmount = newFillAmount;
    }

    public void EnableDamageGradient()
    {
        damageGradient.FullOpacity();
    }

    public void UpdateObjective(int newprogress, int newmaxprogress, string newObjective)
    {
        progress = newprogress;
        maxprogress = newmaxprogress;
        objective = newObjective;
        ObjectiveText.text = objective + "\n  " + progress + " / " + maxprogress;
    }

    public void UpdateObjective(int increment)
    {
        progress = increment;
        ObjectiveText.text = objective + "\n  " + progress + " / " + maxprogress;
    }

    public void GameOver()
    {
        FindObjectOfType<AudioManager>().masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        for(int ii = 0; ii < transform.childCount; ii++)
        {
            transform.GetChild(ii).gameObject.SetActive(false);
        }
        loseScreen.SetActive(true);
        Invoke(nameof(BackToMainMenu), 3.0f);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
