using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwapScript : MonoBehaviour
{
    public ControleArmas idColor;
    public ColorSet[] colors;
    public Material colorswap, colorswapRep;
    SpriteRenderer renderer;
    public int idarma;
    private void Awake()
    {
        idColor = FindObjectOfType<ControleArmas>();
        colorswapRep = new Material(colorswap);
        gameObject.GetComponent<SpriteRenderer>().material = colorswapRep;
        renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        idarma = idColor.idColor;
        ChangePallet(idColor.idColor);
    }
    public void ChangePallet(int idColor)
    {

        renderer.material.SetColor("_Color1", colors[idColor]._1);
        renderer.material.SetColor("_Color2", colors[idColor]._2);
        renderer.material.SetColor("_Color3", colors[idColor]._3);
        renderer.material.SetColor("_Color4", colors[idColor]._4);
        renderer.material.SetColor("_Color5", colors[idColor]._5);
    }
}
