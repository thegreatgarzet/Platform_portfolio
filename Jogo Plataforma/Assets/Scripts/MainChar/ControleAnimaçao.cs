using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleAnimaçao : MonoBehaviour
{
    public Animator controle;
    public void Awake()
    {
        controle.SetInteger("state", 0);
    }
    public void TocaAnimaçao(int id)
    {
        controle.SetInteger("state", id);
        //armorAnim.SetInteger("state", id);
    }
}
