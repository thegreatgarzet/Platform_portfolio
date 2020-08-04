using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaName : MonoBehaviour
{
    public TMP_Text text;
    public Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
