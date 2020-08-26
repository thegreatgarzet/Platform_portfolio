using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointMenuController : MonoBehaviour
{
    public List<Button> allButtons;
    public Button onEnterButton;
    public GameObject armorMenu, checkpointMenu;
    GameManager gameManager;
    MovementController movementController;
    public bool onInsideMenu;
    private void Start()
    {
        checkpointMenu.SetActive(true);
        ReselectButton();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.onSave = true;
        movementController = FindObjectOfType<MovementController>();
        allButtons[0].Select();
        allButtons[1].Select();
        allButtons[0].Select();
    }
    private void Update()
    {
        gameManager.onSave = true;
        if (Input.GetButtonDown("Cancel") && !onInsideMenu)
        {
            gameManager.onSave = false;
            movementController.ispaused = false;
            gameObject.SetActive(false);
        }
    }
    public void EnterArmorMenu()
    {
        onInsideMenu = true;
        armorMenu.SetActive(true);
        checkpointMenu.SetActive(false);
    }
    public void EnterTpMenu()
    {
        onInsideMenu = true;
    }
    public void ReselectButton()
    {
        allButtons[0].Select();
        allButtons[1].Select();
        allButtons[0].Select();
    }
}
