using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_Controller : MonoBehaviour
{
    GameManager gameManager;
    public int actual_menu, weapon_check;
    MovementController player;
    public Image icon_menu_main;
    public TMP_Text text_menu_main;
    public Slider hp_Slider, main_hp_slider, selectedSlider;
    public Slider msc_slider, fx_slider;
    public Button sliderButton;
    public bool onSlider;
    ControleArmas controleArmas;
    public List<Button> buttons_menuEnter;
    public List<Slider> sliders;
    public List<GameObject> menuScreens;
    public List<Sprite> icons_menu;
    public List<string> text_menu;
    private void Start()
    {
        AtivaTela();
        controleArmas = GameObject.Find("MainChar").GetComponentInChildren<ControleArmas>();
        player = GameObject.Find("MainChar").GetComponent<MovementController>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (onSlider)
            {
                sliderButton.Select();
                onSlider = false;
            }
            else
            {
                player.ispaused = false;
                gameManager.onMenu = false;
                gameObject.SetActive(false);
            }
        }
        if (Input.GetButtonDown("L1"))
        {
            if (actual_menu ==0)
            {
                actual_menu = menuScreens.Count-1;
            }
            else
            {
                actual_menu--;
            }
            onSlider = false;
            AtivaTela();
            
        }
        if (Input.GetButtonDown("R1"))
        {
            if (actual_menu == menuScreens.Count-1)
            {
                actual_menu = 0;
            }
            else
            {
                actual_menu++;
            }
            onSlider = false;
            AtivaTela();
        }
        if (onSlider)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                selectedSlider.value += 0.2f * Time.deltaTime;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                selectedSlider.value -= 0.2f * Time.deltaTime;
            }
        }
        AtualizaVidaMenu();
        
    }
    public void AlteraIndicadorMenu()
    {
        icon_menu_main.sprite = icons_menu[actual_menu];
        text_menu_main.text = text_menu[actual_menu];
    }
    public void AtivaTela()
    {
        foreach (GameObject tela in menuScreens)
        {
            tela.SetActive(false);
        }
        menuScreens[actual_menu].SetActive(true);
        if (actual_menu == 0 || actual_menu == 1)
        {
            buttons_menuEnter[actual_menu].Select();
        }
        AlteraIndicadorMenu();
    }
    public void AtualizaVidaMenu()
    {
        hp_Slider.maxValue = main_hp_slider.maxValue;
        hp_Slider.value = main_hp_slider.value;
    }
    public void EnterSlider()
    {
        if (onSlider)
        {
            onSlider = false;
        }
        else
        {
            onSlider = true;
        }
    }
    public void SelectSlider(Slider slider)
    {
        onSlider = true;
        selectedSlider = slider;
        slider.Select();
    }
    public void SelectButton(Button button)
    {
        sliderButton = button;
    }
    public void ChangeWeapon(int id)
    {
        weapon_check = 0;
        foreach (int arma in controleArmas.idArmas)
        {
            if (controleArmas.idArmas[weapon_check] == id)
            {
                controleArmas.DirectChangeWeapon(weapon_check);
                weapon_check = 0;
            }
            else
            {
                weapon_check++;
            }
        }
        

    }
}
