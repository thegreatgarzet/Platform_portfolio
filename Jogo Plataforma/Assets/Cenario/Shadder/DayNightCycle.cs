using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DayNightCycle : MonoBehaviour
{
    public DNColorSet[] daynightcolor;
    public TilemapRenderer tilemapRenderer;
    public SpriteRenderer[] spriterenderers;
    public ControlTransparency transparency;
    public Texture2D texturecolors;
    public bool tilemap, front = true, checktransparency = false;
    public int daytime, counter;
    public float timer, alpha;
    float timerB;
    private void Awake()
    {
        timerB = timer;
        if (tilemap)
        {
            tilemapRenderer = GetComponent<TilemapRenderer>();
        }

        for (int y = 0; y < daynightcolor.Count(); y++)
        {
            daynightcolor[y]._0 = texturecolors.GetPixel(0, y);
            daynightcolor[y]._1 = texturecolors.GetPixel(1, y);
            daynightcolor[y]._2 = texturecolors.GetPixel(2, y);
            daynightcolor[y]._3 = texturecolors.GetPixel(3, y);
            daynightcolor[y]._4 = texturecolors.GetPixel(4, y);
        }
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (front)
            {
                if (daytime < daynightcolor.Count() - 1)
                {
                    daytime++;
                    timer = timerB;
                }
                else if (daytime >= daynightcolor.Count() - 1)
                {
                    daytime--;
                    timer = timerB;
                    front = false;
                }
            }
            else
            {
                if (daytime > 0)
                {
                    daytime--;
                    timer = timerB;
                }
                else if (daytime <= 0)
                {
                    daytime++;
                    timer = timerB;
                    front = true;
                }
            }
        }
        DayNight(daytime);
    }
    public void DayNight(int daytime)
    {
        if (tilemap)
        {

        }
        else
        {
            if (daytime != 0)
            {
                foreach (SpriteRenderer spriterenderer in spriterenderers)
                {
                    spriterenderer.material.SetColor("_Color0", Color.Lerp(daynightcolor[daytime - 1]._0, daynightcolor[daytime]._0, Time.time * 1f));
                    spriterenderer.material.SetColor("_Color1", Color.Lerp(daynightcolor[daytime - 1]._1, daynightcolor[daytime]._1, Time.time * 1f));
                    spriterenderer.material.SetColor("_Color2", Color.Lerp(daynightcolor[daytime - 1]._2, daynightcolor[daytime]._2, Time.time * 1f));
                    spriterenderer.material.SetColor("_Color3", Color.Lerp(daynightcolor[daytime - 1]._3, daynightcolor[daytime]._3, Time.time * 1f));
                    spriterenderer.material.SetColor("_Color4", Color.Lerp(daynightcolor[daytime - 1]._4, daynightcolor[daytime]._4, Time.time * 1f));
                }

            }
            else
            {
                foreach (SpriteRenderer spriterenderer in spriterenderers)
                {
                    spriterenderer.material.SetColor("_Color0", daynightcolor[daytime]._0);
                    spriterenderer.material.SetColor("_Color1", daynightcolor[daytime]._1);
                    spriterenderer.material.SetColor("_Color2", daynightcolor[daytime]._2);
                    spriterenderer.material.SetColor("_Color3", daynightcolor[daytime]._3);
                    spriterenderer.material.SetColor("_Color4", daynightcolor[daytime]._4);
                }
            }
        }
        if (checktransparency)
        {
            if (transparency.enable)
            {
                if (alpha < 1)
                {
                    alpha += 2 * Time.deltaTime;
                }
                else
                {
                    alpha = 1;
                }

            }
            else
            {
                if (alpha > 0)
                {
                    alpha -= 2 * Time.deltaTime;
                }
                else
                {
                    alpha = 0;
                }

            }

            foreach (SpriteRenderer spriterenderer in spriterenderers)
            {
                spriterenderer.material.SetFloat("_NewAlpha", alpha);
            }


        }
    }
}
