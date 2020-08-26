using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Config_Screen : MonoBehaviour
{
    public Toggle cutsceneToggle;
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (cutsceneToggle.isOn)
        {
            gameManager.canCutscene = false;
        }
        else
        {
            gameManager.canCutscene = true;
        }
    }
}
