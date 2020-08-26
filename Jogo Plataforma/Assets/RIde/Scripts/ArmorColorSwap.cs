using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorColorSwap : MonoBehaviour
{
    public Material colorMaterial;
    public List<Color> colorList;
    public ControleArmas controleArmas;
    private void Start()
    {
        controleArmas = GetComponentInChildren<ControleArmas>();
    }
    private void Update()
    {
        UpdateColor(controleArmas.idColor);
    }
    public void UpdateColor(int id)
    {
        colorMaterial.SetColor("Color_BA0719D0", colorList[id]);
    }
}
