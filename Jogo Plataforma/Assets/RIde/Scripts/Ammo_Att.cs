using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo_Att : MonoBehaviour
{
    public List<Slider> slidersAmmo, slidersMenu;
    
    private void Update()
    {
        for (int i = 0; i < slidersMenu.Count; i++)
        {
            slidersMenu[i].maxValue = slidersAmmo[i].maxValue;
            slidersMenu[i].value = slidersAmmo[i].value;
        }    
    }
}
