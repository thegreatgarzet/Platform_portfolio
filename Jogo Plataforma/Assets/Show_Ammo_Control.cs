using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Ammo_Control : MonoBehaviour
{
    public GameObject eTankButton;
    public bool eTank = false;
    public List<GameObject> ammo_bars;
    ControleArmas controleArmas;
    
    private void Start()
    {
        controleArmas = GameObject.Find("MainChar").GetComponentInChildren<ControleArmas>();
        if (eTank) { eTankButton.SetActive(true);}
    }
    private void Update()
    {
        for (int i = 0; i < controleArmas.idArmas.Count; i++)
        {
            ammo_bars[controleArmas.idArmas[i]].SetActive(true);
        }
    }
}
