using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class IconCooldown : MonoBehaviour
{
    [SerializeField]
    private Image ImageCooldown;
    [SerializeField]
    private Image ImageIcon;



    //is it on cooldown?
    private bool isCooldown = false;
    [SerializeField]
    private float cooldownTimer;

    //This counts UP, not DOWN (im sorry.)
    private float timeOnCooldown;

    // Start is called before the first frame update
    private void OnEnable()
    {
        ImageCooldown.fillAmount = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        applyCooldown();
    }

    void applyCooldown()
    {
        if (isCooldown)
        {
            timeOnCooldown += Time.deltaTime;

            if (timeOnCooldown >= cooldownTimer)
            {
                isCooldown = false;
                ImageCooldown.fillAmount = 1.0f;
                //offCooldownGlow.gameObject.SetActive(true);
            }
            else
            {
                ImageCooldown.fillAmount = timeOnCooldown / cooldownTimer;
            }

        }


    }

    public void UseSpell()
    {
        if (isCooldown)
        {
            return;
        }
        isCooldown = true;
        timeOnCooldown = 0.0f;
       // offCooldownGlow.gameObject.SetActive(!isCooldown);
    }

    public void CooldownHardReset()
    {
        timeOnCooldown = 0.0f;
        isCooldown = false;

    }

    public void CooldownTimeManipulate(float changeVal)
    {
        cooldownTimer = changeVal;
    }

    public void ChangeSpellIcon(Sprite spellIcon)
    {
        ImageIcon.sprite = spellIcon;
        ImageCooldown.sprite = spellIcon;
    }
}
