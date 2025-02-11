using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text ClicksTotalText;
    public Text AutoModText;
    public Text Upgrade1AmountText;
    public Text Upgrade2AmountText;
    public Text Upgrade3AmountText;

    public int TotalClicks, autoClickModifier = 0;
    public int TotalClicksModifier = 0;

    public int autoClicksPerSecond;
    public int minimumClicksToUnlockUpgrade;
    public int minimumClicksToUnlockUpgrade2;
    public int minimumClicksToUnlockUpgrade3;
    public int minimumClicksToUnlockMedal;
    public int Upgrade1Amount;
    public int Upgrade2Amount;
    public int Upgrade3Amount;

    public GameObject Medal;

    bool hasUpgrade3;
    float elapsedTime;

    public void Start()
    {
        Medal.SetActive(false);
    }
    public void AddClicks()
    {
        TotalClicks++;
        ClicksTotalText.text = TotalClicks.ToString("0");
        if (hasUpgrade3)
        {
            for (int i = 0; i < TotalClicksModifier; i++)
            {
                TotalClicks++;
                ClicksTotalText.text = TotalClicks.ToString();
            }

        }
    }

    public void AutoClickUpgrade()
    {
        if (TotalClicks >= minimumClicksToUnlockUpgrade)
        {
            autoClickModifier++;
            TotalClicks -= minimumClicksToUnlockUpgrade;
            ClicksTotalText.text = TotalClicks.ToString();
            Upgrade1Amount++;
            Upgrade1AmountText.text = Upgrade1Amount.ToString();
        }
    }

    public void AutoClickUpgrade2()
    {
        if (TotalClicks >= minimumClicksToUnlockUpgrade2)
        {
            autoClickModifier += 3;
            TotalClicks -= minimumClicksToUnlockUpgrade2;
            ClicksTotalText.text = TotalClicks.ToString();
            Upgrade2Amount++;
            Upgrade2AmountText.text = Upgrade2Amount.ToString();
        }
    }

    public void ClickUpgrade3()
    {
        if (TotalClicks >= minimumClicksToUnlockUpgrade3)
        {
            hasUpgrade3 = true;
            TotalClicksModifier++;
            TotalClicks -= minimumClicksToUnlockUpgrade3;
            ClicksTotalText.text = TotalClicks.ToString();
            Upgrade3Amount++;
            Upgrade3AmountText.text = Upgrade3Amount.ToString();
        }


    }

    public void BuyMedal()
    {
        if (TotalClicks >= minimumClicksToUnlockMedal)
        {
            Medal.SetActive(true);
            TotalClicks -= minimumClicksToUnlockMedal;
            ClicksTotalText.text = TotalClicks.ToString();
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < 2f) return;
        elapsedTime = 0;
        if (autoClickModifier == 0) return;
        for (int i = 0; i < autoClickModifier; i++)
        {
            TotalClicks++;
        }
        ClicksTotalText.text = TotalClicks.ToString();
        AutoModText.text = autoClickModifier.ToString();
    }
}

