using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Options : MonoBehaviour
{

    public int buttonId;
    public Button[] buttonList;
    public Button onEnterButton;
    public bool onSliderOrDropBox, started = false;
  
    private void Start()
    {
        OnEnterButton();
       

    }

    private void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            if (onSliderOrDropBox)
            {
                SelectButton();
                onSliderOrDropBox = false;
            }
        }
    }
    public void OnEnterButton()
    {
        onEnterButton.Select();
    }
    public void ActivateSlider(Slider slider)
    {
        slider.Select();
        onSliderOrDropBox = true;
    }
    public void SetButtonID(int buttonSelectedID)
    {
        buttonId = buttonSelectedID;
    }
    public void ActivateDropBox(TMP_Dropdown dropbox)
    {
        dropbox.Select();
        onSliderOrDropBox = true;
    }
    public void SelectButton()
    {
        buttonList[buttonId].Select();
    }
}

