using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPrefsGetSet : MonoBehaviour
{
    [Header("Pref Settings")]
    public float fx_volume, msc_volume;
    public int difficulty;
    public bool dialogue;
    public TMP_Dropdown difficulty_dropdown;
    public Slider msc_Slider, fx_Slider;
    public Toggle dialogueToggle;
    public void SetPrefs()
    {
        difficulty = difficulty_dropdown.value;
        fx_volume = fx_Slider.value;
        msc_volume = msc_Slider.value;
        dialogue = dialogueToggle.isOn;
        PlayerPrefs.SetInt("difficulty", difficulty);
        PlayerPrefs.SetFloat("fx_volume", fx_volume);
        PlayerPrefs.SetFloat("msc_volume", msc_volume);
        if (dialogue)
        {
            PlayerPrefs.SetInt("dialogue", 1);
        }
        else
        {
            PlayerPrefs.SetInt("dialogue", 0);
        }
        
    }
}
