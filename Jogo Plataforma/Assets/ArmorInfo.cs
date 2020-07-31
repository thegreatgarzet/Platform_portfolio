using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmorInfo : MonoBehaviour
{
    public TMP_Text[] texts;
    ArmorControl armorControl;
    private void Start()
    {
        armorControl = FindObjectOfType<ArmorControl>();
    }
    private void Update()
    {
        if (armorControl.head[0].enabled)
        {
            UpdateText(0, "*Weapon Cost Reduce");
        }
        else if (armorControl.head[1].enabled)
        {
            UpdateText(0, "*50%HP Hyper Drive");
        }
        else
        {
            UpdateText(0, "*");
        }

        if (armorControl.body[0].enabled)
        {
            UpdateText(1, "*Giga Atack\n*Dmg Reduction");
        }
        else if (armorControl.body[1].enabled)
        {
            UpdateText(1, "*Ignore Collision\n*Dmg Reduction");
        }
        else
        {
            UpdateText(1, "*");
        }

        if (armorControl.arms[0].enabled)
        {
            UpdateText(2, "*Plasma Buster");
        }
        else if (armorControl.arms[1].enabled)
        {
            UpdateText(2, "*Sword Slash");
        }
        else
        {
            UpdateText(2, "*");
        }

        if (armorControl.legs[0].enabled)
        {
            UpdateText(3, "*AirDash");
        }
        else if (armorControl.legs[1].enabled)
        {
            UpdateText(3, "*Double Jump\n*Hyper Dash");
        }
        else
        {
            UpdateText(3, "*");
        }
    }
    public void UpdateText(int id, string text)
    {
        texts[id].text = text;
    }
}
