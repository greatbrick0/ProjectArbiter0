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
    [SerializeField]
    private GameObject loseTextMain;
    [SerializeField]
    private GameObject loseTextReturning;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject winTextMain;
    [SerializeField]
    private GameObject winTextReturning;
    [SerializeField]
    private GameObject backgroundPanel;

    public LoadSceneButtonGeneral sceneLoader;

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
        ObjectiveText.text = objective + "\n" + progress + " / " + maxprogress;
    }

    public void UpdateObjective(int increment)
    {
        progress = increment;
        ObjectiveText.text = objective + "\n" + progress + " / " + maxprogress;
    }

    public void NewButtonObjective()
    {
        ObjectiveText.text = "Current Objective:\nProceed to Next Room";
    }

    public void NewObjective(string objectiveText)
    {
        ObjectiveText.text = "Current Objective:\n" + objectiveText;
    }

    public void GameOver()
    {
        StartCoroutine(LoseSequence());
    }

    public IEnumerator LoseSequence()
    {
        FindObjectOfType<AudioManager>().masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        for (int ii = 0; ii < transform.childCount; ii++)
        {
            transform.GetChild(ii).gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(1);
        backgroundPanel.SetActive(true);
        LeanTween.value(0, 0.6f, 2).setOnUpdate(FadeInBackground);

        yield return new WaitForSeconds(2);
        loseScreen.SetActive(true);
        LeanTween.value(0, 1, 2).setOnUpdate(FadeInLoseScreen);

        yield return new WaitForSeconds(2);
        LeanTween.value(0, 1, 1).setOnUpdate(FadeInLoseTextMain);

        yield return new WaitForSeconds(2);
        LeanTween.value(0, 1, 1).setOnUpdate(FadeInLoseTextReturning);

        yield return new WaitForSeconds(4);
        BackToMainMenu();
    }

    public void WinGame()
    {
        StartCoroutine(WinSequence());
    } 

    public IEnumerator WinSequence()
    {
        FindObjectOfType<AudioManager>().masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        for (int ii = 0; ii < transform.childCount; ii++)
        {
            transform.GetChild(ii).gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(1);
        backgroundPanel.SetActive(true);
        LeanTween.value(0, 0.6f, 2).setOnUpdate(FadeInBackground);

        yield return new WaitForSeconds(2);
        winScreen.SetActive(true);
        LeanTween.value(0, 1, 2).setOnUpdate(FadeInWinScreen);

        yield return new WaitForSeconds(2);
        LeanTween.value(0, 1, 1).setOnUpdate(FadeInWinTextMain);

        yield return new WaitForSeconds(2);
        LeanTween.value(0, 1, 1).setOnUpdate(FadeInWinTextReturning);

        yield return new WaitForSeconds(4);
        BackToMainMenu();
    }

    #region UI Fade Functions
    private void FadeInBackground(float val)
    {
        backgroundPanel.GetComponent<Image>().color = new Color(0, 0, 0, val);
    }
    private void FadeInWinScreen(float val)
    {
        winScreen.GetComponent<Image>().color = new Color(0.1f, 0.16f, 0.48f, val);
    }
    private void FadeInWinTextMain(float val)
    {
        winTextMain.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, val);
    }
    private void FadeInWinTextReturning(float val)
    {
        winTextReturning.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, val);
    }
    private void FadeInLoseScreen(float val)
    {
        loseScreen.GetComponent<Image>().color = new Color(0.44f, 0.06f, 0.06f, val);
    }
    private void FadeInLoseTextMain(float val)
    {
        loseTextMain.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, val);
    }
    private void FadeInLoseTextReturning(float val)
    {
        loseTextReturning.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, val);
    }
    #endregion

    public void BackToMainMenu()
    {
        sceneLoader.LoadScene("StartMenuScene");
    }
}
