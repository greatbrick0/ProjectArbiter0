using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TutorialObjectiveManager : MonoBehaviour
{
    public int currentTutorialPrompt = 0;
    [SerializeField] AbilityInputSystem abilityInputSystem;

    private void Start()
    {
        abilityInputSystem.abilityLocks[0] = false;
        abilityInputSystem.abilityLocks[1] = false;
        abilityInputSystem.abilityLocks[2] = false;
    }

    [SerializeField] Image ability1Icon;
    [SerializeField] Image ability1Back;
    [SerializeField] Image ability1BackFill;
    [SerializeField] TextMeshProUGUI ability1Bind;

    [SerializeField] Image ability2Icon;
    [SerializeField] Image ability2Back;
    [SerializeField] Image ability2BackFill;
    [SerializeField] TextMeshProUGUI ability2Bind;

    [SerializeField] Image ability3Icon;
    [SerializeField] Image ability3Back;
    [SerializeField] Image ability3BackFill;
    [SerializeField] TextMeshProUGUI ability3Bind;

    [SerializeField] Image sanityMeter;
    [SerializeField] Image sanityMeterBack;
    [SerializeField] Image sanityMeterTrail;
    [SerializeField] TextMeshProUGUI sanityMeterLabel;

    [SerializeField] Image healthBar;
    [SerializeField] Image healthBarBack;
    [SerializeField] TextMeshProUGUI healthBarText;

    public LoadSceneButtonGeneral sceneLoader;

    public void StartEndSequence()
    {
        StartCoroutine(EndSequence());
    }

    public void UpdateTutorialPrompt(int currentPrompt)
    {
        currentTutorialPrompt = currentPrompt;
    }

    public void FadeInAbility1()
    {
        LeanTween.value(0, 1, 1).setOnUpdate(AdjustAbility1);
    }

    public void FadeInAbility2()
    {
        LeanTween.value(0, 1, 1).setOnUpdate(AdjustAbility2);
    }

    public void FadeInAbility3()
    {
        LeanTween.value(0, 1, 1).setOnUpdate(AdjustAbility3);
    }

    public void FadeInSanity()
    {
        LeanTween.value(0, 1, 1).setOnUpdate(AdjustSanity);
    }

    public void FadeInHealthBar()
    {
        LeanTween.value(0, 1, 1).setOnUpdate(AdjustHealthBar);
    }

    private void AdjustAbility1(float val)
    {
        ability1Icon.color = new Color(ability1Icon.color.r, ability1Icon.color.g, ability1Icon.color.b, val);
        ability1Back.color = new Color(ability1Back.color.r, ability1Back.color.g, ability1Back.color.b, val);
        ability1BackFill.color = new Color(ability1BackFill.color.r, ability1BackFill.color.g, ability1BackFill.color.b, val);
        ability1Bind.color = new Color(ability1Bind.color.r, ability1Bind.color.g, ability1Bind.color.b, val);
    }

    private void AdjustAbility2(float val)
    {
        ability2Icon.color = new Color(ability2Icon.color.r, ability2Icon.color.g, ability2Icon.color.b, val);
        ability2Back.color = new Color(ability2Back.color.r, ability2Back.color.g, ability2Back.color.b, val);
        ability2BackFill.color = new Color(ability2BackFill.color.r, ability2BackFill.color.g, ability2BackFill.color.b, val);
        ability2Bind.color = new Color(ability2Bind.color.r, ability2Bind.color.g, ability2Bind.color.b, val);
    }

    private void AdjustAbility3(float val)
    {
        ability3Icon.color = new Color(ability3Icon.color.r, ability3Icon.color.g, ability3Icon.color.b, val);
        ability3Back.color = new Color(ability3Back.color.r, ability3Back.color.g, ability3Back.color.b, val);
        ability3BackFill.color = new Color(ability3BackFill.color.r, ability3BackFill.color.g, ability3BackFill.color.b, val);
        ability3Bind.color = new Color(ability3Bind.color.r, ability3Bind.color.g, ability3Bind.color.b, val);
    }

    private void AdjustSanity(float val)
    {
        sanityMeter.color = new Color(sanityMeter.color.r, sanityMeter.color.g, sanityMeter.color.b, val);
        sanityMeterBack.color = new Color(sanityMeterBack.color.r, sanityMeterBack.color.g, sanityMeterBack.color.b, val);
        sanityMeterTrail.color = new Color(sanityMeterTrail.color.r, sanityMeterTrail.color.g, sanityMeterTrail.color.b, val);
        sanityMeterLabel.color = new Color(sanityMeterLabel.color.r, sanityMeterLabel.color.g, sanityMeterLabel.color.b, val);
    }

    private void AdjustHealthBar(float val)
    {
        healthBar.color = new Color(healthBar.color.r, healthBar.color.g, healthBar.color.b, val);
        healthBarBack.color = new Color(healthBarBack.color.r, healthBarBack.color.g, healthBarBack.color.b, val);
        healthBarText.color = new Color(healthBarText.color.r, healthBarText.color.g, healthBarText.color.b, val);
    }

    private IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(6);
        currentTutorialPrompt = 12;
        yield return new WaitForSeconds(1);
        currentTutorialPrompt = 13;
        yield return new WaitForSeconds(1);
        currentTutorialPrompt = 14;
        yield return new WaitForSeconds(1);
        currentTutorialPrompt = 15;
        yield return new WaitForSeconds(1);
        currentTutorialPrompt = 16;
        yield return new WaitForSeconds(1);
        sceneLoader.LoadScene("StartMenuScene");
    }
}
