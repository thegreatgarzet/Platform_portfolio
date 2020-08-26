using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDoorOpen : MonoBehaviour
{
    DetectPlayerOnRange playerOnRange;
    public Animator[] doors;
    private void Awake()
    {
        playerOnRange = GetComponent<DetectPlayerOnRange>();
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0 && playerOnRange._PlayerOnRange)
        {
            OpenDoor();
        }
    }
    public void OpenDoor()
    {
        foreach (Animator anim in doors)
        {
            anim.SetTrigger("open");
        }
    }
}
