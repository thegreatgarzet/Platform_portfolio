using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class GameManager : MonoBehaviour
{
    public GameObject cameraMain, cameraSafe, tileMapWall, bossWall, player, menuScreen, saveScreen, saveScreenFirstMenu, config;
    public GameObject[] cameraBoss;
    public List<GameObject> bossList;
    public int actualBoss;
    public List<GameObject> colidersRPGTalk;
    //SafeRoom Controles
    public Transform safeRoomDoor, lastUsedDoor;
    public bool isOnSafeRoom = false, canCutscene=true, canCallSaveMenu=false, onSave=false, onMenu=false, onTyping, canCallMenu=true;
    //
    //CheckPoint
    public Transform checkPoint;
    //
    bool canCallFade = true;
    public Animator fadeAnim;
    public bool fading;
    //Kill Player
    public GameObject deathFX;
    public List<SpriteRenderer> spritePlayer, activeSprites;
    public Material colorMaterial, dissolveMaterial;
    Material colorRep, dissolveRep;
    public bool playerDead=false, spawnPlayer;
    float fade;
    RecoilControl recoilControl;
    //
    public GameObject spawnFX;
    //
    ControleArmas controleArmas;
    //Dificulty Level
    public int dificulty=0;
    //
    BossValuesControl bossValuesControl;
    //Hp Stuff
    public GameObject objectHpPlayer, objectHpRide;
    private void Awake()
    {
        player = GameObject.Find("MainChar");
        deathFX = GameObject.Find("DeathFX");
        deathFX.SetActive(false);
        controleArmas = player.GetComponentInChildren<ControleArmas>();
        recoilControl = FindObjectOfType<RecoilControl>();
        fade = 1;
        dissolveMaterial.SetFloat("_Fade", 0.5f);
        dissolveMaterial.SetFloat("_Fade", 1);
        dissolveRep = new Material(dissolveMaterial);
        colorRep = new Material(colorMaterial);
        fadeAnim.SetTrigger("FadeOut");
        bossValuesControl = FindObjectOfType<BossValuesControl>();
        //Get info from playerprefs
        dificulty = PlayerPrefs.GetInt("difficulty");
        int dif = PlayerPrefs.GetInt("dialogue");
        if (dif==0)
        {
            canCutscene = false;
            config.GetComponent<Config_Screen>().cutsceneToggle.isOn = false;
        }
        else
        {
            canCutscene = true;
            config.GetComponent<Config_Screen>().cutsceneToggle.isOn = true;
        }
        menuScreen.GetComponent<Menu_Controller>().msc_slider.value = PlayerPrefs.GetFloat("msc_volume");
        menuScreen.GetComponent<Menu_Controller>().fx_slider.value = PlayerPrefs.GetFloat("fx_volume");
        //
    }
    private void Start()
    {
        if (spawnPlayer)
        {
            DisablePlayer();
            player.transform.position = checkPoint.position;
            Instantiate(spawnFX, new Vector2(checkPoint.position.x, checkPoint.position.y + 1), Quaternion.identity);
            spawnPlayer = false;
        }


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            cameraMain.SetActive(true);
            cameraBoss[0].SetActive(false);
        }
     

        if (playerDead)
        {
            player.GetComponent<MovementController>().ispaused = true;
            deathFX.SetActive(true);
            recoilControl.TomandoDano();
            foreach (SpriteRenderer sprite in spritePlayer)
            {
                sprite.material = dissolveRep;
            }
            fade = dissolveRep.GetFloat("_Fade");
            fade -= 0.8f * Time.deltaTime;
            dissolveRep.SetFloat("_Fade", fade);
            if (canCallFade && fade <= 0)
            {
                fadeAnim.SetTrigger("FadeIn");
                Invoke("ResetScene", 1f);
                canCallFade = false;
            }
            //fade out
            //restart scene
        }
        else
        {
            //dissolveMaterial.SetFloat("_Fade", 0.5f);
            if (Input.GetButtonDown("Pause") && !onSave && !onTyping && canCallMenu)
            {
                menuScreen.SetActive(true);
                player.GetComponent<MovementController>().ispaused = true;
                onMenu = true;
            }
            if (Input.GetAxis("Vertical") > 0 && canCallSaveMenu && !onMenu)
            {
                onMenu = true;
                player.GetComponent<MovementController>().ispaused = true;
                saveScreen.SetActive(true);
                saveScreenFirstMenu.SetActive(true);
            }
        }
        
    }
    public void EnablePlayerHP()
    {
        objectHpRide.SetActive(false);
        objectHpPlayer.SetActive(true);
    }
    public void DisablePlayerHP()
    {
        objectHpPlayer.SetActive(false);
    }
    public void EnableRideHP()
    {
        DisablePlayerHP();
        objectHpRide.SetActive(true);
    }
    public void PlayerInvisible()
    {
        
        foreach (SpriteRenderer sprite in spritePlayer)
        {
            if (sprite.enabled)
            {
                activeSprites.Add(sprite);
            }
        }
        foreach (SpriteRenderer sprite in activeSprites)
        {
            sprite.enabled = false;
        }

    }
    public void PlayerVisible()
    {
        foreach (SpriteRenderer sprite in activeSprites)
        {
            sprite.enabled = true;
        }
    }
    public void FadeEnd()
    {
        fading = false;
    }
    public void EnterBossRoom(int bossNum)
    {
        bossWall.SetActive(true);
        cameraBoss[bossNum].SetActive(true);
        cameraMain.SetActive(false);
        tileMapWall.SetActive(true);
        actualBoss = bossNum;
        bossValuesControl.gameObject.SetActive(true);
        bossValuesControl.startBossFight = true;
        if (!canCutscene)
        {
            Invoke("IntroBoss", 1f);
        }
    }
    public void IntroBoss()
    {
        bossList[actualBoss].GetComponent<Animator>().SetTrigger("Intro");
    }
    public void ExitBossRoom()
    {
        bossWall.SetActive(false);
        cameraMain.SetActive(true);
        foreach (GameObject camera in cameraBoss)
        {
            camera.SetActive(false);
        }
        
    }
    public void CameraInSafe(bool inSafe)
    {
        if (!inSafe)
        {
            cameraSafe.SetActive(false);
            cameraMain.SetActive(true);
        }
        else
        {
            cameraMain.SetActive(false);
            cameraSafe.SetActive(true);
        }
    }
    public void SafeRoom()
    {
        if (!isOnSafeRoom)
        {
            player.transform.position = safeRoomDoor.position;
            isOnSafeRoom = true;
            CameraInSafe(true);
        }
        else
        {
            player.transform.position = lastUsedDoor.position;
            isOnSafeRoom = false;
            CameraInSafe(false);
        }
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void DisablePlayer()
    {
        foreach (SpriteRenderer sprite in spritePlayer)
        {
            sprite.enabled = false;
        }
        player.GetComponent<MovementController>().ispaused = true;
    }
    public void EnablePlayer()
    {
        foreach (SpriteRenderer sprite in spritePlayer)
        {
            sprite.enabled = true;
        }
        player.GetComponent<MovementController>().ispaused = false;
    }
    public void ReturnPlayerControls()
    {
        onTyping = false;
        onSave = false;
        onMenu = false;
        player.GetComponent<MovementController>().ispaused = false;
        controleArmas.canshot = true;
        canCallMenu = true;
    }
}
