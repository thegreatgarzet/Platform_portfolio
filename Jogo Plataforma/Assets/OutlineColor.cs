using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineColor : MonoBehaviour
{
    public int charge=0;
    public Color colorBase;
    public Color[] chargecolor;
    SpriteRenderer playerrenderer;
    public float timer, timerb;
    private void Awake()
    {
        playerrenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        playerrenderer.material.SetColor("_Outline", Color.Lerp(colorBase, chargecolor[charge], Mathf.PingPong(Time.time, 0.2f)));
    }
}
