using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenuController : MonoBehaviour
{
    public int activeMenu;
    public List<Button> allButtons;
    public List<Button> armMenu, legMenu, bodyMenu, headMenu;
    public List<Button> mainButton, enterButtons;
    public Button onEnterButton;
    GameManager gameManager;
    ArmorControl armorControl;
    CheckPointMenuController checkPMenu;
    public GameObject checkPointMenu;
    public bool onInsideMenu;
    private void Start()
    {
        DeactivateAllButtons();
        onEnterButton.Select();
        checkPMenu = GetComponentInParent<CheckPointMenuController>();
        armorControl = FindObjectOfType<ArmorControl>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.onSave = true;
        ActivateMainButton();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && onInsideMenu)
        {
            DeactivateAllButtons();
            ActivateMainButton();
        }else if (Input.GetButtonDown("Cancel") && !onInsideMenu)
        {
            checkPMenu.onInsideMenu = false;
            checkPointMenu.SetActive(true);
            checkPMenu.ReselectButton();
            gameObject.SetActive(false);
        }
    }
    public void AtivaMenu(int i)
    {
        onInsideMenu = true;
        DeactivateMainButtons();
        if (i ==0)
        {
            for (int x = 0; x < armMenu.Count; x++)
            {
                armMenu[x].enabled = true;
            }
            InsideButtonSelect(armMenu);            
        }
        else if(i == 1)
        {
            for (int x = 0; x < legMenu.Count; x++)
            {
                legMenu[x].enabled = true;
            }
            InsideButtonSelect(legMenu);
        }
        else if (i == 2)
        {
            for (int x = 0; x < bodyMenu.Count; x++)
            {
                bodyMenu[x].enabled = true;
            }
            InsideButtonSelect(bodyMenu);
        }
        else if (i == 3)
        {
            for (int x = 0; x < headMenu.Count; x++)
            {
                headMenu[x].enabled = true;
            }
            InsideButtonSelect(headMenu);
        }
    }
    public void DeactivateAllButtons()
    {
        foreach (Button button in allButtons)
        {
            button.enabled = false;
        }
    }
    public void ActivateMainButton()
    {
        foreach (Button item in mainButton)
        {
            item.enabled = true;
        }
        mainButton[0].Select();
        onInsideMenu = false;
    }
    public void DeactivateMainButtons()
    {
        foreach (Button item in mainButton)
        {
            item.enabled = false;
        }
    }
    public void InsideButtonSelect(List<Button> list)
    {
        list[0].Select();
        list[1].Select();
        list[0].Select();
    }
    //Funções para ativar armaduras//
    public void ActivateArmPiece(int i)
    {
        armorControl.ArmActivate(i);
    }
    public void ActivateBodyPiece(int i)
    {
        armorControl.BodyActivate(i);
    }
    public void ActivateLegPiece(int i)
    {
        armorControl.LegsActivate(i);
    }
    public void ActivateHeadPiece(int i)
    {
        armorControl.HeadActivate(i);
    }
}
