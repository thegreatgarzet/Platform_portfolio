using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeleportMenu : MonoBehaviour
{
    public MinimapAreaCheck areacheck;
    public TMP_Text text;
    public int id;
    public Vector2[] tpSpots;
    GameObject player;
    public CheckPointMenuController checkpointControll;
    public GameObject menuScreen, mainMenu, baseMenu;
    GameManager gameManager;
    public bool teleporting;
    private void Awake()
    {
        areacheck = FindObjectOfType<MinimapAreaCheck>();
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.Find("MainChar");
    }
    private void Update()
    {
        if(areacheck != null)
        {
            text.text = areacheck.areaName;
            id = areacheck.id;
        }
        if(id != 0 && Input.GetButtonDown("Atirar") && !teleporting)
        {
            areacheck.canCollide = false;
            teleporting = true;
            gameManager.fading = true;
            gameManager.fadeAnim.SetTrigger("FadeIn");
            menuScreen.SetActive(false);
            
        }
        if (Input.GetButtonDown("Cancel"))
        {
            baseMenu.SetActive(true);
            gameObject.SetActive(false);
            checkpointControll.onInsideMenu = false;
        }
        if (teleporting)
        {
            if (!gameManager.fading)
            {
                TeleportPlayer();
                gameManager.fadeAnim.SetTrigger("FadeOut");
                ExitAllMenus();
            }
        }
    }
    public void TeleportPlayer()
    {
        
        if(id != 0)
        {
            print("teleport");
            player.transform.position = tpSpots[id];
        }
        
    }
    public void ExitAllMenus()
    {
        teleporting = false;
        menuScreen.SetActive(true);
        gameObject.SetActive(false);
        mainMenu.SetActive(false);
        gameManager.ReturnPlayerControls();
    }
}
