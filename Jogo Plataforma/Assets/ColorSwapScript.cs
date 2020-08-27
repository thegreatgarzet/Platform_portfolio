using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwapScript : MonoBehaviour
{
    public ControleArmas idColor;
    public ColorSet[] colors;
    public Material colorswap, colorswapRep;
    SpriteRenderer renderer;
    public int idarma, colorcount;
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
        colorcount = 0;
        foreach (Color color in colors[idColor].colorpallete)
        {
            renderer.material.SetColor("_Color" + colorcount.ToString(), colors[idColor].colorpallete[colorcount]);
            colorcount++;
        }
    }
}
