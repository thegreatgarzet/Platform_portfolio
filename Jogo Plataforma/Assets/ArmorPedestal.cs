using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPedestal : MonoBehaviour
{
    public int armorPiece, whichArmor;
    MovementController player;
    public ArmorControl armor;
    public Transform setPosPlayer, rayPos;
    public LayerMask playerLayer;
    public bool used;
    public float rayDist;
    Animator anim;
    private void Start()
    {
        player = GameObject.Find("MainChar").GetComponent<MovementController>();
        armor = GameObject.Find("MainChar").GetComponent<ArmorControl>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        bool playerOnTop = Physics2D.Raycast(rayPos.position, Vector2.up, rayDist, playerLayer);
        if (!used && playerOnTop)
        {
            player.ispaused = true;
            player.rb.velocity = new Vector2(0.0f, 0.0f);
            player.transform.position = setPosPlayer.position;
            anim.SetTrigger("upgrade");
            used = true;
        }
    }
    public void GetArmor()
    {
        armor.AtivaPeça(armorPiece, whichArmor);
        
    }
    public void Release()
    {
        
        player.ispaused = false;
    }
    
}
