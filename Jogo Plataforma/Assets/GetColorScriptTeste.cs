using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetColorScriptTeste : MonoBehaviour
{
    public Texture2D colorpallet;
    public DNColorSet[] colors;
    private void Start()
    {
      
        for (int y = 0; y < colors.Count(); y++)
        {
            colors[y]._0 = colorpallet.GetPixel(0, y);
            colors[y]._1 = colorpallet.GetPixel(1, y);
            colors[y]._2 = colorpallet.GetPixel(2, y);
            colors[y]._3 = colorpallet.GetPixel(3, y);
            colors[y]._4 = colorpallet.GetPixel(4, y);
        }
    }
    
}
